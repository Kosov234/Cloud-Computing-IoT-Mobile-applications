using Mobile_Lab3.DataContracts;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile_Lab3
{
    public partial class App : Application
    {
        private const string API_URL = "http://192.168.0.16:5000/api";
        public App()
        {
            var client = RestEase.RestClient.For<IPeopleClient>(API_URL);

            InitializeComponent();
            

            MainPage = new MainPage(client);
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
