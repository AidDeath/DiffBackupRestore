using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace DiffBackupRestore
{
    public class BackupHeaderInfo
    {
        //public enum BackupFileType {
        //    FullBackup = 1,
        //    TransactionLogBackup = 2
        //}

        public BackupHeaderInfo(string fileName, DataTable dataTable)
        {
            FileName = Path.GetFileName(fileName);
            FileFullName = fileName;
            DataRow row = dataTable.Rows[0];
            BackupType =  (byte)row["BackupType"];
            //Position = (short)row["Position"];
            //DeviceType = (byte)row["DeviceType"];
            //ServerName = (string)row["ServerName"];
            DatabaseName = (string)row["DatabaseName"];
            //DatabaseVersion = (int)row["DatabaseVersion"];
            FirstLsn = (decimal)row["FirstLsn"];
            LastLsn = (decimal)row["LastLsn"];
            DifferentialBaseLsn = (decimal)row["DifferentialBaseLsn"];
            CheckpointLsn = (decimal)row["CheckpointLsn"];
            //BindingId = (string)row["BindingId"];
            //RecoveryForkId = (string)row["RecoveryForkId"];
            //RecoveryForkId = (string)row["RecoveryForkId"];
            BackupDate = (DateTime)row["BackupFinishDate"];

        }

        public string FileName { get; set; }
        public string FileFullName { get; set; }
        public int BackupType { get; set; }
        //public BackupFileType FileType { get;}
        public int Position { get; set; }
        public int DeviceType { get; set; }
        public string ServerName { get; set; }
        public string DatabaseName { get; set; }
        public int DatabaseVersion { get; set; }
        public decimal FirstLsn { get; set; }
        public decimal LastLsn { get; set; }
        public decimal CheckpointLsn { get; set; }
        public decimal DifferentialBaseLsn { get; set; }
        public string BindingId { get; set; }
        public string RecoveryForkId { get; set; }
        public DateTime BackupDate{ get; set; }

        public bool Checked { get; set; }

    }
}
