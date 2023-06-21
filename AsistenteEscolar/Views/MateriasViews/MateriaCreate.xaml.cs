using AsistenteEscolar.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AsistenteEscolar.Views.MateriasViews
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MateriaCreate : ContentPage
	{
		private Curso curso;
		public MateriaCreate (Curso curso_)
		{
			curso=curso_;
			InitializeComponent ();
		}

        private async void Guardar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var item = new Materia
                {
                    Nombre = nombre.Text,
                    CursoId = curso.Id,
                };

                var resultado = await App.Context.InsertMateriaAsync(item);
                if (resultado == 1)
                {
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "No se pudo guardar la Curso", "Ok");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Aceptar");
            }
        }
    }
}