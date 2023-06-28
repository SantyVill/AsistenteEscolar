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
            LoadItems();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void LoadItems()
        {
            var asistencias = await App.Context.GetAsistenciasByMateriaIdAsync(materia.Id);
            var alumnos = await App.Context.GetAlumnosByMateriaAsync(materia);

            for (int i = 0; i < asistencias.Count()+2; i++)
            {
                tablaAsistencia.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                if (i>1)
                {
                    tablaAsistencia.Children.Add(new Label { Text = asistencias[i-2].Fecha.ToString("dd/MM/yyyy"), FontSize = 16}, i, 0);
                }
            }
            tablaAsistencia.Children.Add(new Label { Text = "Asis"}, 1, 0);
            tablaAsistencia.Children.Add(new Label { Text = "Fecha" }, 0, 0);
            for (int i = 0; i < alumnos.Count()+1; i++)
            {
                tablaAsistencia.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                if (i != 0)
                {
                    tablaAsistencia.Children.Add(new Label { Text = alumnos[i-1].Apellido+", "+alumnos[i-1].Nombre}, 0, i);
                }
            }
            for (int i = 1; i < alumnos.Count()+1; i++)
            {
                for (int j = 1; j < asistencias.Count()+2; j++)
                {
                    if (j!=1)
                    {
                        tablaAsistencia.Children.Add(new Label { Text = alumnos[i-1].AsistenciaAlumnoPorAsistencia(asistencias[j-2])}, j, i);
                    } else
                    {
                        tablaAsistencia.Children.Add(new Label { Text = alumnos[i-1].CantidadAsistencias().ToString()}, j, i);
                    }
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
