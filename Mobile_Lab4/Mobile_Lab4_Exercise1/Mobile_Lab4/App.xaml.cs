using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile_Lab4
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Mobile_Lab4.MainPage());
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
