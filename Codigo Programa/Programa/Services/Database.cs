using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Data.SQLite;
using System.Reflection;
using Applicacion.Models;
using Programa.Models;

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
            Paises pais = Data.FindWithQuery<Paises>($"SELECT * FROM Paises where id_pais = {id}");
            return pais;
        }
        public List<Tipo_Persona> getTipo_Persona()
        {
            return Data.Query<Tipo_Persona>("SELECT * FROM Tipo_Persona");
        }
        public string getTipoPersona(int id)
        {
            Tipo_Persona tipo = Data.FindWithQuery<Tipo_Persona>($"SELECT * FROM Tipo_Persona where id_tipo = {id}");
            return tipo.nombre;
        }
        public List<Personas> getPersonas()
        {
            return Data.Query<Personas>("SELECT * FROM Personas");
        }
        public Personas getPersona(int id)
        {
            Personas persona = Data.FindWithQuery<Personas>($"SELECT * FROM Personas where id_persona = {id}");
            return persona;
        }
        public void createPersona(string id, string fname, string sname, string lname, string slname, int paisid, int tipoid)
        {
            String query = string.Format("INSERT INTO Personas(identificacion, primer_nombre, segundo_nombre, primer_apellido, segundo_apellido, id_pais_origen, id_tipo) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', {5}, {6})", id, fname, sname, lname, slname, paisid, tipoid);
            try { 
                Data.Execute(query);
            }
            catch(Exception e)
            {

            }
        }
        public List<Telefonos> getTelefonos()
        {
            return Data.Query<Telefonos>("SELECT * FROM Telefonos");
        }

        public void createTelefono(string numero, int paisid, int personaid)
        {
            String query = string.Format("INSERT INTO Telefonos VALUES ({0},{1},{2})", numero, paisid, personaid);
            try
            {
                Data.Execute(query);
            }
            catch(Exception e)
            {

            }
        }
        public List<Telefonos> getPersonaTelefonos(int id)
        {
            return Data.Query<Telefonos>($"SELECT * FROM Telefonos WHERE id_persona = {id}");
        }
        public void insertarRegistro(int id_Persona, string registro)
        {
            String query = string.Format("INSERT INTO Control_Actividad(id_persona, actividad) VALUES ({0},'{1}');", id_Persona, registro);
            try
            {
                Data.Execute(query);
            }
            catch (Exception e)
            {

            }
        }
        public void insertarRegistro(string id_Persona, string registro, string fecha)
        {
            String query = string.Format("INSERT INTO Control_Actividad(id_persona, actividad, fecha) VALUES ({0},'{1}','{2}');", id_Persona, registro, fecha);
            try
            {
                Data.Execute(query);
            }
            catch (Exception e)
            {

            }
        }
        public List<Control_Actividad> getActividades()
        {
            return Data.Query<Control_Actividad>("SELECT * FROM Control_Actividad");
        }
    }
}
