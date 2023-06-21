using System;
using System.Collections.Generic;
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
        public AsistenciasIndex(Materia materia_)
        {
            materia = materia_;
            InitializeComponent();
        }

        private async void TomarAsistencia_Clicked(object sender, EventArgs e)
        {
            var alumnos = await App.Context.GetAlumnosByCursoIdAsync(materia.CursoId);
            await Navigation.PushAsync(new TomarAsistencia(materia, alumnos));
        }
    }
}