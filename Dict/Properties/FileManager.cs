using System;
using System.IO;

namespace Dict
{
	public interface FileManager
	{
		void getBglPath();

        StreamWriter getFileManagerStreamWriter();

        StreamReader getFileMangerStreamReader();

        void CloseStreamWirter(StreamWriter w);
        void CLoseStreamReader(StreamReader r);
            

	}
}

