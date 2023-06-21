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

        public TomarAsistencia(Materia materia_)
        {
            materia = materia_;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            CargarAlumnos();
        }

        private void CargarAlumnos()
        {
            // Obtener la lista de alumnos del curso
            // Puedes utilizar el servicio correspondiente o la lógica que ya tienes implementada

            // Generar los controles de asistencia para cada alumno
            foreach (var alumno in alumnos)
            {
                var checkBox = new CheckBox
                {
                    Text = alumno.Apellido+", "+alumno.Nombre,
                    BindingContext = alumno,
                    Margin = new Thickness(0, 5)
                };
                AlumnosStackLayout.Children.Add(checkBox);
            }
        }

        private void GuardarAsistencia_Clicked(object sender, EventArgs e)
        {
            // Iterar sobre los controles de asistencia y guardar los registros
            foreach (CheckBox checkBox in AlumnosStackLayout.Children)
            {
                var alumno = checkBox.BindingContext as Alumno;
                var presente = checkBox.IsChecked;

                // Guardar el registro de asistencia en la tabla AsistenciaAlumno
                // Puedes utilizar el servicio correspondiente o la lógica que ya tienes implementada
            }

            // Guardar el registro de asistencia en la tabla Asistencia
            // Puedes utilizar el servicio correspondiente o la lógica que ya tienes implementada

            DisplayAlert("Éxito", "La asistencia ha sido registrada correctamente.", "Aceptar");
        }
    }
}