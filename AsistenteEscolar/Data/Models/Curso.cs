using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsistenteEscolar.Data.Models
{
    public class Curso
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Anio { get; set; }
        [Indexed]
        public int InstitucionId { get; set; }
    }

}
