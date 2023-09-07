using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsistenteEscolar.Data.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AsistenteEscolar.Views.CursosViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CursoEdit : ContentPage
    {
        private Curso curso;
        public CursoEdit(Curso _curso)
        {
            curso = _curso;
            InitializeComponent();
            LoadItems();
        }
        
        private void LoadItems()
        {
            nombre.Text = curso.Nombre;
            anio.Text = curso.Anio.ToString();
        }
        private async void Editar_Clicked(object sender, EventArgs e)
        {
            try
            {
                curso.Nombre = nombre.Text;
                curso.Anio = int.Parse(anio.Text);
                var resultado = await App.Context.UpdateCursoAsync(curso);
                if (resultado == 1)
                {
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "No se pudo editar la Curso", "Ok");
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