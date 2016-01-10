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
using Dict.Modle;

namespace Dict
{
     
   public class DictonaryManager
    {
        private static DictonaryManager instance;
        public List<string> history { get; private set; }
        public List<string> allEntries { get; private set; }
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

      public  List<Dictonary> dicts;   //should be private ,it's temprory public so json can serlize it 
        
        public DictonaryManager()
        {
            regiesterFileListiner();
            allEntries=new List<string>();
            dicts = new List<Dictonary>();
            history = new List<string>();
            
        }


        public List<BGLEntry> getDefintion(string word)
        {
            List<BGLEntry> entryes = new List<BGLEntry>();
           
                foreach(var d in dicts){
                     try
                     {
                    entryes.Add(d.dict[word]);
                     }catch(Exception ){

                     }
                }
                if (entryes.Count == 0)
                    return null;
                return entryes;
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
                Dictonary d = new Dictonary();
                dicts.Add(d);
                d.dict=new Dictionary<string,BGLEntry>();
                while ((entry = parser.readEntry(o)) != null)
                {
                    if (entry.headword.IndexOf("$") > 0)
                        entry.headword = entry.headword.Substring(0, entry.headword.IndexOf("$"));
                    if (entry.definition.IndexOf("3Pu<charset c=T>02D0;</charset>d") > 0)
                    {
                        entry.definition = entry.definition.Remove(entry.definition.LastIndexOf("3Pu<charset c=T>02D0;</charset>d"));
                    }

                  
                    
                    try
                    {
                        d.dict[entry.headword] = entry;
                    }
                    catch (KeyNotFoundException)
                    {

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
           var dm= Xamarin.Forms.DependencyService.Get<FileManager>().LoadDictonaryManager();

           return dm;
         }

           

        
        private void writeDictonaryManager()
        {

            Xamarin.Forms.DependencyService.Get<FileManager>().saveDictionaryManager(Instance);
         //asd  
        }
        private void regiesterFileListiner()
        {
            MessagingCenter.Subscribe<Page, string>(new Page(), "FilePicked", parseFile);
        }

         
    }
        #endregion


}
        