using SQLite;
using System;

namespace afernandezS5A.Models
{
    [Table("persona")]
    public class Persona
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(25)]
        public string? Name { get; set; } 
    }
}