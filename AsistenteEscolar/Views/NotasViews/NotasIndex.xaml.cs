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

            var trimestres = new string[] { "1° Trimestre", "2° Trimestre", "3° Trimestre" };
            foreach (var trimestreNombre in trimestres)
            {
                if (!notas.Any(nota_ => nota_.Nombre == trimestreNombre))
                {
                    // La nota no existe, así que la creamos
                    var nuevaNota = new Nota
                    {
                        Nombre = trimestreNombre,
                        Fecha = DateTime.Now, // Puedes ajustar la fecha si es necesario
                        MateriaId = materia.Id
                    };

                    // Insertar la nueva nota en la base de datos
                    await App.Context.InsertNotaAsync(nuevaNota);
                    notas.Add(nuevaNota); // Agregar la nueva nota a la lista de notas
                }
            }

            for (int i = 0; i < notas.Count() + 1; i++)//Defino la cantidad de columnas
            {
                tablaNota.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                if (i > 0)
                {
                    int currentIndex = i;
                    tablaNota.Children.Add(new Button
                    {
                        Text = notas[currentIndex - 1].Nombre,
                        FontSize = 13,
                        Command = new Command(async () =>
                        {
                            var action = await DisplayActionSheet("Opciones", "Cancelar", null, notas[currentIndex - 1].NotasAlumnos.Count()==0?"Cargar":"Editar" /*,"Eliminar"*/);

                            if (action == "Editar")
                            {
                                await Navigation.PushAsync(new NotasEdit(materia,notas[currentIndex - 1]));
                            } else if (action=="Cargar")
                            {
                                await Navigation.PushAsync(new AgregarNotas(materia, notas[currentIndex - 1]));
                            }
                            /*else if (action == "Eliminar")
                            {
                                if (await DisplayAlert("Confirmacion", "Estas seguro de eliminar la Nota de la fecha " + notas[currentIndex - 1].Nombre, "Si", "No"))
                                {
                                    var resultado = await App.Context.DeleteNotaAsync(notas[currentIndex - 1]);
                                    if (resultado == 1)
                                    {
                                        LoadItems();
                                    }
                                }
                            }*/
                        })
                    }, i, 0);
                }
            }
            tablaNota.Children.Add(new Label
            {
                Text = "Alumno",
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
            int nota;
            for (int i = 1; i < alumnos.Count() + 1; i++)
            {
                for (int j = 0; j < notas.Count(); j++)
                {
                    nota = alumnos[i - 1].NotaDelAlumnoPorNota(notas[j]);
                    tablaNota.Children.Add(new Label
                    {
                        Text = (nota == 0) ? "Pe" : nota+"" ,
                        /*BackgroundColor = (alumnos[i - 1].NotaDelAlumnoPorNota(notas[j - 1]) >= 6) ? Color.LawnGreen : Color.FromHex("#ff6e65"),*/
                        HorizontalTextAlignment = TextAlignment.Center,
                    }, j+1, i);
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
