using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DiffBackupRestore
{
    public static class QueryRepository
    {
        public static BackupHeaderInfo GetBackupInfo(string backupPath)
        {
            return new BackupHeaderInfo(backupPath, DatabaseUtility.GetTable($"RESTORE HEADERONLY FROM DISK = '{backupPath}'"));
        }

        public static int RestoreBackup(BackupHeaderInfo backupInfo) 
        {
            switch (backupInfo.BackupType)
            {
                case 1:
                    return DatabaseUtility.Exec($"RESTORE DATABASE {backupInfo.DatabaseName} FROM DISK = '{backupInfo.FileFullName}' WITH NORECOVERY;");
                case 2:
                    return DatabaseUtility.Exec($"RESTORE LOG {backupInfo.DatabaseName} FROM DISK = '{backupInfo.FileFullName}' WITH NORECOVERY;");
                default:
                    return - 1;
            }

        }

        public static void UnlockDatabase(string dbName)
        {
            DatabaseUtility.Exec($"RESTORE DATABASE {dbName} WITH RECOVERY;");
        }
    }
}



// RESTORE HEADERONLY FROM DISK = 'D:\Temp\BackupNorthwind\Northwind_db_202301161100.BAK'

//RESTORE DATABASE Northwind FROM DISK = '\\G506-SR-SQL\Transfer\TMP1\Northwind_db_202301161640.BAK' WITH NORECOVERY;

//RESTORE LOG Northwind FROM DISK = '\\G506-SR-SQL\Transfer\TMP1\Northwind_tlog_202301161640.TRN'  WITH NORECOVERY;