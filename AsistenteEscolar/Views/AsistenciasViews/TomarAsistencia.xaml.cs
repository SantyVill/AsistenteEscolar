using AsistenteEscolar.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Shapes;
using Xamarin.Forms.Xaml;

namespace AsistenteEscolar.Views.AsistenciasViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TomarAsistencia : ContentPage
    {
        private Materia materia;
        private List<Alumno> alumnos;
        private Asistencia asistencia;

        public TomarAsistencia(Materia materia_)
        {
            materia = materia_;
            InitializeComponent();

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await CargarAsistenciaAsync();
        }

        private async Task CargarAsistenciaAsync()
        {
            asistencia = new Asistencia
            {
                Fecha = DateTime.Now,
                MateriaId = materia.Id,
            };


            await CargarAlumnosAsync();
        }

        private async Task CargarAlumnosAsync()
        {
            alumnos = await App.Context.GetAlumnosByCursoIdAsync(materia.CursoId);

            foreach (var alumno in alumnos)
            {
                var switchControl = new Switch
                {
                    BindingContext = alumno,
                    Margin = new Thickness(0, 5)
                };
                switchControl.Toggled += SwitchControl_Toggled;

                var label = new Label
                {
                    Text = alumno.Apellido + ", " + alumno.Nombre,
                    Margin = new Thickness(10, 0)
                };

                var stackLayout = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Children = { switchControl, label }
                };

                AlumnosStackLayout.Children.Add(stackLayout);
            }
        }

        private void SwitchControl_Toggled(object sender, ToggledEventArgs e)
        {
            // Manejar el evento de cambio de estado del Switch
            var switchControl = (Switch)sender;
            var alumno = switchControl.BindingContext as Alumno;
            var presente = switchControl.IsToggled;

            var item = new AsistenciaAlumno
            {
                AlumnoId = alumno.Id,
                AsistenciaId = asistencia.Id,
                Asistio = presente
            };

            /* await App.Context.InsertAsistenciaAlumnoAsync(item); */
        }

        private async void GuardarAsistencia_Clicked(object sender, EventArgs e)
        {
            await App.Context.InsertAsistenciaAsync(asistencia);
            // Iterar sobre los controles de asistencia y guardar los registros
            foreach (var stackLayout in AlumnosStackLayout.Children)
            {
                var switchControl = ((StackLayout)stackLayout).Children[0] as Switch;
                var alumno = switchControl.BindingContext as Alumno;
                var presente = switchControl.IsToggled;

                var item = new AsistenciaAlumno
                {
                    AlumnoId = alumno.Id,
                    AsistenciaId = asistencia.Id,
                    Asistio = presente
                };

                // Guardar el registro de asistencia en la tabla AsistenciaAlumno
                await App.Context.InsertAsistenciaAlumnoAsync(item);
            }

            await DisplayAlert("Éxito", "La asistencia ha sido registrada correctamente.", "Aceptar");
            await Navigation.PopAsync();
        }
    }
}
