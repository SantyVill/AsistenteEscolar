using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        private Materia materia;
        public List<Asistencia> Asistencias { get; set; }

        public AsistenciasIndex(Materia materia_)
        {
            materia = materia_;
            InitializeComponent();
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await CargarAsistencias();
        }

        private async Task CargarAsistencias()
        {
            Asistencias = await App.Context.GetAsistenciasByMateriaIdAsync(materia.Id);

            foreach (var asistencia in Asistencias)
            {
                asistencia.asistenciasAlumnos = await App.Context.GetAsistenciasAlumnosByAsistenciaIdAsync(asistencia.Id);
            }

            AsistenciaListView.ItemsSource = Asistencias;
        }

        private async void TomarAsistencia_Clicked(object sender, EventArgs e)
        {
            var alumnos = await App.Context.GetAlumnosByCursoIdAsync(materia.CursoId);
            await Navigation.PushAsync(new TomarAsistencia(materia));
        }
    }
}
