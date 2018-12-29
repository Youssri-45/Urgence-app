using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Interfaces.Animations;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using Android.Widget;
using Rg.Plugins.Popup.Extensions;
using XamarinForms.SQLite;
using SQLite;
using XamarinForms.SQLite.SQLite;
using App10;
namespace App10.views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPop : PopupPage
    {
        private SQLiteConnection _sqLiteConnection;
        
        public AddPop()
        {
            
            _sqLiteConnection = DependencyService.Get<ISQLite>().GetConnection();
           
            _sqLiteConnection.CreateTable<TodoItem>();
            InitializeComponent();
          
           
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        // Method for animation child in PopupPage
        // Invoced after custom animation end
        protected override Task OnAppearingAnimationEnd()
        {
            return Content.FadeTo(1);
        }

        // Method for animation child in PopupPage
        // Invoked before custom animation begin
        protected override Task OnDisappearingAnimationBegin()
        {
            return Content.FadeTo(0.5);
        }

        protected override bool OnBackButtonPressed()
        {
            // Prevent hide popup
            //return base.OnBackButtonPressed();
            return true;
        }

        // Invoced when background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Return default value - CloseWhenBackgroundIsClicked
            return base.OnBackgroundClicked();
        }

        private async void add_ClickedAsync(object sender, EventArgs e)
        {
            
                 _sqLiteConnection.Insert(new TodoItem
                {

                    Name = ncontact.Text,
                    Number = numcontact.Text,
                });
           
            
            await Navigation.PushAsync(new SQLiteSamplePage().GetSampleContentPage());
            await Navigation.PopPopupAsync();
            var message = "contact added";
            DependencyService.Get<IMessage>().ShortAlert(message);
        }
    }
}