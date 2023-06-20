using AsistenteEscolar.Data.Models;
using AsistenteEscolar.Views.CursosViews;
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
    public partial class InstitucionesIndex : ContentPage
    {
        public InstitucionesIndex()
        {
            InitializeComponent();
            LoadItems();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadItems();
        }

        private async void LoadItems()
        {
            var items = await App.Context.GetAllInstitucionesAsync();
            Lista_Instituciones.ItemsSource = items;
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InstitucionCreate());
        }

        private async void Lista_Instituciones_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Institucion institucion = e.Item as Institucion;
            await Navigation.PushAsync(new CursosIndex(institucion));
        }

        private async void btnDelete_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Confirmacion", "Estas seguro de eliminar la institución", "Si", "No"))
            {
                var item = (Institucion)(sender as MenuItem).CommandParameter;
                var resultado = await App.Context.DeleteInstitucionAsync(item);
                if (resultado == 1)
                {
                    LoadItems();
                }
            }
        }
    }
}