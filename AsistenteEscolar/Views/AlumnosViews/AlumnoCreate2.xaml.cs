using AsistenteEscolar.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AsistenteEscolar.Views.AlumnosViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlumnoCreate2 : ContentPage
    {
        private Curso curso;
        public AlumnoCreate2(Curso curso_)
        {
            curso = curso_;
            InitializeComponent();
        }

        private async void Guardar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var item = new Alumno
                {
                    Nombre = nombre.Text,
                    Apellido = apellido.Text,
                    CursoId = curso.Id,
                };

                var resultado = await App.Context.InsertAlumnoAsync(item);
                if (resultado == 1)
                {
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "No se pudo guardar los datos del Alumno", "Ok");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Aceptar");
            }
        }
    }
}