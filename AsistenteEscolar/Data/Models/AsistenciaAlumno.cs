using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsistenteEscolar.Data.Models
{
    public class AsistenciaAlumno
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; } // Agregar una clave primaria única
        
        [Indexed]
        public int AsistenciaId { get; set; }

        [Indexed]
        public int AlumnoId { get; set; }

        public bool Asistio { get; set; }
    }
}
