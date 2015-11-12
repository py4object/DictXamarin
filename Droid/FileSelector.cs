using System;
using Dict;


[assembly:Xamarin.Forms.Dependency(typeof(Dict.Droid.FileSelector))]
namespace Dict.Droid
{
	public class FileSelector:Dict.FileSlector
	{
		
		public string getfilePath ()
		{
			return "This should be a path";
		}


	}
}

