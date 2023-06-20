using AsistenteEscolar.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AsistenteEscolar.Views.CursosViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CursosIndex : ContentPage
    {
        private int institucionId;
        public CursosIndex(Institucion institucion)
        {
            institucionId = institucion.Id;
            InitializeComponent();
        }
    }
}