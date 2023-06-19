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
    public partial class InstitucionCreate : ContentPage
    {
        public InstitucionCreate()
        {
            InitializeComponent();
        }

        private async void Guardar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var item = new Institucion
                {
                    Nombre = nombre.Text,
                };

                var resultado = await App.Context.InsertInstitucionAsync(item);
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
    }
}