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
					HistoryListView,
                    wv,
					addDictBtn

				}
			};
			this.SizeChanged += (object sender, EventArgs e) => {
			wordToLookupEntry.WidthRequest=this.Width-lookUpBtn.Width;
            MessagingCenter.Subscribe<Page, string>(new Page(), "FilePicked", (boo, arg) =>
            {
                BGLParser.BGLParser p = new BGLParser.BGLParser();
                System.IO.Stream s = p.open(arg);
                p.parseMetaData(s);
                p.closeStream(s);
                s = p.open(arg);
                BGLParser.BGLEntry entry;

                while ((entry = p.readEntry(s)) != null)
                {
                    string he = entry.headword;
                    if (he.IndexOf("$")!=-1)
                    {
                        he = he.Substring(0, he.IndexOf("$"));   
                    }
                    te[he] = entry.definition;
                    
                }

                

            });

		};
            addDictBtn.Clicked += addBtnDict_Clicked;
            lookUpBtn.Clicked += lookUpBtn_Clicked;
		}

        private void lookUpBtn_Clicked(object sender, EventArgs e)
        {
            string def=null;
            try
            {
                def = te[wordToLookupEntry.Text];
               
            }catch(KeyNotFoundException ){


            }
            if (!string.IsNullOrWhiteSpace(def))
            {
                sr.Html = def;
                wv.Source = sr;
            }
            
        }

        private void addBtnDict_Clicked(object sender, EventArgs e)
        {
            var asd = DependencyService.Get<Dict.FileSlector>();
          

           
             DependencyService.Get<Dict.FileSlector>().getfilePath();
        }

        

	}
}


