using Plugin.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Plugin.Geolocator.Abstractions;

using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using Plugin.Geolocator;


using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using Xam.Plugin.DeviceManager;
using Plugin.Connectivity;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Android.Net.Wifi;
using Android.Widget;
using Android.Locations;
using Android.Content;
using Plugin.Permissions;
using Android.Content.PM;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using Plugin.Permissions.Abstractions;
using XamarinForms.SQLite;
using System.IO;


namespace App10.views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        string latitude;
        string longitude;
        double latdouble;
        double longdouble;
        // public List<Places> lh = new List<Places>();
        public ObservableCollection<Places> lh = new ObservableCollection<Places>();
        public ObservableCollection<Places> lp = new ObservableCollection<Places>();
        // public List<Places> lp = new List<Places>();
        int i = 0;
        int j = 0;

        public Page1()
        {
            string h;
            var assetManager = Android.App.Application.Context.Assets;
            using (var streamReader = new StreamReader(assetManager.Open("HTMLPage1.html")))
            {
                var html = streamReader.ReadToEnd();
                h = html;
            }
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            var htmlSource = new HtmlWebViewSource();
            htmlSource.Html = @h;
            //web.Source = htmlSource;
           
        }
       

        private void Button_Clicked(object sender, EventArgs e)
        {
            var pc = CrossMessaging.Current.PhoneDialer;
            if (pc.CanMakePhoneCall)
                pc.MakePhoneCall("197", "");

        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            var pc = CrossMessaging.Current.PhoneDialer;
            if (pc.CanMakePhoneCall)
                pc.MakePhoneCall("190", "");
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            
            var pc = CrossMessaging.Current.PhoneDialer;
            if (pc.CanMakePhoneCall)
                pc.MakePhoneCall("198", "");
        }
        private async void Alertcon()
        {
            var result = await DisplayAlert("Alert", "no internet connection", "connect","cancel");
            if (result)
            {
                var wifimanager = Android.App.Application.Context.GetSystemService(Android.App.Application.WifiService) as WifiManager;

                wifimanager.SetWifiEnabled(true);
            }

        }

        private async void Alertloc()
        {
            await DisplayAlert("Alert", "check location settings", "ok");


            //if ((int)Android.OS.Build.VERSION.SdkInt < 24)

            //else
            // await CrossPermissions.Current.RequestPermissionsAsync(new[] { Plugin.Permissions.Abstractions.Permission.Location });

            // await  CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync( Plugin.Permissions.Abstractions.Permission.Location);
            // if (result)
            //{
            
            //  Intent gpsSettingIntent = new Intent();
            //Android.App.Application.Context.StartActivity(gpsSettingIntent);
            // var locationmanager = Android.App.Application.Context.GetSystemService(Android.App.Application.LocationService) as LocationManager;
            //locationmanager.IsProviderEnabled = true;
            // }
            // {
            // var locmanager = Android.App.Application.Context.GetSystemService(Android.App.Application.LocationService) as LocationManager;
            //  Intent intent = new Intent(Android.Provider.Settings.ActionSecuritySettings);

            //  Android.App.Application.Context.StartService(intent);
            //  }
        }
        private async void Button_Clicked_3(object sender, EventArgs e)
        {
                
            if (CrossConnectivity.Current.IsConnected == false)
            {
               Alertcon();                                  
            }
            else if ((CrossGeolocator.Current.IsGeolocationEnabled ==false)&&(CrossConnectivity.Current.IsConnected ==true))
            {
                Alertloc();
            }
            else if((CrossGeolocator.Current.IsGeolocationEnabled == true) &&(CrossConnectivity.Current.IsConnected == true))
            {
                act.IsVisible = true;
                act.IsRunning = true;
                await RetlocationAsync();
                act.IsVisible = false;
               
            }

            

        }

       private async Task RetlocationAsync()
        {
            var resurl = "https://www.googleapis.com/geolocation/v1/geolocate?key=AIzaSyC0aGVoknSa3xlfHKL4LTKdZbVZvww9mgQ";
            var uri = new Uri(resurl);
            var client = new HttpClient();
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                JObject rss = JObject.Parse(content);
                var newlat = rss["location"]["lat"].ToString();
                var newlong = rss["location"]["long"].ToString();
                Xamarin.Forms.Maps.Position newpos = new Xamarin.Forms.Maps.Position(Convert.ToDouble(newlat), Convert.ToDouble(newlong));
                var newgeo = new Xamarin.Forms.Maps.Geocoder();
                var newaddress = await newgeo.GetAddressesForPositionAsync(newpos);
                //newadr.Text = newaddress.ElementAt(0);
            }
           var locator = CrossGeolocator.Current;

            locator.DesiredAccuracy = 20;
            var position = await locator.GetPositionAsync();
            var geocoder = new Xamarin.Forms.Maps.Geocoder();
            Xamarin.Forms.Maps.Position pos = new Xamarin.Forms.Maps.Position(position.Latitude,position.Longitude);
            latdouble = position.Latitude;
            longdouble = position.Longitude;


            
            
           var address = await geocoder.GetAddressesForPositionAsync(pos);
            latitude = position.Latitude.ToString();
            

            longitude = position.Longitude.ToString();
           
            address1.Text = address.ElementAt(0);
            loc.IsVisible = true;
            ShowOnMap.IsVisible = true;
            hosb.IsEnabled = true;
            hosb.BackgroundColor = Color.LightSeaGreen;
            //hosb.BackgroundColor= Xamarin.Forms.Color.GreenYellow;
            
            polb.IsEnabled = true;
            polb.BackgroundColor = Color.LightSeaGreen;
            //polb.BackgroundColor = Xamarin.Forms.Color.GreenYellow;

            // var address = await geoCoder.GetAddressesForPositionAsync(position1);
            //foreach (var add in address)
            //addresslabel.Text += add + "\n";



        }

        private async void Button_Clicked_4Async(object sender, EventArgs e)
        {
            
            var RestUrl = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + latitude + "," + longitude + "&rankby=distance&type=hospital&keyword=hopital|clinic|emergency|clinical|clinique|urgence|مستشفي |عيادة|مصحة&key=AIzaSyC0aGVoknSa3xlfHKL4LTKdZbVZvww9mgQ";
            var uri = new Uri(RestUrl);
            var client = new HttpClient();
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                JObject rss = JObject.Parse(content);
                //string n = rss["results"][0]["name"].ToString();
                // Response response = JsonConvert.DeserializeObject<Response>(content));


                // ObservableCollection<string> hosList = new ObservableCollection<string>();
                
                while  (i < 10)
                {
                    
                    var place = new Places(rss["results"][i]["name"].ToString(),rss["results"][i]["geometry"]["location"]["lat"].ToString(),rss["results"][i]["geometry"]["location"]["lng"].ToString());
                    var geocoder = new Xamarin.Forms.Maps.Geocoder();
                    var pos = new Xamarin.Forms.Maps.Position(Convert.ToDouble(place.Latitude), Convert.ToDouble(place.Longitude));
                    var addr = await geocoder.GetAddressesForPositionAsync(pos);
                    place.Address = addr.ElementAt(0);
                    lh.Add(place);
                    i++;
                }
               // for (int i = 0; i < 10; i++)
                //{
                 //   var place = new places(rss["results"][i]["name"].ToString(), rss["results"][i]["geometry"]["location"]["lat"].ToString(), rss["results"][i]["geometry"]["location"]["lng"].ToString());
                   // lh.Add(place);
                //}
               

            }
             await Navigation.PushAsync(new hospitallist(lh,latitude,longitude));
        }

        private async void Button_Clicked_5Async(object sender, EventArgs e)
        {
            

            var RestUrl = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + latitude + "," + longitude + "&rankby=distance&type=police&key=AIzaSyC0aGVoknSa3xlfHKL4LTKdZbVZvww9mgQ";
            var uri = new Uri(RestUrl);
            var client = new HttpClient();
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                JObject rss = JObject.Parse(content);
                //string n = rss["results"][0]["name"].ToString();
                // Response response = JsonConvert.DeserializeObject<Response>(content));


                // ObservableCollection<string> hosList = new ObservableCollection<string>();
                
               while(j<10)
                {
                    var place = new Places(rss["results"][j]["name"].ToString(),rss["results"][j]["geometry"]["location"]["lat"].ToString(), rss["results"][j]["geometry"]["location"]["lng"].ToString() );
                    var geocoder = new Xamarin.Forms.Maps.Geocoder();
                    var pos = new Xamarin.Forms.Maps.Position(Convert.ToDouble(place.Latitude), Convert.ToDouble(place.Longitude));
                    var addr = await geocoder.GetAddressesForPositionAsync(pos);
                    place.Address = addr.ElementAt(0);
                    lp.Add(place);
                    j++;
                }


            }
             await Navigation.PushAsync(new policelist(lp,latitude,longitude));
        }

        private void Button_Clicked_6(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SQLiteSamplePage().GetSampleContentPage());
        }

        private void ShowOnMap_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TabbedPage1(latdouble, longdouble));
        }
    }

   

}