using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Xamarin.Forms;
using BGLParser;
using Dict.Pages;
using System.Runtime.Serialization;

namespace Dict
{
     
   public class DictonaryManager
    {
        private static DictonaryManager instance;
        public static DictonaryManager Instance
        {
            get
            {
                if (instance == null)
                {
                    var dm = loadDictonaryManager();
                    instance = (dm != null) ? dm : new DictonaryManager();
                    
                }
                return instance;
            }
        }

        private Dictionary<string, List<BGLEntry>> dict;
        
        public DictonaryManager()
        {
            regiesterFileListiner();
            dict = new Dictionary<string, List<BGLParser.BGLEntry>>();
        }


        public List<BGLEntry> getDefintion(string word)
        {
            try
            {
                return dict[word];
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }
        #region Adding and parsing the bgl file
        public bool addDictonary()
        {
            
            Xamarin.Forms.DependencyService.Get<FileManager>().getBglPath();
            
            return true;//this should be fixed so that it tells if an error occured  
        }

        private async void parseFile(Page arg1, string bglPath)
        {
         await   ParseTheBGL(bglPath);


            
        }
        public async void parseFile( string bglPath)
        {
            await ParseTheBGL(bglPath);

        }

        private async Task ParseTheBGL(string bglPath)
        {
            WaitPage wait = new WaitPage("");
            await App.Current.MainPage.Navigation.PushModalAsync(wait);
           await Task.Run(() =>
            {
                
                BGLParser.BGLParser parser = new BGLParser.BGLParser();
                Stream o = parser.open(bglPath);
                parser.parseMetaData(o);
                parser.closeStream(o);
                o = parser.open(bglPath);
                BGLEntry entry;
                while ((entry = parser.readEntry(o)) != null)
                {
                    if (entry.headword.IndexOf("$") > 0)
                        entry.headword = entry.headword.Substring(0, entry.headword.IndexOf("$"));
                    try
                    {
                        List<BGLEntry> entryList = dict[entry.headword];
                        entryList.Add(entry);
                    }
                    catch (KeyNotFoundException)
                    {
                        List<BGLEntry> entryList = new List<BGLEntry>();
                        entryList.Add(entry);
                        dict[entry.headword] = entryList;
                    }

                }
                writeDictonaryManager();
            });

           wait.finish();
          
        }
#endregion
        #region -----Save and load the object
        private static DictonaryManager loadDictonaryManager()
        {

            return null;
           return  Xamarin.Forms.DependencyService.Get<FileManager>().LoadDictonaryManager();
    
         }

           

        
        private void writeDictonaryManager()
        {

         //   Xamarin.Forms.DependencyService.Get<FileManager>().saveDictionaryManager(Instance);
           
        }
        private void regiesterFileListiner()
        {
            //MessagingCenter.Subscribe<Page, string>(new Page(), "FilePicked", parseFile);
        }

         
    }
        #endregion


}
        