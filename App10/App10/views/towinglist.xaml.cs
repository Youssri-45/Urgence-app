

using Plugin.Messaging;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

using System.Collections.Generic;
using System.Collections;
using App10;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Extensions;

namespace App10.views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class towinglist : ContentPage
    {
        public ObservableCollection<Assurances> Items { get; set; }
        private readonly FloatingActionButtonView fab;
        public towinglist()
        {
            InitializeComponent();
            fab = new FloatingActionButtonView()
            {
                //ImageName = "ambulance.png",
                ColorNormal = Color.FromHex("ff3498db"),
                ColorPressed = Color.Black,
                ColorRipple = Color.FromHex("ff3498db"),
                
                Clicked = async (sender, args) =>
                {
                await  Navigation.PushPopupAsync(new AddPop());



                },
            };
            AbsoluteLayout.SetLayoutFlags(list1, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(list1, new Rectangle(0f, 0f, 1f, 1f));
            absolute.Children.Add(list1);
            AbsoluteLayout.SetLayoutFlags(fab, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(fab, new Rectangle(1f, 1f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
           
            absolute.Children.Add(fab);


            this.Title = "Tap To Call";
            Assurances.Items.Add(new Assurances("name1", "1111"));
            Assurances.Items.Add(new Assurances("name1", "1111"));
            Assurances.Items.Add(new Assurances("name1", "1111"));
            Assurances.Items.Add(new Assurances("name1", "1111"));
            Assurances.Items.Add(new Assurances("name1", "1111"));
            Assurances.Items.Add(new Assurances("name1", "1111"));
            Assurances.Items.Add(new Assurances("name1", "1111"));
            Assurances.Items.Add(new Assurances("name1", "1111"));
            Assurances.Items.Add(new Assurances("name1", "1111"));
            Assurances.Items.Add(new Assurances("name1", "1111"));
            Assurances.Items.Add(new Assurances("name3", "3333"));
            Assurances.Items.Add(new Assurances("name3", "3333"));
            Assurances.Items.Add(new Assurances("name3", "3333"));
            Assurances.Items.Add(new Assurances("name3", "3333"));
            Assurances.Items.Add(new Assurances("name3", "3333"));
            Assurances.Items.Add(new Assurances("name3", "3333"));
            Assurances.Items.Add(new Assurances("name5", "5555"));
            Items = Assurances.Items;

               

            BindingContext = this;
            
        }

        void Handle_ItemTapped(object sender, SelectedItemChangedEventArgs e)
        {
            var s = e.SelectedItem as Assurances;
            var n = s.Number;
            var pc = CrossMessaging.Current.PhoneDialer;
           if (pc.CanMakePhoneCall)
               pc.MakePhoneCall(n, "");

            //if (e.SelectedItem == null)
            //return;

            // await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            // ((ListView)sender).SelectedItem = null;
        }

        //private void Button_Clicked(object sender, EventArgs e)
       // { 
           // var s = list1.SelectedItem as string;
            //var pc = CrossMessaging.Current.PhoneDialer;
           // if (pc.CanMakePhoneCall)
               // pc.MakePhoneCall(s, "");

        //}







        // private void MenuItem_Clicked(object sender, EventArgs e)
        //{
        // var numm = sender as MenuItem;
        //var num = numm.BindingContext as string;
        //  var pc = CrossMessaging.Current.PhoneDialer;
        //if (pc.CanMakePhoneCall)
        //     pc.MakePhoneCall(num, "");

        //}


    }
}