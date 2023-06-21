using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsistenteEscolar.Data.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AsistenteEscolar.Views.NotasViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotasIndex : ContentPage
    {
        public NotasIndex(Materia materia)
        {
            InitializeComponent();
        }
    }
}