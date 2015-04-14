using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCCSC131Project.Models
{
    public class DBConnection
    {

        private DBConnection() { }
        private string databaseName = string.Empty;
        public string DatabaseName
        {
            get { return databaseName; }
            set { databaseName = value; }
        }
        public string Password { get; set; }
        private MySqlConnection Connection = null;
        private static DBConnection _instance = null;
        public static DBConnection Instance()
        {
            if (_instance == null)
                _instance = new DBConnection();
            return _instance;

        }

        public bool IsConnect()
        {
            bool result = true;
            if (Connection == null)
            {
                if (databaseName == string.Empty)
                    result = false;
                string StrCon = string.Format("Server=camellia.arvixe.com; database={0}; UID=codemonkeys; password=45EN2F3S8W2~_cc5+d_S97j9_u4~ad78wK6ce", databaseName);
                Connection = new MySqlConnection(StrCon);
                Connection.Open();
                result = true;
            }
            
                
            return result;
        }

        public MySqlConnection GetConnection()
        {
            return Connection;
        }



        public void Close()
        {
            Connection.Close();
        }

        public List<String> values(string query, String outputField)
        {
            //string results = "";
            List<string> results = new List<string>();
            DBConnection DBCon = DBConnection.Instance();
            DBCon.DatabaseName = "wellDressedDatabase";
            if (DBCon.IsConnect())
            {
                //string query = "Select * from users";
                MySqlCommand cmd = new MySqlCommand(query, DBCon.GetConnection());
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    //results += dr.GetString(dr.GetOrdinal("fname")) + "<br/>\n";
                    results.Add(dr.GetString(dr.GetOrdinal(outputField)));
                }
                dr.Close();
                DBCon.Close();
            }
            return results;
        }
    }	
}