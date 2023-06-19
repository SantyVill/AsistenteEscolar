using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsistenteEscolar.Data.Models
{
    public class AsistenciaAlumno
    {
        [Indexed]
        public int AsistenciaId { get; set; }

        [Indexed]
        public int AlumnoId { get; set; }

        public bool Asistio { get; set; }
    }
}
