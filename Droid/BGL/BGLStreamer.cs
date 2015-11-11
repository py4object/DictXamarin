using System;
using System.IO;
using ICSharpCode.SharpZipLib.GZip;
using Dict.Droid;

[assembly:Xamarin.Forms.Dependency(typeof(BGLStreamer))]
namespace Dict.Droid
{

	public class BGLStreamer :BGLStream
	{
		public Stream getBGLStream (string filePath){
			Stream file;
			try {
				file = new FileStream(filePath, FileMode.Open);
			} catch (IOException e) {
				throw e;
			}
			byte[] buf = new byte[6];
			int i = file.Read (buf, 0, 6);
			;
			/* First four bytes: BGL signature 0x12340001 or 0x12340002 (big-endian) */
			if (buf [0] != 0x12 || buf [1] != 0x34 || buf [2] != 0x0 || (buf [3] != 0 && buf [3] > 2) || i < 6) {
				file.Close ();
				throw new FileLoadException (filePath + "Not a bgl file");
			}
			i = buf [4] << 8 | buf [5];
			if (i < 6) {
				throw new FileLoadException (filePath + "gz pointer not found");
			}
			file.Seek (i, SeekOrigin.Begin);
			return new GZipInputStream (file);
		}

		public void closeStream (Stream s)
		{
			s.Close ();
		}
	}

}

