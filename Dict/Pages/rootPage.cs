using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace Dict.Pages
{

    public class RootPage : MasterDetailPage
    {
        public RootPage()
        {

            Detail = new NavigationPage(new MainPage());

            Master = new Pages.MasterPage();
            this.Title = "Welcome to Dict";
        }
    }
}
