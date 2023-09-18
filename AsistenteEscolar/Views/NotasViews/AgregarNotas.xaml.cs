using AsistenteEscolar.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AsistenteEscolar.Views.NotasViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgregarNotas : ContentPage
    {
        private Materia materia;
        private List<Alumno> alumnos;
        private Nota nota;

        public AgregarNotas(Materia materia_ ,Nota nota_)
        {
            materia = materia_;
            nota = nota_;
            InitializeComponent();

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await CargarItems();
        }

        private async Task CargarItems()
        {
            await CargarAlumnosAsync();
        }

        private async Task CargarAlumnosAsync()
        {
            alumnos = await App.Context.GetAlumnosByCursoIdAsync(materia.CursoId);

            foreach (var alumno in alumnos)
            {
                

                var label = new Label
                {
                    Text = alumno.Apellido + ", " + alumno.Nombre,
                    Margin = new Thickness(10, 0)
                };
                var entry = new Entry
                {
                    BindingContext = alumno,
                    Margin = new Thickness(0, 5),
                    Keyboard = Keyboard.Numeric,
                    MaxLength = 2,
                    HorizontalOptions = LayoutOptions.Fill,
                    WidthRequest = 40, 
                };

                var stackLayout = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Children = { entry, label }
                };

                AlumnosStackLayout.Children.Add(stackLayout);
            }
        }

        /*private void SwitchControl_Toggled(object sender, ToggledEventArgs e)
        {
            // Manejar el evento de cambio de estado del Switch
            var switchControl = (Switch)sender;
            var alumno = switchControl.BindingContext as Alumno;
            var presente = switchControl.IsToggled;

            var item = new NotaAlumno
            {
                AlumnoId = alumno.Id,
                NotaId = nota.Id,
                //Nota = nota
            };

             await App.Context.InsertAsistenciaAlumnoAsync(item); 
        }*/

        private async void GuardarNota_Clicked(object sender, EventArgs e)
        {
            /*//await App.Context.InsertNotaAsync(asistencia);
            // Iterar sobre los controles de asistencia y guardar los registros
            foreach (var stackLayout in AlumnosStackLayout.Children)
            {
                var switchControl = ((StackLayout)stackLayout).Children[0] as Switch;
                var alumno = switchControl.BindingContext as Alumno;
                var presente = switchControl.IsToggled;

                var item = new NotaAlumno
                {
                    AlumnoId = alumno.Id,
                    NotaId = nota.Id,
                    Nota = nota
                };

                // Guardar el registro de asistencia en la tabla AsistenciaAlumno
                await App.Context.InsertAsistenciaAlumnoAsync(item);
            }

            await DisplayAlert("Éxito", "La asistencia ha sido registrada correctamente.", "Aceptar");
            await Navigation.PopAsync();*/
            int resultado = 0;
            foreach (var stackLayout in AlumnosStackLayout.Children)
            {
                var entry = ((StackLayout)stackLayout).Children[0] as Entry;
                var alumno = entry.BindingContext as Alumno;
                if (entry != null && !string.IsNullOrWhiteSpace(entry.Text))
                {
                    // Parsear la nota ingresada (asumiendo que las notas son números válidos)
                    if (float.TryParse(entry.Text, out float notaValue) && (int)notaValue <= 10 && (int)notaValue >= 1)
                    {
                        var notaAlumno = new NotaAlumno2
                        {
                            AlumnoId = alumno.Id,
                            NotaId = nota.Id,
                            Nota = (int)notaValue
                        };

                        //await DisplayAlert("Error", "Alumno: " + alumno.NombreCompleto()+"    nota: "+notaAlumno.Nota, "Aceptar");
                        // Guardar el registro de nota en la base de datos
                        resultado+= await App.Context.InsertNotaAlumnoAsync(notaAlumno);
                    }
                    else
                    {
                        await DisplayAlert("Error", "Por favor, ingrese una nota válida para " + alumno.Apellido+", "+alumno.Nombre, "Aceptar");
                        return;
                    }
                }
            }
            if (resultado!=0)
            {
                await DisplayAlert("Éxito", "Las notas han sido registradas correctamente.", "Aceptar");
            }
            else
            {
                await DisplayAlert("Error","No se pudo cargar las notas","Aceptar");
            }
            await Navigation.PopAsync();
        }
    }
}