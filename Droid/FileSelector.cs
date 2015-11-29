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

[assembly:Xamarin.Forms.Dependency(typeof(FileSelector))]
namespace Dict.Droid
{
	public class FileSelector:Dict.FileManager
	{		
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
            string filename = "SACA.ASDQR";
            string path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), filename);
            if(!File.Exists(path)){return null;}
            using (Stream strem =File.Open(path,FileMode.Open))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                DictonaryManager dm = bformatter.Deserialize(strem) as DictonaryManager;
                strem.Close();
                return dm;
              
            }
        }

        public void saveDictionaryManager(DictonaryManager dm)
        {
            string filename = "SACA.ASDQR";
            string path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), filename);

            using (Stream stream=File.Open(path,FileMode.Create)){
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                bformatter.Serialize(stream, dm);
                stream.Close();
            }
        }
    }
}

