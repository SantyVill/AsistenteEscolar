using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsistenteEscolar.Data.Models
{
    public class Alumno
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public int CursoId { get; set; }
    }
}
