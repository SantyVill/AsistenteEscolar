using AsistenteEscolar.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AsistenteEscolar.Views.InstitucionesViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InstitucionShow : TabbedPage
    {
        public InstitucionShow(Institucion institucion)
        {
            InitializeComponent();
        }
    }
}