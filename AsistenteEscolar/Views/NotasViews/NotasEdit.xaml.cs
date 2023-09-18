﻿using AsistenteEscolar.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AsistenteEscolar.Views.NotasViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotasEdit : ContentPage
    {
        private Materia materia;
        private List<Alumno> alumnos;
        private Nota nota;

        public NotasEdit(Materia materia_, Nota nota_)
        {
            materia = materia_;
            nota = nota_;
            InitializeComponent();

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await CargarItems();
        }

        private async Task CargarItems()
        {
            await CargarAlumnosAsync();
        }

        private async Task CargarAlumnosAsync()
        {
            alumnos = await App.Context.GetAlumnosByMateriaAsync(materia);

            foreach (var alumno in alumnos)
            {
                NotaAlumno2 notaAlumno = alumno.NotaAlumnoPorNota(nota);
                if (notaAlumno == null)
                {
                    notaAlumno = new NotaAlumno2
                    {
                        AlumnoId = alumno.Id,
                        NotaId = nota.Id,
                        Nota = 0
                    };
                    alumno.notas.Add(notaAlumno);
                    await App.Context.InsertNotaAlumnoAsync(notaAlumno);
                }
                var label = new Label
                {
                    Text = alumno.Apellido + ", " + alumno.Nombre,
                    Margin = new Thickness(10, 0)
                };
                var entry = new Entry
                {
                    BindingContext = alumno,
                    Text=alumno.NotaAlumnoPorNota(nota).Nota.ToString(),
                    Margin = new Thickness(0, 5),
                    Keyboard = Keyboard.Numeric,
                    MaxLength = 2,
                    HorizontalOptions = LayoutOptions.Fill,
                    WidthRequest = 40,
                };

                var stackLayout = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Children = { entry, label }
                };

                AlumnosStackLayout.Children.Add(stackLayout);
            }
        }


        private async void GuardarNota_Clicked(object sender, EventArgs e)
        {
            int resultado = 0;
            foreach (var stackLayout in AlumnosStackLayout.Children)
            {
                var entry = ((StackLayout)stackLayout).Children[0] as Entry;
                var alumno = entry.BindingContext as Alumno;
                if (entry != null && !string.IsNullOrWhiteSpace(entry.Text))
                {
                    // Parsear la nota ingresada (asumiendo que las notas son números válidos)
                    if (float.TryParse(entry.Text, out float notaValue) && (int)notaValue<=10 && (int)notaValue>=0)
                    {
                        var notaAlumno = new NotaAlumno2
                        {
                            AlumnoId = alumno.Id,
                            NotaId = nota.Id,
                            Nota = (int)notaValue,
                            Id = alumno.NotaAlumnoPorNota(nota).Id
                        };

                        // Guardar el registro de nota en la base de datos
                        resultado = await App.Context.UpdateNotaAlumnoAsync(notaAlumno);
                        if (resultado==01)
                        {
                            await App.Context.InsertNotaAlumnoAsync(notaAlumno);
                        }
                        
                    }
                    else
                    {
                        await DisplayAlert("Error", "Por favor, ingrese una nota válida para " + alumno.Apellido + ", " + alumno.Nombre, "Aceptar");
                        return;
                    }
                }
            }
            if (resultado != 0)
            {
                await DisplayAlert("Éxito", "Las notas han sido editadas correctamente.", "Aceptar");
            }
            else
            {
                await DisplayAlert("Error", "No se pudo editar las notas", "Aceptar");
            }
            await Navigation.PopAsync();
        }
    }
}