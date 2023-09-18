using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AsistenteEscolar.Data.Models;
using AsistenteEscolar.Views.AlumnosViews;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AsistenteEscolar.Views.AsistenciasViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AsistenciasIndex : ContentPage
    {
        //private List<Asistencia> asistencias;
        //private List<Alumno> alumnos;
        private Materia materia;
        public ObservableCollection<AlumnoAsistencia> AlumnosAsistencia { get; set; }

        public AsistenciasIndex(Materia materia_)
        {
            materia = materia_;
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
            var asistencias = await App.Context.GetAsistenciasByMateriaIdAsync(materia.Id);
            var alumnos = await App.Context.GetAlumnosByMateriaAsync(materia);
            tablaAsistencia.Children.Clear();
            tablaAsistencia.ColumnDefinitions.Clear();
            tablaAsistencia.RowDefinitions.Clear();

            for (int i = 0; i < asistencias.Count()+2; i++)
            {
                tablaAsistencia.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                if (i>1)
                {
                    int currentIndex = i;
                    //tablaAsistencia.Children.Add(new Label { Text = asistencias[i-2].Fecha.ToString("dd/MM/yyyy"), FontSize = 13}, i, 0);
                    //--tablaAsistencia.Children.Add(new Button { Text = asistencias[i - 2].Fecha.ToString("dd/MM/yyyy"), FontSize = 13}, i, 0);
                    tablaAsistencia.Children.Add(new Button
                    {
                        Text = asistencias[currentIndex - 2].Fecha.ToString("dd/MM"),
                        FontSize = 13,
                        Command = new Command(async () =>
                        {
                            // Aquí puedes mostrar un ActionSheet o realizar acciones directamente
                            var action = await DisplayActionSheet("Opciones", "Cancelar", null, "Editar", "Eliminar");

                            if (action == "Editar")
                            {
                                await Navigation.PushAsync(new AsistenciaEdit(asistencias[currentIndex-2]));
                            }
                            else if (action == "Eliminar")
                            {
                                if (await DisplayAlert("Confirmacion", "Estas seguro de eliminar la asistencia de la fecha "+asistencias[currentIndex-2].Fecha.ToString("dd/MM"), "Si", "No"))
                                {
                                    var resultado = await App.Context.DeleteAsistenciaAsync(asistencias[currentIndex-2]);
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
            tablaAsistencia.Children.Add(new Label { 
                Text = "% Asist.",
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center 
            }, 1, 0);
            //tablaAsistencia.Children.Add(new Label { Text = "% Asist." }, 2, 0);
            tablaAsistencia.Children.Add(new Label { Text = "Alumno\\Fecha Asist.",
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment=TextAlignment.Center }, 0, 0);
            for (int i = 0; i < alumnos.Count()+1; i++)
            {
                tablaAsistencia.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                if (i != 0)
                {
                    tablaAsistencia.Children.Add(new Label { Text = alumnos[i-1].Apellido+", "+alumnos[i-1].Nombre}, 0, i);
                }
            }
            for (int i = 1; i < alumnos.Count()+1; i++)
            {
                for (int j = 1; j < asistencias.Count()+2; j++)
                {
                    if (j!=1)
                    {
                        tablaAsistencia.Children.Add(new Label { 
                            Text = alumnos[i-1].AsistenciaAlumnoPorAsistencia(asistencias[j-2]),
                            BackgroundColor= (alumnos[i - 1].AsistenciaAlumnoPorAsistencia(asistencias[j - 2])=="P") ? Color.LawnGreen:Color.FromHex("#ff6e65"),
                            HorizontalTextAlignment = TextAlignment.Center,
                        }, j, i);
                    } else
                    {
                        tablaAsistencia.Children.Add(new Label { 
                            Text = "%"+alumnos[i-1].PorcentajeAsistencias(materia).ToString(),
                            BackgroundColor= (alumnos[i - 1].PorcentajeAsistencias(materia)>=75) ?Color.LawnGreen:Color.FromHex("#ff6e65"),
                            HorizontalTextAlignment=TextAlignment.Center,
                        }, j, i);
                    }

                    /*if (j!=1 || j!=2)
                    {
                        tablaAsistencia.Children.Add(new Label { Text = alumnos[i-1].AsistenciaAlumnoPorAsistencia(asistencias[j-3])}, j, i);
                    } else
                    {
                        if (j==1)
                        {
                            tablaAsistencia.Children.Add(new Label { Text = alumnos[i-1].CantidadAsistencias().ToString()}, j, i);
                        } else if (j==2)
                        {
                            tablaAsistencia.Children.Add(new Label { Text = alumnos[i - 1].PorcentajeAsistencias().ToString() }, j, i);
                        }
                    }*/
                }
            }
        }

        private async void TomarAsistencia_Clicked(object sender, EventArgs e)
        {
            var alumnos = await App.Context.GetAlumnosByCursoIdAsync(materia.CursoId);
            await Navigation.PushAsync(new TomarAsistencia(materia));
        }
    }

    public class AlumnoAsistencia
    {
        public Alumno Alumno { get; set; }
        public List<Asistencia> Asistencias { get; set; }
    }
}
