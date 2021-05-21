using Mobile_Lab3.DataContracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mobile_Lab3
{
    public partial class MainPage : ContentPage
    {

        private readonly IPeopleClient client;
        Person person = new Person();
        public MainPage(IPeopleClient client)
        {
            InitializeComponent();

            
            entryFirstName.TextChanged += entryFirstName_TextChanged;
            entryLastName.TextChanged += entryLastName_TextChanged;
            entryPhoneNumber.TextChanged += entryPhoneNumber_TextChanged;
            btnPhoto.Clicked += btnPhoto_Clicked;
            btnSave.Clicked += BtnSave_Clicked;
            this.client = client;
        }

        private async void BtnSave_Clicked(object sender, EventArgs e)
        {
            if (!Validate())
            {
                await DisplayAlert("Validation Error", "First name, last name, phone number and picture are required.", "Ok");
                return;
            }

            try
            {
                await client.AddPersonAsync(person);
                await DisplayAlert("Success", "Data has been saved.", "Ok");
                Clear();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Ok");
            }
        }

        private void Clear()
        {
            entryFirstName.Text = string.Empty;
            entryLastName.Text = string.Empty;
            entryPhoneNumber.Text = string.Empty;
            imgPhoto.Source = null;
            person = new Person();
        }

        private void entryPhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            person.PhoneNumber = e.NewTextValue;
        }

        private void entryLastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            person.LastName = e.NewTextValue;
        }

        private void entryFirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            person.FirstName = e.NewTextValue;
        }

        private async void btnPhoto_Clicked(object sender, EventArgs e)
        {
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            { 
            });

            if (photo == null)
                return;

            imgPhoto.Source = ImageSource.FromStream(() => photo.GetStream());
            byte[] bytes;
            using (var memoryStream = new MemoryStream())
            {
                photo.GetStream().CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }

            string base64 = Convert.ToBase64String(bytes);
            person.PictureBase64 = base64;
        }

        private bool Validate()
        {
            return !(string.IsNullOrWhiteSpace(person.FirstName) ||
                    string.IsNullOrWhiteSpace(person.LastName) ||
                    string.IsNullOrWhiteSpace(person.PhoneNumber) ||
                    string.IsNullOrWhiteSpace(person.PictureBase64)
                );
        }
    }
}
