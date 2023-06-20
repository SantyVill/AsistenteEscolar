using AsistenteEscolar.Data.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AsistenteEscolar.Data
{
    public class DataBaseContext : SQLiteConnection
    {
        public SQLiteAsyncConnection Connection { get; set; }
        //public DataBaseContext(string path)
        //{
        //    Connection = new SQLiteAsyncConnection(path);
        //    Connection.CreateTableAsync<Institucion>().Wait();
        //}
        public DataBaseContext(string databasePath) : base(databasePath)
        {
            CreateTable<Institucion>();
            CreateTable<Curso>();
            CreateTable<Materia>();
            CreateTable<Alumno>();
            CreateTable<Nota>();
            CreateTable<NotaAlumno>();
            CreateTable<Asistencia>();
            CreateTable<AsistenciaAlumno>();
        }

        // CRUD para la tabla Institucion
        public async Task<List<Institucion>> GetAllInstitucionesAsync()
        {
            return await Task.Run(() => Table<Institucion>().ToList());
        }

        public async Task<Institucion> GetInstitucionByIdAsync(int id)
        {
            return await Task.Run(() => Table<Institucion>().FirstOrDefault(i => i.Id == id));
        }

        public async Task<int> InsertInstitucionAsync(Institucion institucion)
        {
            return await Task.Run(() => Insert(institucion));
        }

        public async Task<int> UpdateInstitucionAsync(Institucion institucion)
        {
            return await Task.Run(() => Update(institucion));
        }

        public async Task<int> DeleteInstitucionAsync(Institucion institucion)
        {
            return await Task.Run(() => Delete(institucion));
            
        }

        // CRUD para la tabla Curso
        public async Task<List<Curso>> GetCursosByInstitucionIdAsync(int institucionId)
        {
            return await Task.Run(() => Table<Curso>().Where(c => c.InstitucionId == institucionId).ToList());
        }

        public async Task<Curso> GetCursoByIdAsync(int cursoId)
        {
            return await Task.Run(() => Table<Curso>().FirstOrDefault(c => c.Id == cursoId));
        }

        public async Task<int> InsertCursoAsync(Curso curso)
        {
            return await Task.Run(() => Insert(curso));
        }

        public async Task<int> UpdateCursoAsync(Curso curso)
        {
            return await Task.Run(() => Update(curso));
        }

        public async Task<int> DeleteCursoAsync(Curso curso)
        {
            return await Task.Run(() => Delete(curso));
        }

        //CRUD para la tabla Alumno
        public async Task<List<Alumno>> GetAlumnosByCursoIdAsync(int cursoId)
        {
            return await Task.Run(() => Table<Alumno>().Where(c => c.CursoId == cursoId).ToList());
        }

        public async Task<Alumno> GetAlumnoByIdAsync(int alumnoId)
        {
            return await Task.Run(() => Table<Alumno>().FirstOrDefault(c => c.Id == alumnoId));
        }

        public async Task<int> InsertAlumnoAsync(Alumno alumno)
        {
            return await Task.Run(() => Insert(alumno));
        }

        public async Task<int> UpdateAlumnoAsync(Alumno alumno)
        {
            return await Task.Run(() => Update(alumno));
        }

        public async Task<int> DeleteAlumnoAsync(Alumno alumno)
        {
            return await Task.Run(()=> Delete(alumno));
        }

        //CRUD para la tabla Materia
        public async Task<List<Materia>> GetMateriasByCursoIdAsync(int cursoId)
        {
            return await Task.Run(() => Table<Materia>().Where(c => c.CursoId == cursoId).ToList());
        }

        public async Task<Materia> GetMateriaByIdAsync(int MateriaId)
        {
            return await Task.Run(() => Table<Materia>().FirstOrDefault(c => c.Id == MateriaId));
        }

        public async Task<int> InsertMateriaAsync(Materia materia)
        {
            return await Task.Run(() => Insert(materia));
        }

        public async Task<int> UpdateMateriaAsync(Materia materia)
        {
            return await Task.Run(() => Update(materia));
        }

        public async Task<int> DeleteMateriaAsync(Materia materia)
        {
            return await Task.Run(()=> Delete(materia));
        }
        //CRUD para la tabla Nota
        public async Task<List<Nota>> GetNotasByCursoIdAsync(int materiaId)
        {
            return await Task.Run(() => Table<Nota>().Where(c => c.MateriaId == materiaId).ToList());
        }

        public async Task<Nota> GetNotaByIdAsync(int NotaId)
        {
            return await Task.Run(() => Table<Nota>().FirstOrDefault(c => c.Id == NotaId));
        }

        public async Task<int> InsertNotaAsync(Nota nota)
        {
            return await Task.Run(() => Insert(nota));
        }

        public async Task<int> UpdateNotaAsync(Nota nota)
        {
            return await Task.Run(() => Update(nota));
        }

        public async Task<int> DeleteNotaAsync(Nota nota)
        {
            return await Task.Run(()=> Delete(nota));
        }

        //CRUD para la tabla NotaAlumno
        public async Task<List<NotaAlumno>> GetNotaAlumnosByNotaIdAsync(int notaId)
        {
            return await Task.Run(() => Table<NotaAlumno>().Where(c => c.NotaId == notaId).ToList());
        }

        /* public async Task<NotaAlumno> GetNotaAlumnoByIdAsync(int NotaAlumnoId)
        {
            return await Task.Run(() => Table<NotaAlumno>().FirstOrDefault(c => c.Id == NotaAlumnoId));
        } */

        public async Task<int> InsertNotaAlumnoAsync(NotaAlumno notaAlumno)
        {
            return await Task.Run(() => Insert(notaAlumno));
        }

        public async Task<int> UpdateNotaAlumnoAsync(NotaAlumno notaAlumno)
        {
            return await Task.Run(() => Update(notaAlumno));
        }

        public async Task<int> DeleteNotaAlumnoAsync(NotaAlumno notaAlumno)
        {
            return await Task.Run(()=> Delete(notaAlumno));
        }

        //CRUD para la tabla Asistencia
        public async Task<List<Asistencia>> GetAsistenciasByMateriaIdAsync(int materiaId)
        {
            return await Task.Run(() => Table<Asistencia>().Where(c => c.MateriaId == materiaId).ToList());
        }

        public async Task<Asistencia> GetAsistenciaByIdAsync(int asistenciaId)
        {
            return await Task.Run(() => Table<Asistencia>().FirstOrDefault(c => c.Id == asistenciaId));
        }

        public async Task<int> InsertAsistenciaAsync(Asistencia asistencia)
        {
            return await Task.Run(() => Insert(asistencia));
        }

        public async Task<int> UpdateAsistenciaAsync(Asistencia asistencia)
        {
            return await Task.Run(() => Update(asistencia));
        }

        public async Task<int> DeleteAsistenciaAsync(Asistencia asistencia)
        {
            return await Task.Run(()=> Delete(asistencia));
        }

        //CRUD para la tabla AsistenciaAlumno
        public async Task<List<AsistenciaAlumno>> GetAsistenciaAlumnosByMateriaIdAsync(int materiaId)
        {
            return await Task.Run(() => Table<AsistenciaAlumno>().Where(c => c.AsistenciaId == materiaId).ToList());
        }

        /* public async Task<AsistenciaAlumno> GetAsistenciaAlumnoByIdAsync(int asistenciaAlumnoId)
        {
            return await Task.Run(() => Table<AsistenciaAlumno>().FirstOrDefault(c => c.Id == asistenciaAlumnoId));
        } */

        public async Task<int> InsertAsistenciaAlumnoAsync(AsistenciaAlumno asistenciaAlumno)
        {
            return await Task.Run(() => Insert(asistenciaAlumno));
        }

        public async Task<int> UpdateAsistenciaAlumnoAsync(AsistenciaAlumno asistenciaAlumno)
        {
            return await Task.Run(() => Update(asistenciaAlumno));
        }

        public async Task<int> DeleteAsistenciaAlumnoAsync(AsistenciaAlumno asistenciaAlumno)
        {
            return await Task.Run(()=> Delete(asistenciaAlumno));
        }
    }

}
