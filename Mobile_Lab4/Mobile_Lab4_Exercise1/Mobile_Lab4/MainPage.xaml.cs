using Mobile_Lab4.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mobile_Lab4
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            btnFirst.Clicked += (s, e) => Navigation.PushAsync(new FirstPage());
            btnSecond.Clicked += (s, e) => Navigation.PushAsync(new SecondPage());
            btnThird.Clicked += (s, e) => Navigation.PushAsync(new ThirdPage());
            btnFourth.Clicked += (s, e) => Navigation.PushAsync(new FourthPage());
        }
    }
}
