using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<AsistenciaAlumno> asistenciasAlumno;

        public string AsistenciaAlumnoPorAsistencia(Asistencia asistencia){
            foreach (var item in this.asistenciasAlumno)
            {
                if (item.AsistenciaId==asistencia.Id)
                {
                    return item.Asistio?"P":"A";
                }
            }
            return "-";
        }

        public int CantidadAsistencias()
        {
            if (this.asistenciasAlumno != null)
            {
                int cantidadAsistencias = 0;
                foreach (var item in this.asistenciasAlumno)
                {
                    if (item.Asistio)
                    {
                        cantidadAsistencias += 1;
                    }
                }
                return cantidadAsistencias;
            }
            else
            {
                return 0;
            }
        }

        public int PorcentajeAsistencias()
        {
            if (this.asistenciasAlumno.Count()==0)
            {
                return 0;
            }
            else
            {
                return this.CantidadAsistencias()*100 / this.asistenciasAlumno.Count();
            }
        }

        /* public int CantidadInasistencias(){
            if (this.asistenciasAlumno!=null)
            {
                int cantidadInasistencias=0;
                foreach (var item in this.asistenciasAlumno)
                {
                    if (item.Asistio)
                    {
                        cantidadInasistencias++;
                    }
                }
                return cantidadInasistencias;
            } else
            {
                return 0;
            }
        } */
    }
}
