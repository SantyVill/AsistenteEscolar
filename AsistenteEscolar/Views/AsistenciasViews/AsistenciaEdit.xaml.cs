using AsistenteEscolar.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AsistenteEscolar.Views.AsistenciasViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AsistenciaEdit : ContentPage
    {
        private Asistencia asistencia;
        private Alumno alumno;
        //private List<NullableBool> asistenciaList;


        public AsistenciaEdit(Asistencia asistencia_)
        {
            asistencia = asistencia_;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadItems();
        }

        private void LoadItems(){
            CargarListaAsync();
        }

        private async void CargarListaAsync()
        {
            foreach (var item in asistencia.asistenciasAlumnos)
            {
                alumno = await App.Context.GetAlumnoByIdAsync(item.AlumnoId);
                var switchControl = new Switch
                {
                    BindingContext = item,
                    Margin = new Thickness(0, 5),
                    IsToggled = item.Asistio
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
            var asistenciaAlumno = switchControl.BindingContext as AsistenciaAlumno;
            var presente = switchControl.IsToggled;

            var item = new AsistenciaAlumno
            {
                AlumnoId = asistenciaAlumno.AlumnoId,
                AsistenciaId = asistencia.Id,
                Asistio = presente
            };

            //await App.Context.InsertAsistenciaAlumnoAsync(item);
        }

        private async void GuardarAsistencia_Clicked(object sender, EventArgs e)
        {
            // Iterar sobre los controles de asistencia y guardar los registros
            foreach (var stackLayout in AlumnosStackLayout.Children)
            {
                var switchControl = ((StackLayout)stackLayout).Children[0] as Switch;
                var asistenciaAlumno = switchControl.BindingContext as AsistenciaAlumno;
                var presente = switchControl.IsToggled;

                asistenciaAlumno.Asistio = presente;

                var item = new AsistenciaAlumno
                {
                    AlumnoId = asistenciaAlumno.AlumnoId,
                    AsistenciaId = asistencia.Id,
                    Asistio = presente
                };

                // Guardar el registro de asistencia en la tabla AsistenciaAlumno
                await App.Context.UpdateAsistenciaAlumnoAsync(asistenciaAlumno);
            }

            await DisplayAlert("Éxito", "La asistencia se ha editado correctamente.", "Aceptar");
            await Navigation.PopAsync();
        }
    } 
}