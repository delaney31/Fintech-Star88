using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Star8Test
{
    public partial class SendMoney : ContentPage
    {
        public SendMoney()
        {
            InitializeComponent();
            ToolbarItems.Add(new ToolbarItem("Cancel", "", async () =>
            {
                await Navigation.PopAsync();
            }));

        }
    }
}
