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
    public partial class AlumnosIndex : ContentPage
    {
        private Curso curso;
        public AlumnosIndex(Curso curso_)
        {
            curso = curso_;
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
            var items = await App.Context.GetAlumnosByCursoIdAsync(curso.Id);
            Lista_Alumnos.ItemsSource = items;
        }

        /* private async void Lista_Alumnos_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new AlumnosIndex(curso));
        } */

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AlumnoCreate2(curso));
        }

        private async void btnDelete_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Confirmacion", "Estas seguro de eliminar la institución", "Si", "No"))
            {
                var item = (Alumno)(sender as MenuItem).CommandParameter;
                var resultado = await App.Context.DeleteAlumnoAsync(item);
                if (resultado == 1)
                {
                    LoadItems();
                }
            }
        }

        private async void btnEdit_Clicked(object sender, EventArgs e)
        {
            var alumno = (Alumno)(sender as MenuItem).CommandParameter;
            await Navigation.PushAsync(new AlumnoEdit(alumno));
        }
    }
}