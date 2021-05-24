using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using Microsoft.Data.Sqlite;
using System.Data.SQLite;

namespace Applicacion.Services
{
    class Personas
    {
        public int id_pais { get; set; }
        public string iso { get; set; }
        public string nombre_pais { get; set; }
        public int codigo_telefono { get; set; }
    }
    class Database
    {
        string dbPath = System.Environment.CurrentDirectory + "\\DB";
        string dbFilePath;
        private readonly SQLite.SQLiteConnection localDB;

        public Database()
        {
            if (!string.IsNullOrEmpty(dbPath) && !Directory.Exists(dbPath))
                Directory.CreateDirectory(dbPath);
            dbFilePath = dbPath + "\\AirCanadaCenter.db";
            if (!System.IO.File.Exists(dbFilePath))
            {
                SQLiteConnection.CreateFile(dbFilePath);
            }
            localDB = new SQLite.SQLiteConnection(dbFilePath);
        }

        public SQLite.SQLiteConnection Data
        {
            get { return localDB; }
        }

        public string ReadData(String query)
        {
            String Result = "";
            var list = Data.Query<Personas>(query);
            return Result;
        }

















        /*
        SqliteConnection dbConnection;
        SqliteCommand command;
        string sqlCommand;
        string dbPath = System.Environment.CurrentDirectory + "\\DB";
        string dbFilePath;

        public Database()
        {
            if (!string.IsNullOrEmpty(dbPath) && !Directory.Exists(dbPath))
                Directory.CreateDirectory(dbPath);
            dbFilePath = dbPath + "\\AirCanadaCenter.db";
            if (!System.IO.File.Exists(dbFilePath))
            {
                SQLiteConnection.CreateFile(dbFilePath);
            }
        }
        public string createDbConnection()
        {
            string strCon = string.Format("Data Source={0};", dbFilePath);
            dbConnection = new SqliteConnection(strCon);
            dbConnection.Open();
            command = dbConnection.CreateCommand();
            return strCon;
        }
        public String read(string query)
        {
            string strCon = string.Format("Data Source={0};", dbFilePath);
            dbConnection = new SqliteConnection(strCon);
            dbConnection.Open();

            using var cmd = new SqliteCommand(query, dbConnection);
            string result = cmd.ExecuteScalar().ToString();

            return result;
        }


        public string ReadData(string query)
        {
            SqliteDataReader sqlite_datareader;
            SqliteCommand sqlite_cmd;
            string myreader = "";

            string strCon = string.Format("Data Source={0};", dbFilePath);
            SqliteConnection conn = new SqliteConnection(strCon);
            conn.Open();

            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = query;

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                myreader = sqlite_datareader.GetString(0);
            }
            conn.Close();
            return myreader;
        }

        public bool checkIfExist(string tableName)
        {
            command.CommandText = "SELECT name FROM sqlite_master WHERE name='" + tableName + "'";
            var result = command.ExecuteScalar();

            return result != null && result.ToString() == tableName ? true : false;
        }

        public void executeQuery(string sqlCommand)
        {
            SqliteCommand triggerCommand = dbConnection.CreateCommand();
            triggerCommand.CommandText = sqlCommand;
            triggerCommand.ExecuteNonQuery();
        }

        public bool checkIfTableContainsData(string tableName)
        {
            command.CommandText = "SELECT count(*) FROM " + tableName;
            var result = command.ExecuteScalar();

            return Convert.ToInt32(result) > 0 ? true : false;
        }
    */
    }
}
