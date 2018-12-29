using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App10.views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class policelist : ContentPage
    {
        public string latitude;
        public string longitude;
        public ObservableCollection<Places> Items { get; set; }

        public policelist(ObservableCollection<Places>x,string la,string lg)
        {
            InitializeComponent();
            this.Title = "Nearest Police Stations";
            Items = x;
            latitude = la;
            longitude = lg;
            BindingContext = this;
        }

         void Handle_ItemTapped(object sender, SelectedItemChangedEventArgs e)
        {
            var nn = e.SelectedItem as Places;
            var lng = nn.Longitude;
            var lat = nn.Latitude;
            Uri uri = new Uri("http://maps.google.com/maps?saddr=" + latitude + "," + longitude + "&daddr=" + lat + "," + lng);
            Device.OpenUri(uri);
            // if (e.SelectedItem == null)
            //   return;

            //await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            //((ListView)sender).SelectedItem = null;
        }
    }
}