using afernandezS5A.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace afernandezS5A.Repositories
{
    public class PersonRepository
    {
        string _dbPath;
        private SQLite.SQLiteConnection _conn;

        // Constructor
        public string StatusMessage { get; set; }
        public PersonRepository(string Path)
        {
            _dbPath = Path;

        }
        // Conexion a la base de datos y creacion de la tabla persona
        private void Init()
        {
            if (_conn is not null)
                return;
            _conn = new(_dbPath);
            _conn.CreateTable<Persona>();
        }

        // Metodo para agregar una persona a la base de datos insert
        public void AddNewPerson(string name)
        {
            int result = 0;
            try
            {
                Init();
                if (string.IsNullOrEmpty(name))
                    throw new Exception("El nombre es requerido");
                Persona persona = new() { Name = name };
                result = _conn.Insert(persona);
                StatusMessage = string.Format("Dato ingresad");

            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Error:" + ex.Message);
            }
        }

        // Metodo para obtener todas las personas de la base de datos Select

        public List<Persona> GetAllPerson()
        {
            try
            {
                Init();
                return _conn.Table<Persona>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Error:" + ex.Message);
            }
            return new List<Persona>();
        }


        // Metodo para eliminar una persona de la base de datos Delete

        public void DeletePerson(int id)
        {
            try
            {
                Init();

                // Buscamos el registro por su ID para asegurarnos de que exista
                var persona = _conn.Table<Persona>().FirstOrDefault(p => p.Id == id);

                if (persona != null)
                {
                    // Ejecutamos la eliminacion
                    int result = _conn.Delete(persona);
                    StatusMessage = string.Format("Dato eliminado correctamente");
                }
                else
                {
                    StatusMessage = string.Format("No se encontró la persona con el ID especificado");
                }
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Error: " + ex.Message);
            }
        }

    }
}
