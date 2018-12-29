using System;
using App10;
using App10.views;
using Rg.Plugins.Popup.Pages;
using SQLite;


using Xamarin.Forms;
using XamarinForms.SQLite.SQLite;
using System.Collections.ObjectModel;
using Rg.Plugins.Popup.Extensions;
using Plugin.Messaging;

namespace XamarinForms.SQLite
{
    public class SQLiteSamplePage:ContentPage
    {
        private  SQLiteConnection _sqLiteConnection;
        public ListView listView { get; set; }
        public SQLiteSamplePage()
        {

            _sqLiteConnection = DependencyService.Get<ISQLite>().GetConnection();
            _sqLiteConnection.CreateTable<TodoItem>();
        }

       
        public void Insertitem(string name,string num)
        {
            var newitem=new TodoItem{
                Name=name,
                Number=num
            };
            _sqLiteConnection.Insert(newitem);
           

        }


        // ADD
        //_sqLiteConnection.Insert(new TodoItem
        // {
        //    Name = "aymen",
        //    Number = "1111",
        // });

        // UPDATE
        //_sqLiteConnection.Update(new TodoItem
        //{
        //   ID = 1,
        //   Name = "aymen",
        //   Number = "22222",
        // });

        //_sqLiteConnection.Insert(new TodoItem
        //{
        //  Name = "aymen2",
        //   Number = "5555",
        // });

        // DELETE
        //_sqLiteConnection.Delete<TodoItem>(2);


        /// <summary>
        /// Gets a ContentPage that contains the items saved in the SQLite database.
        /// </summary>
        /// <returns></returns>
        public ContentPage GetSampleContentPage()
        {
            //addButton.Clicked += (s, e) =>
            //{
            //   _sqLiteConnection.Insert(new TodoItem
            //  {
            //      Name = entryname.Text,
            //      Number = entrynum.Text,
            //    });
            //  };

            listView = new ListView
            {

                ItemsSource = _sqLiteConnection.Table<TodoItem>(),
                HasUnevenRows = true,
        };
            var fab = new FloatingActionButtonView()
            {
                //ImageName = "ambulance.png",
                ColorNormal = Color.FromHex("ff3498db"),
                ColorPressed = Color.Aqua,
                ColorRipple = Color.FromHex("ff3498db"),
                ImageName = "add.png",
                HasShadow = true,
           };
            fab.Clicked += async (sender, args) =>
            {
                await Navigation.PushPopupAsync(new AddPop());

                //listView.ItemsSource = _sqLiteConnection.Table<TodoItem>();

            };
            
            var refreshButton = new Button
            {
                Text = "Refresh TodoItems"
            };
            refreshButton.Clicked += (s, e) =>
            {
                listView.ItemsSource = _sqLiteConnection.Table<TodoItem>();
               // _sqLiteConnection.UpdateAll(_sqLiteConnection.Table<TodoItem>());
            };

            listView.ItemTapped += (s, e) =>
            {
                var g = e.Item as TodoItem;
                var n = g.Number;
                var pc = CrossMessaging.Current.PhoneDialer;
                if (pc.CanMakePhoneCall)
                    pc.MakePhoneCall(n, "");
            };
            
            //listView.ItemTemplate=
           
            var vcell = new DataTemplate(() =>
              {
                  var stacklayout = new Grid();

                  var nlabel = new Label();
                  nlabel.SetBinding(Label.TextProperty, "Name");
                  var numlabel = new Label();
                  numlabel.SetBinding(Label.TextProperty, "Number");
                  // var st = new StackLayout();
                  var gr = new Grid();
                  gr.Children.Add(nlabel,0,0);
                  gr.Children.Add(numlabel,0,1);
                  //bdl.CommandParameter=
                  //bdl.BindingContext = stacklayout;
                  //bdl.SetBinding(Button.TextProperty,x);
                  // bdl.SetBinding( bdl.CommandParameter, new Binding("."));
                  //var del = new Command((object item) => {
                  // var myItem = item as TodoItem;
                  //_sqLiteConnection.Delete<TodoItem>(myItem.ID);
                  //listView.ItemsSource = _sqLiteConnection.Table<TodoItem>();
                  // });

                  var bdl = new Button
                  {                      
                      Text = "X",
                      HorizontalOptions = LayoutOptions.Center,
                      VerticalOptions = LayoutOptions.Center,
                      TextColor = Color.Red,
                      BackgroundColor = Color.Transparent,
                      BorderWidth = 0,
                      
                     // WidthRequest = 40,
                     // HeightRequest = 40,
                  };
                  
                  //bdl.CommandParameter = gr;
                  bdl.Clicked += async ( s, e) =>
                  {
                     var button=  s as Button;
                      var myItem = button.BindingContext as TodoItem;
                     // var myItem = listView.ItemSelected as TodoItem;
                      // (TodoItem)((Button)s).CommandParameter;
                     
                     // listView.ItemsSource = _sqLiteConnection.Table<TodoItem>();
                      var d=await DisplayAlert("alert", myItem.Name+" will be deleted from your emergency contacts", "delete","cancel");
                      if (d == true)
                      {
                          _sqLiteConnection.Delete<TodoItem>(myItem.ID);
                          listView.ItemsSource = _sqLiteConnection.Table<TodoItem>();
                          var message = "contact deleted";
                          DependencyService.Get<IMessage>().ShortAlert(message);
                      }
                  };
                  
                  //st.Children.Add(bdl);
                  // stacklayout.Children.Add(nlabel);
                  stacklayout.Children.Add(gr,0,0);
                  stacklayout.Children.Add(bdl,4,0);
                 
                  return new ViewCell { View = stacklayout };
              });
          
           
            
            listView.ItemTemplate = vcell;
            var absolute = new AbsoluteLayout();
            
            AbsoluteLayout.SetLayoutFlags(listView, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(listView, new Rectangle(0f, 0f, 1f, 1f));
           absolute.Children.Add(listView);
            AbsoluteLayout.SetLayoutFlags(fab, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(fab, new Rectangle(1f, 1f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
           absolute.Children.Add(fab);


            var PopupPage = new ContentPage();
            PopupPage.Title = "emergency contacts";
            var navdelete = new ToolbarItem();
            navdelete.Order =ToolbarItemOrder.Secondary;
            navdelete.Text = "delete all";
            navdelete.Clicked +=(s,e)=>{ _sqLiteConnection.DeleteAll<TodoItem>(); listView.ItemsSource = _sqLiteConnection.Table<TodoItem>(); };
            var navbutton = new ToolbarItem();
            navbutton.Order = ToolbarItemOrder.Secondary;
            navbutton.Text = "refresh";
            navbutton.Clicked  +=(s,e) => { listView.ItemsSource = _sqLiteConnection.Table<TodoItem>(); };
            PopupPage.Content = absolute;
            PopupPage.ToolbarItems.Add(navbutton);
            PopupPage.ToolbarItems.Add(navdelete);
            //  Content = new AbsoluteLayout
            //  {

            //Children =
            // {      fab,                                                             

            //   listView,
            //}
            //  }

            return PopupPage;
        }
    }
}
