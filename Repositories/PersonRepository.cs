using SQLite;
using System;
using System.Collections.Generic;
using afernandezS5A.Models;

namespace afernandezS5A.Repositories
{
    public class PersonRepository
    {
        string _dbPath;
        private SQLiteConnection? _conn;

        public string StatusMessage { get; set; } = string.Empty;

        public PersonRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        private void Init()
        {
            if (_conn is not null)
                return;

            _conn = new SQLiteConnection(_dbPath);
            _conn.CreateTable<Persona>();
        }

        public void AddNewPerson(string name)
        {
            try
            {
                Init();

                if (string.IsNullOrEmpty(name))
                    throw new Exception("El nombre es requerido");

                Persona persona = new() { Name = name };
                int result = _conn!.Insert(persona);
                StatusMessage = "Dato ingresado correctamente";
            }
            catch (Exception ex)
            {
                StatusMessage = "Error: " + ex.Message;
            }
        }

        public List<Persona> GetAllPerson()
        {
            try
            {
                Init();
                return _conn!.Table<Persona>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = "Error: " + ex.Message;
            }
            return new List<Persona>();
        }

        public void DeletePerson(int id)
        {
            try
            {
                Init();

                var persona = _conn!.Table<Persona>().FirstOrDefault(p => p.Id == id);

                if (persona != null)
                {
                    _conn.Delete(persona);
                    StatusMessage = "Dato eliminado correctamente";
                }
                else
                {
                    StatusMessage = "No se encontró la persona con el ID especificado";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = "Error: " + ex.Message;
            }
        }

        // Método para actualizar una persona existente
        public void UpdatePerson(int id, string newName)
        {
            try
            {
                Init();

                if (string.IsNullOrEmpty(newName))
                    throw new Exception("El nuevo nombre es requerido");

                var persona = _conn!.Table<Persona>().FirstOrDefault(p => p.Id == id);

                if (persona != null)
                {
                    persona.Name = newName;
                    int result = _conn.Update(persona);
                    StatusMessage = "Dato actualizado correctamente";
                }
                else
                {
                    StatusMessage = "No se encontró la persona con el ID especificado";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = "Error: " + ex.Message;
            }
        }
    }
}