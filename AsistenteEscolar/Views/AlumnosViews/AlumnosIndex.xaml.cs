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
        public AlumnosIndex(Curso curso)
        {
            InitializeComponent();
        }
    }
}