using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Data.SQLite;
using System.Reflection;
using Applicacion.Models;

namespace Applicacion.Services
{
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

        private string createDB()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            const string path = "Application.Database.TextFile1.txt";
            using (Stream stream = assembly.GetManifestResourceStream(path))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
        private string loadDB()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            const string path = "Application.Database.DMS.sql";
            using (Stream stream = assembly.GetManifestResourceStream(path))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public SQLite.SQLiteConnection Data
        {
            get { return localDB; }
        }

        public List<Paises> getPaises()
        {
            return Data.Query<Paises>("SELECT * FROM Paises");
        }
        public Paises getPais( int id)
        {
            Paises pais =  Data.Find<Paises>($"SELECT * FROM Paises where id_pais = {id}");
            return pais;
        }
        public List<Tipo_Persona> getTipo_Persona()
        {
            return Data.Query<Tipo_Persona>("SELECT * FROM Tipo_Persona");
        }
        public List<Personas> getPersonas()
        {
            return Data.Query<Personas>("SELECT * FROM Personas");
        }
        public void createPersona(string id, string fname, string sname, string lname, string slname, int paisid, int tipoid)
        {
            String query = string.Format("INSERT INTO Personas(identificacion, primer_nombre, segundo_nombre, primer_apellido, segundo_apellido, id_pais_origen, id_tipo) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', {5}, {6})", id, fname, sname, lname, slname, paisid, tipoid);
            Data.Execute(query);
        }
        public List<Telefonos> getTelefonos()
        {
            return Data.Query<Telefonos>("SELECT * FROM Telefonos");
        }
    }
}
