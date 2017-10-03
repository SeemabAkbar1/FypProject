using System;
using System.Configuration;

namespace PUCIT.AIMRL.WebAppName.DAL
{    
    public sealed class DatabaseHelper
    {
        private static volatile DatabaseHelper _instance;
        private static readonly object SyncRoot = new Object();
        public string MainDBConnectionString
        {
            get;
            private set;
        }
       
        private DatabaseHelper()
        {
            String connStr = ConfigurationManager.ConnectionStrings["MainDBConnectionString"].ConnectionString;
           
            //if (GlobalDataManager.IsCSEncrypted == true)
            {
                //connStr = Common.EncryptDecryptUtility.Decrypt(connStr);
            }
            MainDBConnectionString = connStr;
        }
                
        public static DatabaseHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null) _instance = new DatabaseHelper();
                    }
                }
                return _instance;
            }
        }
    }
}