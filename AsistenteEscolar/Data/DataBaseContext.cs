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
            int rowsAffected = await Task.Run(() => Insert(institucion));
            return rowsAffected > 0 ? 1 : 0;
        }

        public async Task UpdateInstitucionAsync(Institucion institucion)
        {
            await Task.Run(() => Update(institucion));
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

        public async Task InsertCursoAsync(Curso curso)
        {
            await Task.Run(() => Insert(curso));
        }

        public async Task UpdateCursoAsync(Curso curso)
        {
            await Task.Run(() => Update(curso));
        }

        public async Task DeleteCursoAsync(Curso curso)
        {
            await Task.Run(() => Delete(curso));
        }

        // CRUD para la tabla Alumno
        //public async Task<List<Alumno>> GetAlumnosByCursoIdAsync(int cursoId)
        //{
        //    return await Table<Alumno>().Where(a => a.CursoId == cursoId).ToListAsync();
        //}

        //public async Task<Alumno> GetAlumnoByIdAsync(int alumnoId)
        //{
        //    return await Table<Alumno>().FirstOrDefaultAsync(a => a.Id == alumnoId);
        //}

        //public async Task InsertAlumnoAsync(Alumno alumno)
        //{
        //    await InsertAsync(alumno);
        //}

        //public async Task UpdateAlumnoAsync(Alumno alumno)
        //{
        //    await UpdateAsync(alumno);
        //}

        //public async Task DeleteAlumnoAsync(Alumno alumno)
        //{
        //    await DeleteAsync(alumno);
        //}
    }
}
