using System;
using Dict;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Compass.FilePicker;
using Xamarin.Forms;
using Dict.Droid;

[assembly:Xamarin.Forms.Dependency(typeof(FileSelector))]
namespace Dict.Droid
{
	public class FileSelector:Dict.FileSlector
	{		
		public string getfilePath ()
		{
			
			var intent = new Intent(Forms.Context,typeof(FilePickerActivity));
			intent.PutExtra(FilePickerActivity.ExtraInitialDirectory, "/");
			intent.SetType("text/plain");
			intent.PutExtra(FilePickerActivity.ExtraMode, (int)FilePickerMode.File);
			((Activity)Forms.Context).StartActivityForResult(intent, FilePickerActivity.ResultCodeDirSelected);

            
            var result = FilePickerActivity.ResultSelectedDir;

            return result;

		}


	}
}

