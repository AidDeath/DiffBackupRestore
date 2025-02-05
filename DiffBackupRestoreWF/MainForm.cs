using DiffBackupRestore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace DiffBackupRestoreWF
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            LoadParameters();
            FilePathTextBox.Text = "Установите соединение и выберите каталог с бэкапами";
        }

        public List<BackupHeaderInfo> BackupInfoList;


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

        private void TestConnButton_Click(object sender, EventArgs e)
        {
            try
            {
                SaveParameters();
                DatabaseUtility.OpenConnection($"Server={ServerName.Text}; User Id={UserName.Text}; Password={Password.Text}");
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Не удалось подключиться\n{ex.GetBaseException().Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                StatusMessage.Text = "нет подключения";
                SetParametersEnabled(true);

                return;
            }

            StatusMessage.Text = "установлено";
            SetParametersEnabled(false);
            TestConnButton.Enabled = false;
            FilePathTextBox.Text = "Выберите путь к каталогу с бэкапами доступный с сервера";

        }


        /// <summary>
        /// Set visibility of settings fields when connection established or closed
        /// </summary>
        /// <param name="value"></param>
        private void SetParametersEnabled(bool value)
        {
            ServerName.Enabled = UserName.Enabled = Password.Enabled = TestConnButton.Enabled = value;

            SelectFolderButton.Enabled = CloseConnButton.Visible =  !value;

        }

        private void CloseConnButton_Click(object sender, EventArgs e)
        {
            DatabaseUtility.SetConnectionString(string.Empty);
            SetParametersEnabled(true);
            ExecuteRecoveryButton.Enabled = false;
            StatusMessage.Text = "отключено";
        }

        [STAThread]
        private void SelectFolderButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Title = "Выбор полной копии для восстановления";
            dialog.Filter = "Файл BAK|*.BAK";

            if (dialog.ShowDialog() != DialogResult.OK) return;

            FilePathTextBox.Text = dialog.FileName;

            //var LoadingWindow = new LoadingWindow() { Owner = this, WindowStartupLocation = WindowStartupLocation.CenterOwner };
            BusyForm.SetBusyText("sadfsadf");
            BusyForm.ShowBusyForm(this.Location);
            //busyForm.BusyLabel.Text = "Чтение и анализ бэкапов...";
            

            try
            {

                //LoadingWindow.Show();
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

                BackupInfoDataGridView.AutoGenerateColumns = false;
                BackupInfoDataGridView.DataSource = RecoverableBackups;
                
                BackupInfoDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                //BackupInfoListView.View;

                BusyForm.CloseForm();
                if (RecoverableBackups.Count != BackupInfoList.Count)
                    MessageBox.Show($"Цепочка бэкапов нарушена. в каталоге присутствуют не все trn?\nВозможно восстановление по {RecoverableBackups.Last().BackupDate}", "Ошибка цепочки", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            catch (Exception ex)
            {
                BusyForm.CloseForm();
                MessageBox.Show(ex.GetBaseException().Message);
            }
            finally
            {
                //LoadingWindow.Close();
                //BusyForm.CloseForm();
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

        private void ExecuteRecoveryButton_Click(object sender, EventArgs e)
        {
            var backups = BackupInfoDataGridView.DataSource as List<BackupHeaderInfo>;

            var lastBackup = BackupInfoDataGridView.SelectedRows[0].DataBoundItem as BackupHeaderInfo;
            var result = MessageBox.Show($"БД {lastBackup.DatabaseName} будет восстановлена в состояние на {lastBackup.BackupDate}\nНастоятельно рекомендую иметь копию до восстановления.\n Продолжить?", "Восстановление БД", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);

            if (result != DialogResult.Yes) return;

            //var LoadingWindow = new LoadingWindow() { Owner = this, WindowStartupLocation = WindowStartupLocation.CenterOwner };
            //LoadingWindow.Show();            

            try
            {
                foreach (var backup in backups)
                {
                    QueryRepository.RestoreBackup(backup);
                    if (backup == lastBackup)
                        break;
                }

                QueryRepository.UnlockDatabase($"{BackupInfoList.FirstOrDefault()?.DatabaseName}");

                //LoadingWindow.Close();

                MessageBox.Show($"БД {BackupInfoList.FirstOrDefault()?.DatabaseName} восстановлена в состояние на {lastBackup.BackupDate}",
                    "Восстановление БД", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Восстановление БД неуспешно.\n{ex.GetBaseException().Message}", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //LoadingWindow.Close();
            }

        }

        private void BackupInfoDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            //var listView = (ListView)sender;
            
            ExecuteRecoveryButton.Enabled = (BackupInfoDataGridView.SelectedRows.Count > 0 
                                            && BackupInfoDataGridView.SelectedRows[0]?.DataBoundItem != null) 
                && !TestConnButton.Enabled;
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseConnButton_Click(this, e);
        }

        private void HelpButton_Click(object sender, EventArgs e)
        {
            HelpForm helpForm = new HelpForm() { Owner = this, StartPosition = FormStartPosition.CenterParent};
            helpForm.ShowDialog();

        }

        private void Password_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return) TestConnButton_Click(this, e);
        }
    }
}
