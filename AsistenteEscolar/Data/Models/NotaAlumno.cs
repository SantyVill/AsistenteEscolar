using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsistenteEscolar.Data.Models
{
    public class NotaAlumno
    {
        [Indexed]
        public int NotaId { get; set; }

        [Indexed]
        public int AlumnoId { get; set; }

        public decimal Nota { get; set; }
    }
}
