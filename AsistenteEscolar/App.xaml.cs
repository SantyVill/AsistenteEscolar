using AsistenteEscolar.Data;
using AsistenteEscolar.Views.InstitucionesViews;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AsistenteEscolar
{
    public partial class App : Application
    {
        public static DataBaseContext Context { get; set; }
        public App()
        {
            InitializeComponent();
            InitializeDataBase();
            MainPage = new NavigationPage(new InstitucionesIndex());
        }

        private void InitializeDataBase()
        {
            var folderApp = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var dbPath = System.IO.Path.Combine(folderApp,"AsistenteEscolar.db3");
            Context = new DataBaseContext(dbPath);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
