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



        


        public System.IO.StreamWriter getFileManagerStreamWriter()
        {
            StreamWriter writer;
            string FileName = "XSADMCAWE.CSPOW";
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            if (File.Exists(Path.Combine(path, FileName)))
            {
                File.Delete(Path.Combine(path, FileName));
            }
            writer = new StreamWriter(File.Create(Path.Combine(path, FileName)));
           
            return writer;
        }


        public StreamReader getFileMangerStreamReader()
        {
            StreamReader reader;
            string FileName = "XSADMCAWE.CSPOW";
            string path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),FileName);
            if (!File.Exists(path))
                return null;
            reader = new StreamReader(path);
            return reader; ;
            
        }


        public void CloseStreamWirter(StreamWriter w)
        {
            w.Close();
        }

        public void CLoseStreamReader(StreamReader r)
        {
            r.Close();
        }
    }
}

