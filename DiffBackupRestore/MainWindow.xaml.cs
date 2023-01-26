using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Path = System.IO.Path;

namespace DiffBackupRestore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<BackupHeaderInfo> BackupInfoList;

        public MainWindow()
        {
            InitializeComponent();
            LoadParameters();


        }


        private void TestConnButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveParameters();
                DatabaseUtility.OpenConnection($"Server={ServerName.Text}; User Id={UserName.Text}; Password={Password.Password}");
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Не удалось подключиться\n{ex.GetBaseException().Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                StatusMessage.Text = "нет подключения";
                SetParametersEnabled(true);

                return;
            }

            StatusMessage.Text = "установлено";
            SetParametersEnabled(false);
            TestConnButton.IsEnabled = false;
        }



        /// <summary>
        /// Set visibility of settings fields when connection established or closed
        /// </summary>
        /// <param name="value"></param>
        private void SetParametersEnabled(bool value)
        {
            ServerName.IsEnabled = UserName.IsEnabled = Password.IsEnabled = TestConnButton.IsEnabled = value;

            SelectFolderButton.IsEnabled = !value;

            CloseConnButton.Visibility = !value ? Visibility.Visible : Visibility.Hidden;
        }

        #region Registry Parameters

        /// <summary>
        /// Save parameters to registry
        /// </summary>
        private void SaveParameters()
        {
            var softwareKey = Registry.CurrentUser.OpenSubKey("Software", true);
            var settings = softwareKey.CreateSubKey("DiffBackuprestore");
            softwareKey.Close();

            settings.SetValue("ServerName", ServerName.Text);
            settings.SetValue("UserName", UserName.Text);
            settings.Close();
        }

        /// <summary>
        /// Load parameters from registry
        /// </summary>
        private void LoadParameters()
        {
            using (var softwareKey = Registry.CurrentUser.OpenSubKey("Software", false))
            {
                if (softwareKey.GetSubKeyNames().Any(key => key == "DiffBackuprestore"))
                    using (var settings = softwareKey.OpenSubKey("DiffBackuprestore", false))
                    {
                        ServerName.Text = settings.GetValue("ServerName").ToString();
                        UserName.Text = settings.GetValue("UserName").ToString();
                    }
            }

        }

        #endregion

        private void SelectFolderButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Title = "Выбор полной копии для восстановления";
            dialog.Filter = "Файл BAK|*.BAK";

            if (dialog.ShowDialog() != true) return;

            FilePathTextBox.Text = dialog.FileName;

            var LoadingWindow = new LoadingWindow() { Owner = this, WindowStartupLocation = WindowStartupLocation.CenterOwner };

            try
            {

                LoadingWindow.Show();

                //var table = DatabaseUtility.GetTable($"RESTORE HEADERONLY FROM DISK = '{dialog.FileName}'");

                //var bckInfo = new BackupHeaderInfo(table);


                BackupInfoList = new List<BackupHeaderInfo>();

                var fullBackup = QueryRepository.GetBackupInfo(dialog.FileName);

                BackupInfoList.Add(fullBackup);

                //adding TRN only
                foreach (var file in Directory.GetFiles(Path.GetDirectoryName(dialog.FileName)))
                {
                    var backupInfo = QueryRepository.GetBackupInfo(file);

                    if (backupInfo.BackupType == 2
                        && backupInfo.DatabaseName == fullBackup.DatabaseName
                        && backupInfo.RecoveryForkId == fullBackup.RecoveryForkId
                        && backupInfo.BackupDate > fullBackup.BackupDate)

                        BackupInfoList.Add(backupInfo);
                }

                BackupInfoList = BackupInfoList.OrderBy(bck => bck.BackupDate).ToList();

                var RecoverableBackups = SelectRecoverableChain(BackupInfoList);

                BackupInfoListView.ItemsSource = RecoverableBackups;

                if (RecoverableBackups.Count != BackupInfoList.Count)
                    MessageBox.Show($"Цепочка бэкапов нарушена. в каталоге присутствуют не все trn?\nВозможно восстановление по {RecoverableBackups.Last().BackupDate}", "Ошибка цепочки", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().Message);
            }
            finally
            {
                LoadingWindow.Close();
            }


        }

        /// <summary>
        /// Check LSN chain and return only full chain
        /// </summary>
        /// <param name="backups"></param>
        /// <returns></returns>
        private List<BackupHeaderInfo> SelectRecoverableChain(List<BackupHeaderInfo> backups)
        {

            //ищем полный бэкап
            var full_backup = backups.First(b => b.BackupType == 1);
            if (full_backup is null) throw new Exception("Не найден полный бэкап в наборе");

            var recoverable = new List<BackupHeaderInfo>();
            recoverable.Add(full_backup);

            //ищем первую ТРН
            var trn_backups = backups.Where(b => b.BackupType == 2).ToList();

            if (trn_backups.Count == 0) return recoverable;

            var firstTrn = trn_backups.FirstOrDefault(trn => trn.LastLsn > full_backup.LastLsn && trn.FirstLsn < full_backup.LastLsn);

            if (firstTrn == null) return recoverable;

            recoverable.Add(firstTrn);
            trn_backups.Remove(firstTrn);

            while (trn_backups.Count != 0)
            {
                var previousTrn = recoverable.Last();
                var currentTrn = trn_backups.FirstOrDefault(trn => trn.FirstLsn == previousTrn.LastLsn);

                if (currentTrn == null) return recoverable;

                recoverable.Add(currentTrn);
                trn_backups.Remove(currentTrn);
            }

            return recoverable;

        }

        private void BackupInfoListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listView = (ListView)sender;
            ExecuteRecoveryButton.IsEnabled = (listView.SelectedItem != null) && !TestConnButton.IsEnabled;
        }

        private void ExecuteRecoveryButton_Click(object sender, RoutedEventArgs e)
        {
            var backups = BackupInfoListView.ItemsSource as List<BackupHeaderInfo>;
            var lastBackup = BackupInfoListView.SelectedItem as BackupHeaderInfo;
            var result = MessageBox.Show($"БД {lastBackup.DatabaseName} будет восстановлена в состояние на {lastBackup.BackupDate}\nНастоятельно рекомендую иметь копию до восстановления.\n Продолжить?", "Восстановление БД", MessageBoxButton.YesNo, MessageBoxImage.Asterisk);

            if (result != MessageBoxResult.Yes) return;

            var LoadingWindow = new LoadingWindow() { Owner = this, WindowStartupLocation = WindowStartupLocation.CenterOwner };
            LoadingWindow.Show();

            try
            {
                foreach (var backup in backups)
                {
                    QueryRepository.RestoreBackup(backup);
                    if (backup == lastBackup)
                        break;
                }

                QueryRepository.UnlockDatabase($"{BackupInfoList.FirstOrDefault()?.DatabaseName}");

                LoadingWindow.Close();

                MessageBox.Show($"БД {BackupInfoList.FirstOrDefault()?.DatabaseName} восстановлена в состояние на {lastBackup.BackupDate}",
                    "Восстановление БД", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Восстановление БД неуспешно.\n{ex.GetBaseException().Message}", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                LoadingWindow.Close();
            }

        }

        private void CloseConnButton_Click(object sender, RoutedEventArgs e)
        {
            DatabaseUtility.SetConnectionString(string.Empty);
            SetParametersEnabled(true);
            ExecuteRecoveryButton.IsEnabled = false;
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            var helpWindow = new HelpWindow();
            helpWindow.Owner = this;

            helpWindow.Show();
        }
    }
}
