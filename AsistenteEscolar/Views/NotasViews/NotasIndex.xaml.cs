using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsistenteEscolar.Data.Models;
using AsistenteEscolar.Views.AsistenciasViews;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AsistenteEscolar.Views.NotasViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotasIndex : ContentPage
    {
        private Materia materia;
        public NotasIndex(Materia _materia)
        {
            materia = _materia;
            InitializeComponent();
            BindingContext = this;
        }

        protected override void OnAppearing()
        {
            LoadItems();
            base.OnAppearing();
        }

        private async void LoadItems()
        {
            var notas = await App.Context.GetNotasByMateriaIdAsync(materia.Id);
            var alumnos = await App.Context.GetAlumnosByMateriaAsync(materia);
            tablaNota.Children.Clear();
            tablaNota.ColumnDefinitions.Clear();
            tablaNota.RowDefinitions.Clear();

            for (int i = 0; i < notas.Count() + 2; i++)
            {
                tablaNota.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                if (i > 1)
                {
                    int currentIndex = i;
                    tablaNota.Children.Add(new Button
                    {
                        Text = notas[currentIndex - 2].Nombre,
                        FontSize = 13,
                        Command = new Command(async () =>
                        {
                            var action = await DisplayActionSheet("Opciones", "Cancelar", null, "Editar", "Eliminar");

                            if (action == "Editar")
                            {
                                await Navigation.PushAsync(new NotasEdit(notas[currentIndex - 2]));
                            }
                            else if (action == "Eliminar")
                            {
                                if (await DisplayAlert("Confirmacion", "Estas seguro de eliminar la Nota de la fecha " + notas[currentIndex - 2].Nombre, "Si", "No"))
                                {
                                    var resultado = await App.Context.DeleteNotaAsync(notas[currentIndex - 2]);
                                    if (resultado == 1)
                                    {
                                        LoadItems();
                                    }
                                }
                            }
                        })
                    }, i, 0);
                }
            }
            tablaNota.Children.Add(new Label
            {
                Text = "% Asist.",
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            }, 1, 0);
            tablaNota.Children.Add(new Label
            {
                Text = "Alumno\\Fecha Asist.",
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            }, 0, 0);
            for (int i = 0; i < alumnos.Count() + 1; i++)
            {
                tablaNota.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                if (i != 0)
                {
                    tablaNota.Children.Add(new Label { Text = alumnos[i - 1].Apellido + ", " + alumnos[i - 1].Nombre }, 0, i);
                }
            }
            for (int i = 1; i < alumnos.Count() + 1; i++)
            {
                for (int j = 1; j < notas.Count() + 2; j++)
                {
                    if (j != 1)
                    {
                        tablaNota.Children.Add(new Label
                        {
                            Text = (alumnos[i - 1].NotaDelAlumnoPorNota(notas[j - 2]).Nota>=6)?"Aprobado":"Reprobado",
                            BackgroundColor = (alumnos[i - 1].NotaDelAlumnoPorNota(notas[j - 2]).Nota >= 6) ? Color.LawnGreen : Color.FromHex("#ff6e65"),
                            HorizontalTextAlignment = TextAlignment.Center,
                        }, j, i);
                    }
                    else
                    {
                        tablaNota.Children.Add(new Label
                        {
                            //Text = "%" + alumnos[i - 1].PorcentajeNotas().ToString(),
                           // BackgroundColor = (alumnos[i - 1].PorcentajeNotas() >= 75) ? Color.LawnGreen : Color.FromHex("#ff6e65"),
                            //HorizontalTextAlignment = TextAlignment.Center,
                        }, j, i);
                    }
                }
            }
        }

        private async void TomarNota_Clicked(object sender, EventArgs e)
        {
            var alumnos = await App.Context.GetAlumnosByCursoIdAsync(materia.CursoId);
            //await Navigation.PushAsync(new TomarNota(materia));
        }
    }

    public class AlumnoNota
    {
        public Alumno Alumno { get; set; }
        public List<Nota> Asistencias { get; set; }
    }
}
