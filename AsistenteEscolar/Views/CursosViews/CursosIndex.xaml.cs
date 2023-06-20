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
    public partial class CursosIndex : ContentPage
    {
        private int institucionId;
        public CursosIndex(Institucion institucion)
        {
            institucionId = institucion.Id;
            InitializeComponent();
            Title="Cursos de "+institucion.Nombre;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadItems();
        }

        private async void LoadItems()
        {
            var items = await App.Context.GetCursosByInstitucionIdAsync(institucionId);
            Lista_Cursos.ItemsSource = items;
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CursoCreate(institucionId));
        }

        private async void Lista_Cursos_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Curso curso = e.Item as Curso;
            await Navigation.PushAsync(new CursoShow(curso));
        }

        private async void btnDelete_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Confirmacion", "Estas seguro de eliminar la institución", "Si", "No"))
            {
                var item = (Curso)(sender as MenuItem).CommandParameter;
                var resultado = await App.Context.DeleteCursoAsync(item);
                if (resultado == 1)
                {
                    LoadItems();
                }
            }
        }
    }
}