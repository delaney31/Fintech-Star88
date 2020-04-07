using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Star8Test
{
    public partial class FirstTestPage : ContentPage
    {
        public FirstTestPage()
        {
            InitializeComponent();
            testButton.Clicked += async (s, e) =>
            {
                await Navigation.PushAsync(new SendMoney());
            };
            NavigationPage.SetBackButtonTitle(this, " ");
        }
    }
}
