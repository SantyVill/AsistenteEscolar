using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsistenteEscolar.Data.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AsistenteEscolar.Views.MateriasViews
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MateriaEdit : ContentPage
	{
		private Materia materia;
		public MateriaEdit (Materia _materia)
		{
			materia=_materia;
			InitializeComponent ();
		}

		private async void Editar_Clicked(object sender, EventArgs e)
        {
            try
            {
                materia.Nombre = nombre.Text;
                var resultado = await App.Context.UpdateMateriaAsync(materia);
                if (resultado == 1)
                {
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "No se pudo guardar la Materia", "Ok");
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