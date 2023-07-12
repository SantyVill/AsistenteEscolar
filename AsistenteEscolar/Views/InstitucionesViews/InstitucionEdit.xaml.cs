using AsistenteEscolar.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AsistenteEscolar.Views.InstitucionesViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InstitucionEdit : ContentPage
    {
        private Institucion institucion;
        public InstitucionEdit(Institucion _institucion)
        {
            institucion = _institucion;
            InitializeComponent();
            nombre.Text =institucion.Nombre;
        }

        private async void Editar_Clicked(object sender, EventArgs e)
        {
            try
            {
                institucion.Nombre = nombre.Text;
                var resultado = await App.Context.UpdateInstitucionAsync(institucion);
                if (resultado == 1)
                {
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "No se pudo guardar la Institucion", "Ok");
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