using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsistenteEscolar.Data.Models
{
    public class Nota
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public int MateriaId { get; set; }
    }
}
