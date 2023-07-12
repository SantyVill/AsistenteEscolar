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
    public partial class MateriasIndex : ContentPage
    {
        private Curso curso;
        public MateriasIndex(Curso curso_)
        {
            curso=curso_;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadItems();
        }

        private async void LoadItems()
        {
            var items = await App.Context.GetMateriasByCursoIdAsync(curso.Id);
            Lista_Materias.ItemsSource = items;
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MateriaCreate2(curso));
        }

        private async void Lista_Materias_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Materia materia = e.Item as Materia;
            await Navigation.PushAsync(new MateriaShow(materia));
        }

        private async void btnDelete_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Confirmacion", "Estas seguro de eliminar la materia", "Si", "No"))
            {
                var item = (Materia)(sender as MenuItem).CommandParameter;
                var resultado = await App.Context.DeleteMateriaAsync(item);
                if (resultado == 1)
                {
                    LoadItems();
                }
            }
        }

        private async void btnEdit_Clicked(object sender, EventArgs e)
        {
            var materia = (Materia)(sender as MenuItem).CommandParameter;
            await Navigation.PushAsync(new MateriaEdit(materia));
        }
    }
}