using BGLParser;
using Dict.Modle;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using System.Linq;

namespace Dict
{
    
    
	public class MainPage : ContentPage
    {
        #region ----fileds---
        Entry wordToLookUpEntry;
        Layout viewDief;
        Layout viewHistory;
        Button lookUp;
        Button addDict;
        WebView defintion;
        ListView history;
        int state=0;
        RelativeLayout root;
        #endregion


        public MainPage()
        {
            history = new ListView();
            history.ItemsSource = DictonaryManager.Instance.history;
            defintion=new WebView();
            wordToLookUpEntry = new Entry
            {
                Placeholder="Enter a word too look up"
            };

            addDict = new Button {
                  Text="click to add a bgl Dictionary",
            };

            lookUp = new Button
            {
                Text="look up"
            };

            
             root= new RelativeLayout { };
            root.Children.Add(wordToLookUpEntry,Constraint.Constant(0),Constraint.Constant(0),
               Constraint.RelativeToParent((parent)=>(parent.Width/3)*2.3));
            root.Children.Add(lookUp, Constraint.RelativeToView(wordToLookUpEntry, (parnt, sibiling) => sibiling.X + sibiling.Width));
            root.Children.Add(history, Constraint.Constant(0), Constraint.RelativeToView(wordToLookUpEntry, (parent, sibiling) => sibiling.Height + 2)
                ,Constraint.RelativeToParent((parent)=>parent.Width),
                Constraint.RelativeToView(wordToLookUpEntry,(parent,sibling)=>parent.Height-sibling.Height));
            root.Children.Add(addDict,Constraint.RelativeToParent((parent)=>(parent.Width/2)-10)
                ,Constraint.RelativeToParent((parent)=>parent.Height-parent.Height*0.1));
            Content = root;

            lookUp.Clicked += lookUp_Clicked;
            addDict.Clicked += addDict_Clicked;
            history.ItemTapped+=((sender,e)=>{
               
                if (state == 2)
                {
                    

                    List<string> diHistory = DictonaryManager.Instance.history;
                    if (!diHistory.Contains(e.Item as string)) diHistory.Add(e.Item as string);

                }
                lookupWord(e.Item as string);
            });
            wordToLookUpEntry.TextChanged += wordToLookUpEntry_TextChanged;
        }

         void wordToLookUpEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
           
             //Device.BeginInvokeOnMainThread(async() => {
             // await System.Threading.Tasks.Task.Run(() =>
             //    {
             //        if (wordToLookUpEntry.Text != "")
             //        {
             //            List<string> allentries = DictonaryManager.Instance.allEntries;
             //            List<string> possblie = (from l in allentries where l.StartsWith(wordToLookUpEntry.Text.ToLower()) select l).ToList();
             //            if (possblie.Count > 0)
             //            {
             //                state = 2;
             //                history.ItemsSource = possblie;
             //            }
             //        }
             //    });

             //   });

                
          }
        


        void addDict_Clicked(object sender, EventArgs e)
        {
            DictonaryManager.Instance.addDictonary();
        }

        void lookUp_Clicked(object sender, EventArgs e)
        {
            string word = wordToLookUpEntry.Text;
            word = word.TrimEnd().ToLower();
            
            if (lookupWord(word))
            {
                List<string> diHistory = DictonaryManager.Instance.history;
                if (!diHistory.Contains(word)) diHistory.Add(word);
            }
        }

        bool  lookupWord(string word)
        {
            HtmlWebViewSource resultsource = new HtmlWebViewSource();
            
            List<BGLEntry> result = DictonaryManager.Instance.getDefintion(word);
            bool funresult;
            if (result == null)
            {
                resultsource.Html = "<h1>No result</h1>";
                funresult=false;
            }
            else
            {
                funresult=true;
                resultsource.Html = "<h1>" + word.ToUpper() + "</h1><br>";
                foreach (BGLEntry s in result)
                {
                    resultsource.Html += "<h4>" + s.definition + "</h4><br>";
                    foreach (var d in s.alternates)
                    {
                        resultsource.Html += "<h4>" + d + "</h4><br>";
                    }
                }
            }
                defintion.Source = resultsource;
                if (state == 0||state==2)
                {
                    root.Children.Remove(history);
                    root.Children.Add(defintion, Constraint.Constant(0), Constraint.RelativeToView(wordToLookUpEntry, (parent, sibiling) => sibiling.Height + 2)
                , Constraint.RelativeToParent((parent) => parent.Width),
                Constraint.RelativeToView(wordToLookUpEntry, (parent, sibling) => parent.Height - sibling.Height));
                    state = 1;
                    
                }
                return funresult;
        }

        

       
    

        private void addBtnDict_Clicked(object sender, EventArgs e)
        {
          //  DictonaryManager.Instance.addDictonary();
            Device.BeginInvokeOnMainThread(() =>
            {
               
            });
        }
        protected override bool OnBackButtonPressed()
        {
            if (state == 0)
                return false;
            else
            {
                history.ItemsSource = DictonaryManager.Instance.history;
                root.Children.Remove(defintion);
                root.Children.Add(history, Constraint.Constant(0), Constraint.RelativeToView(wordToLookUpEntry, (parent, sibiling) => sibiling.Height + 2)
                , Constraint.RelativeToParent((parent) => parent.Width),
                Constraint.RelativeToView(wordToLookUpEntry, (parent, sibling) => parent.Height - sibling.Height));
                state = 0;
                return true;
            }
        }
        

	}
}


