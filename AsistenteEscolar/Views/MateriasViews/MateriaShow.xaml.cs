using AsistenteEscolar.Data.Models;
using AsistenteEscolar.Views.AlumnosViews;
using AsistenteEscolar.Views.AsistenciasViews;
using AsistenteEscolar.Views.NotasViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AsistenteEscolar.Views.MateriasViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MateriaShow : TabbedPage
    {
        public MateriaShow(Materia materia)
        {
            InitializeComponent();
            /* Title = "Curso: " + materia.Nombre; */
            // Crear las páginas de notas y asistencias
            var notasPage = new NotasIndex(materia);
            var asistenciasPage = new AsistenciasIndex(materia);

            // Asignar los títulos de las pestañas
            notasPage.Title = "Notas";
            asistenciasPage.Title = "Asistencias";

            // Agregar las páginas a la página de pestañas
            Children.Add(asistenciasPage);
            Children.Add(notasPage);
        }
    }
}