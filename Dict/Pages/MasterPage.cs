using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace Dict.Pages
{
    public class MasterPage : ContentPage
    {
        ListView lv;
       
        public MasterPage()
        
        {
            this.Title = "Stuff";
             lv = new ListView
            {

            };
            List<string>buttons=new List<string>();
            buttons.Add("click to add a Dict");
            lv.ItemsSource = buttons;
            lv.ItemTapped+=addict;
            
            Content = new StackLayout
            {
               Children={lv}
            };
        }

        private void addict(object sender, ItemTappedEventArgs e)
        {
          
            DictonaryManager.Instance.addDictonary();
        }
    }
}
