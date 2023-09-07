using AsistenteEscolar.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AsistenteEscolar.Views.CursosViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CursoCreate2 : ContentPage
    {
        private int institucionId;
        public CursoCreate2(int idInstitucion)
        {
            institucionId = idInstitucion;
            InitializeComponent();
            Title = "Nuevo Curso";
        }

        private async void Guardar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var item = new Curso
                {
                    Nombre = nombre.Text,
                    Anio = int.Parse(anio.Text),
                    InstitucionId = institucionId,
                };

                var resultado = await App.Context.InsertCursoAsync(item);
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