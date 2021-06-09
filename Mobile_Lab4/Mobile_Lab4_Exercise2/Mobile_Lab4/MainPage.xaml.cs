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
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();

            this.Children.Add(new FirstPage());
            this.Children.Add(new SecondPage());
            this.Children.Add(new ThirdPage());
            this.Children.Add(new FourthPage());
        }
    }
}
