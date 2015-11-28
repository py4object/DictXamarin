using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Xamarin.Forms;
using BGLParser;
namespace Dict.Modle
{
    
    class DictonaryManager
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

        private async Task ParseTheBGL(string bglPath)
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
        }
#endregion
        #region -----Save and load the object
        private static DictonaryManager loadDictonaryManager()
        {
            return null;
          
           StreamReader reader = Xamarin.Forms.DependencyService.Get<FileManager>().getFileMangerStreamReader();
            {
                if (reader==null)
                    return null;

                DictonaryManager e= Newtonsoft.Json.JsonConvert.DeserializeObject<DictonaryManager>(reader.ReadToEnd());
                e.regiesterFileListiner();
                DependencyService.Get<FileManager>().CLoseStreamReader(reader);
                return e;
            }

           

        }
        private void writeDictonaryManager()
        {
            
            StreamWriter writer = Xamarin.Forms.DependencyService.Get<FileManager>().getFileManagerStreamWriter();
              writer.AutoFlush=true;
                writer.Write(Newtonsoft.Json.JsonConvert.SerializeObject(Instance));
                DependencyService.Get<FileManager>().CloseStreamWirter(writer);
          

           
        }
        private void regiesterFileListiner()
        {
            //MessagingCenter.Subscribe<Page, string>(new Page(), "FilePicked", parseFile);
        }

         
    }
        #endregion


}
        