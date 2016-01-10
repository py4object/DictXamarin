using BGLParser;
using Dict.Modle;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using System.Linq;
using System.Text;

namespace Dict
{
    
    
	public class MainPage : ContentPage
    {
        #region ----fileds---
        Entry wordToLookUpEntry;
        Layout viewDief;
        Layout viewHistory;
        Button lookUp;
      
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
          
            Content = root;

            lookUp.Clicked += lookUp_Clicked;
            
            history.ItemTapped+=((sender,e)=>{
               
                if (state == 2)
                {
                    

                    List<string> diHistory = DictonaryManager.Instance.history;
                    if (!diHistory.Contains(e.Item as string)) diHistory.Add(e.Item as string);

                }
                lookupWord(e.Item as string);
            });
            wordToLookUpEntry.TextChanged += wordToLookUpEntry_TextChanged;
           defintion.Navigating+=defintion_Navigating;
        }

        private void defintion_Navigating(object sender, WebNavigatingEventArgs e)
        {
            e.Cancel = true;
            string d = e.Url.Substring("http://".Length);
            d = PCLWebUtility.WebUtility.UrlDecode(d);
            var s = d.Split(',');
            int i = Int16.Parse(s[0]);
            int j = Int16.Parse(s[1].Remove(s[1].IndexOf("/")));
            string word = wordToLookUpEntry.Text;
            word = word.TrimEnd().ToLower();
           var dd= DictonaryManager.Instance.getDefintion(word);
           if (dd != null)
           {
               try
               {
                   var f = dd[i].definition.Split(' ');
                   wordToLookUpEntry.Text = f[j].Remove(f[j].IndexOf(","));
                   lookupWord(f[j].Remove(f[j].IndexOf(",")));
               }
               catch (Exception)
               {
                   
               }
           }
           else
           {
               HtmlWebViewSource resultsource = new HtmlWebViewSource();
               resultsource.Html = "<h1>No result</h1>";
               defintion.Source = resultsource;
           }
            
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
                for (int i = 0; i < result.Count;i++ )
                {
                    var s = result[i]; 
                    string dd = s.definition;
                    var x = dd.Split(' ');
                    for (int j = 0; j < x.Length; j++)
                    {
                        string l = x[j];
                        resultsource.Html += "<h4> <a href=\"http://" +i+ ","+j + "\">" + l + "  </h4>";
                    }
                    for (int j = 0; j < s.alternates.Count; j++)
                    {
                        string d = s.alternates[i];
                        resultsource.Html += "<h4> <a href=\"http://" + i + "," + j + "\">" + d + "  </h4>";
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


