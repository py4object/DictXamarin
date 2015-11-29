using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace Dict.Pages
{
    public class WaitPage : ContentPage
    {
        public WaitPage(string title)
        {
            Content = new StackLayout
            {
                HorizontalOptions=LayoutOptions.CenterAndExpand,
                VerticalOptions=LayoutOptions.CenterAndExpand,
                Children = {
					new ActivityIndicator{
                        IsRunning=true,
                        IsVisible=true,
                        WidthRequest=50,
                        HeightRequest=50
                    },
                    new Label{
                        Text="Pleas wait this might take awhile"
}
				}
                
            };
            
            
        }
        public void finish(){
            this.Navigation.PopModalAsync();
        }
        protected override void OnDisappearing()
        {
            
        }
        protected override  Boolean OnBackButtonPressed()
        {
            return true;
        }
    }
}
