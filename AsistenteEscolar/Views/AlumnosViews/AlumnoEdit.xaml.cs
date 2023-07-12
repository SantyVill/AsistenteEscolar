using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsistenteEscolar.Data.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AsistenteEscolar.Views.AlumnosViews
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AlumnoEdit : ContentPage
	{
		Alumno alumno;
		public AlumnoEdit (Alumno _alumno)
		{
			alumno=_alumno;
			InitializeComponent ();
		}

		private async void Editar_Clicked(object sender, EventArgs e)
        {
            try
            {
                alumno.Nombre = nombre.Text;
                var resultado = await App.Context.UpdateAlumnoAsync(alumno);
                if (resultado == 1)
                {
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "No se pudo guardar la Alumno", "Ok");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Aceptar");
            }
        }

        private async void Cancelar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
	}
}