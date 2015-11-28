using BGLParser;
using Dict.Modle;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Dict
{
	public class MainPage : ContentPage
    {
        Dictionary<string, string> te;
        Entry wordToLookupEntry;
        WebView wv;
        HtmlWebViewSource sr;
		public MainPage ()
		{
           te=new Dictionary<string,string>();
           wv = new WebView()
           {
               Source = new HtmlWebViewSource()
               {
                   Html=@"<html>
                       hoo</html>"
               }
           };
           sr= new HtmlWebViewSource();
			 wordToLookupEntry=new Entry{
				Placeholder="Enter a word to look up"
			};
			Button addDictBtn=new Button{
				Text="Add a new dict"
			};
			ListView HistoryListView=new ListView{
			};
			Button lookUpBtn=new Button{
				Text="Look up a word"
			};

            
			Content = new StackLayout { 
				VerticalOptions = LayoutOptions.Center,
				Children = {
					new StackLayout {
						Orientation = StackOrientation.Horizontal,
						HorizontalOptions = LayoutOptions.StartAndExpand,
						Children = {
							wordToLookupEntry,
							lookUpBtn,

						},

					},
                    wv,
					addDictBtn

				}
                
			};
			this.SizeChanged += (object sender, EventArgs e) => {
                wordToLookupEntry.WidthRequest = this.Width - lookUpBtn.Width;
                wv.HeightRequest = this.Height - wordToLookupEntry.Height-addDictBtn.Height;
		};
            addDictBtn.Clicked += addBtnDict_Clicked;
            lookUpBtn.Clicked += lookUpBtn_Clicked;
		}

        private void lookUpBtn_Clicked(object sender, EventArgs e)
        {


            List<BGLParser.BGLEntry> entrylist = DictonaryManager.Instance.getDefintion(wordToLookupEntry.Text.TrimEnd());
            if (entrylist != null) 
            {
                sr.Html = "";
                foreach(BGLEntry entry in entrylist){
                    sr.Html += entry.definition +"<br>";
                    foreach (string es in entry.alternates)
                    {
                        sr.Html += es + "<br>";
                    }
                }
                wv.Source = sr;
            }
            
        }

        private void addBtnDict_Clicked(object sender, EventArgs e)
        {
            DictonaryManager.Instance.addDictonary();
           
        }

        

	}
}


