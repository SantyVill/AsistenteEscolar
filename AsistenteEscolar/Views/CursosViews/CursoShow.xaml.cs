using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsistenteEscolar.Data.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AsistenteEscolar.Views.MateriasViews;
using AsistenteEscolar.Views.AlumnosViews;

namespace AsistenteEscolar.Views.CursosViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CursoShow : TabbedPage
    {
        public CursoShow(Curso curso)
        {
            InitializeComponent();
            Title ="Curso: "+curso.Nombre;

            // Crear las páginas de materias y alumnos
            var materiasPage = new MateriasIndex(curso);
            var alumnosPage = new AlumnosIndex(curso);

            // Asignar los títulos de las pestañas
            materiasPage.Title = "Materias";
            alumnosPage.Title = "Alumnos";

            // Agregar las páginas a la página de pestañas
            Children.Add(materiasPage);
            Children.Add(alumnosPage);
        }
    }
}