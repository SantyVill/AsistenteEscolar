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
        public MateriasIndex(Curso curso)
        {
            InitializeComponent();
        }
    }
}