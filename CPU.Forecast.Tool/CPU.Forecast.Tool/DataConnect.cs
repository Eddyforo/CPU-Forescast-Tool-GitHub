using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CPU.Forecast.Tool
{
    public static class DataConnect
    {
        private static System.Data.SqlClient.SqlConnection conn;

        private static string sDataSource;
        private static string sDataBase;
        private static string sUser;
        private static string sPassword;

        public static string SDataSource
        {
            get
            {
                return sDataSource;
            }

            set
            {
                sDataSource = value;
            }
        }

        public static string SDataBase
        {
            get
            {
                return sDataBase;
            }

            set
            {
                sDataBase = value;
            }
        }

        public static string SUser
        {
            get
            {
                return sUser;
            }

            set
            {
                sUser = value;
            }
        }

        public static string SPassword
        {
            get
            {
                return sPassword;
            }

            set
            {
                sPassword = value;
            }
        }

        public static SqlConnection Conn
        {
            get
            {
                return conn;
            }

            set
            {
                conn = value;
            }
        }

        public static void InicializarVaribales()
        {
            SDataSource = string.Empty;
            SDataBase = string.Empty;
            SUser = string.Empty;
            SPassword = string.Empty;
            conn = new SqlConnection();

        }

        public static bool ConnectToSql()
        {
            if (Conn.State != System.Data.ConnectionState.Open)
            {

                SqlConnectionStringBuilder sql = new SqlConnectionStringBuilder();

                sql.DataSource = SDataSource;
                sql.InitialCatalog = SDataBase;
                sql.UserID = SUser;
                sql.Password = SPassword;
                sql.ConnectTimeout = 30;

                Conn = new SqlConnection(sql.ConnectionString);


                try
                {
                    if (!variablesVacias())
                    {
                        Conn.Open();
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Error.addMensaje("Failed to connect to data source", ex.Message, true);
                    return false;
                }

            }

            return true;
        }

        public static void DisConnectSql()
        {
            try
            {
                Conn.Close();
            }
            catch (Exception ex)
            {
                Error.addMensaje("Failed to Disconnect to data source", ex.Message, true);
            }
          
        }

        private static bool variablesVacias()
        {
            if (SDataBase == string.Empty || SDataSource == string.Empty || SUser == string.Empty || SPassword == string.Empty)
            {
                return true;
            }
            return false;
        }
    }
}
