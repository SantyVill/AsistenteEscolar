using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AsistenteEscolar.Data.Models;
using AsistenteEscolar.Views.AlumnosViews;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AsistenteEscolar.Views.AsistenciasViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AsistenciasIndex : ContentPage
    {
        //private List<Asistencia> asistencias;
        //private List<Alumno> alumnos;
        private Materia materia;
        public ObservableCollection<AlumnoAsistencia> AlumnosAsistencia { get; set; }

        public AsistenciasIndex(Materia materia_)
        {
            materia = materia_;
            InitializeComponent();
            BindingContext = this;
            /* var asistencias = App.Context.GetAsistenciasByMateriaIdAsync(materia.Id); */
            LoadItems();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
        }

        /* private async Task LoadItems()
        {
            asistencias = await App.Context.GetAsistenciasByMateriaIdAsync(materia.Id);
            alumnos = await App.Context.GetAlumnosByCursoIdAsync(materia.CursoId);
            var tableSection = new TableSection("Asistencia");
            tableSection.Add(new TextCell{Text="Alumnos/Fecha"});
            foreach (var asistencia in asistencias)
            {
                tableSection.Add(new TextCell{Text=asistencia.Fecha.ToString()});
            }
            foreach (var alumno in alumnos)
            {
                tableSection.Add(new TextCell{Text=alumno.Apellido+", "+alumno.Nombre});
                foreach (var asistencia in asistencias)
                {
                    tableSection.Add(new TextCell{Text="presente"});
                }
            }
        } */
        private async void LoadItems()
        {
            /* var asistencias = await App.Context.GetAsistenciasByMateriaIdAsync(materia.Id);
            var alumnos = await App.Context.GetAlumnosByCursoIdAsync(materia.CursoId); */
            // var asistencias = App.Context.GetAsistenciasByMateriaIdAsync(materia.Id).Result;
            // var alumnos = App.Context.GetAlumnosByCursoIdAsync(materia.CursoId).Result;

            var asistencias = await App.Context.GetAsistenciasByMateriaIdAsync(materia.Id)/* .GetAwaiter().GetResult() */;
            var alumnos = await App.Context.GetAlumnosByCursoIdAsync(materia.CursoId)/* .GetAwaiter().GetResult() */;
            /* prueba.Children.Add(new Label{Text="asistencias count: "+asistencias.Count().ToString()});
            prueba.Children.Add(new Label{Text="alumnos count: "+alumnos.Count().ToString()});
            prueba.Children.Add(new Label{Text="alumnos"+alumnos[0].Nombre+" Asistencias count: "+(alumnos[0].asistenciasAlumno[0].Asistio?"P":"A")}); */

            for (int i = 0; i <= asistencias.Count(); i++)
            {
                tablaAsistencia.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                if (i != 0)
                {
                    tablaAsistencia.Children.Add(new Label { Text = asistencias[i-1].Fecha.ToString("dd/MM/yyyy")}, i, 0);
                }
                else
                {
                    tablaAsistencia.Children.Add(new Label { Text = "Alumnos/Fecha" }, 0, 0);
                }
            }
            for (int i = 0; i <= alumnos.Count(); i++)
            {
                tablaAsistencia.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                if (i != 0)
                {
                    tablaAsistencia.Children.Add(new Label { Text = alumnos[i-1].Apellido+", "+alumnos[i-1].Nombre}, 0, i);
                }
            }
            for (int i = 1; i <= alumnos.Count(); i++)
            {
                for (int j = 1; j <= asistencias.Count(); j++)
                {
                    tablaAsistencia.Children.Add(new Label { Text = alumnos[i-1].asistenciasAlumno[j-1].Asistio?"P":"A"}, j, i);
                }
            }
        }

        private async void TomarAsistencia_Clicked(object sender, EventArgs e)
        {
            var alumnos = await App.Context.GetAlumnosByCursoIdAsync(materia.CursoId);
            await Navigation.PushAsync(new TomarAsistencia(materia));
        }
    }

    public class AlumnoAsistencia
    {
        public Alumno Alumno { get; set; }
        public List<Asistencia> Asistencias { get; set; }
    }
}
