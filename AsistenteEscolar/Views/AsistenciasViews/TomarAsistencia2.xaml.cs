using AsistenteEscolar.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AsistenteEscolar.Views.AsistenciasViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TomarAsistencia2 : ContentPage
    {
        private List<Alumno> alumnos;
        private int currentIndex;
        private List<NullableBool> asistenciaList;

        public TomarAsistencia2(Materia materia)
        {
            alumnos = App.Context.GetAlumnosByCursoIdAsync(materia.CursoId).Result;
            InitializeComponent();
            currentIndex = 0;
            asistenciaList = new List<NullableBool>(alumnos.Count);
            foreach (var alumno in alumnos)
            {
                asistenciaList.Add(NullableBool.Unknown);
            }
            LoadAlumno();
        }

        private void LoadAlumno()
        {
            if (currentIndex < alumnos.Count)
            {
                var alumno = alumnos[currentIndex];
                AlumnoLabel.Text = alumno.Apellido + ", " + alumno.Nombre;
                PresenteRadioButton.IsChecked = asistenciaList[currentIndex] == NullableBool.True;
                AusenteRadioButton.IsChecked = asistenciaList[currentIndex] == NullableBool.False;
            }
        }

        private void NextButton_Clicked(object sender, EventArgs e)
        {
            if (currentIndex < alumnos.Count)
            {
                var alumno = alumnos[currentIndex];
                if (PresenteRadioButton.IsChecked)
                {
                    asistenciaList[currentIndex] = NullableBool.True;
                }
                else if (AusenteRadioButton.IsChecked)
                {
                    asistenciaList[currentIndex] = NullableBool.False;
                }
                currentIndex++;
                LoadAlumno();
            }
        }

        private async void GuardarAsistenciaButton_Clicked(object sender, EventArgs e)
        {
            var asistenciaAlumnoList = new List<AsistenciaAlumno>();
            for (int i = 0; i < alumnos.Count; i++)
            {
                var alumno = alumnos[i];
                var asistencia = asistenciaList[i];
                if (asistencia != NullableBool.Unknown)
                {
                    var asistenciaAlumno = new AsistenciaAlumno
                    {
                        AlumnoId = alumno.Id,
                        Asistio = asistencia == NullableBool.True
                    };
                    asistenciaAlumnoList.Add(asistenciaAlumno);
                }
            }

            // Guardar los registros de asistencia en la base de datos
            foreach (var asistenciaAlumno in asistenciaAlumnoList)
            {
                await App.Context.InsertAsistenciaAlumnoAsync(asistenciaAlumno);
            }

            await DisplayAlert("Éxito", "La asistencia ha sido registrada correctamente.", "Aceptar");
        }
    }

    public enum NullableBool
    {
        True,
        False,
        Unknown
    }
}
