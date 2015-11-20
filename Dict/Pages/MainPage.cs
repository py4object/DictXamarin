﻿using System;

using Xamarin.Forms;

namespace Dict
{
	public class MainPage : ContentPage
	{
		public MainPage ()
		{
			Entry wordToLookupEntry=new Entry{
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
					addDictBtn

				}
			};
			this.SizeChanged += (object sender, EventArgs e) => {
			wordToLookupEntry.WidthRequest=this.Width-lookUpBtn.Width;
            MessagingCenter.Subscribe<Page, string>(new Page(), "FilePicked", (boo, arg) =>
            {
               

            });

		};
            addDictBtn.Clicked += addBtnDict_Clicked;
		
		}

        private void addBtnDict_Clicked(object sender, EventArgs e)
        {
            var asd = DependencyService.Get<Dict.FileSlector>();
            var result = asd.getfilePath();

           
            var sdf = DependencyService.Get<Dict.FileSlector>().getfilePath();
        }

        

	}
}


