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
        public List<NotaAlumno2> notas;

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

        public AsistenciaAlumno AsistenciaDelAlumnoPorAsistencia(Asistencia asistencia)
        {
            foreach (var item in this.asistenciasAlumno)
            {
                if (item.AsistenciaId == asistencia.Id)
                {
                    return item;
                }
            }
            return null;
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

        public NotaAlumno2 NotaDelAlumnoPorNota(Nota nota)
        {
            foreach (var item in this.notas)
            {
                if (item.NotaId == nota.Id)
                {
                    return item;
                }
            }
            return null;
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

        public float PromedioNotas(Materia materia)
        {
            List<Nota> notas = new List<Nota>();
            notas = App.Context.GetNotasByMateriaIdAsync(materia.Id).Result;
            float promerdio=0;
            if (this.notas.Count > 0)
            {
                foreach (var item in this.notas)
                {
                    promerdio += (float)item.Nota;
                }           
                return promerdio/this.notas.Count;
            }
            return 0;
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
