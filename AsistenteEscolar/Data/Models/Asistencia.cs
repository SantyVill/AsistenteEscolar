using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsistenteEscolar.Data.Models
{
    public class Asistencia
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int MateriaId { get; set; }
    }
}
