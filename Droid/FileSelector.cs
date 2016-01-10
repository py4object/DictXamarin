using System;
using Dict;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Compass.FilePicker;
using Xamarin.Forms;
using Dict.Droid;
using System.IO;
using Newtonsoft.Json;

[assembly:Xamarin.Forms.Dependency(typeof(FileSelector))]
namespace Dict.Droid
{
   
	public class FileSelector:Dict.FileManager
    {
        private static string filename = "boo.json";
		public void getBglPath ()
		{
			
			var intent = new Intent(Forms.Context,typeof(FilePickerActivity));
			intent.PutExtra(FilePickerActivity.ExtraInitialDirectory, "/");
			intent.SetType("text/plain");
			intent.PutExtra(FilePickerActivity.ExtraMode, (int)FilePickerMode.File);
			((Activity)Forms.Context).StartActivityForResult(intent, FilePickerActivity.ResultCodeDirSelected);

            

            

		}



        


     



        public void CloseStreamWirter(StreamWriter w)
        {
            w.Close();
        }

        public void CLoseStreamReader(StreamReader r)
        {
            r.Close();
        }


        public DictonaryManager LoadDictonaryManager()
        {
            try
            {
                DictonaryManager dm = null;
                 string filepath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), filename);
                using (StreamReader reader = new StreamReader(File.Open(filepath, FileMode.Open)))
                {
                    string bo = reader.ReadToEnd();
                    dm = JsonConvert.DeserializeObject<DictonaryManager>(bo);

                }
                return dm;
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public void saveDictionaryManager(DictonaryManager dm)
        {

            string filepath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), filename);
            JsonSerializer serlizer = new JsonSerializer();
            serlizer.NullValueHandling = NullValueHandling.Ignore;
                
            using (StreamWriter writer = new StreamWriter(@filepath))
            {
                using (JsonWriter jwriter = new JsonTextWriter(writer))
                {
                    serlizer.Serialize(jwriter, dm);
                }
            }
        }
    }
}

