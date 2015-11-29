using System;
using System.IO;

namespace Dict
{
	public interface FileManager
	{
		void getBglPath();

         DictonaryManager LoadDictonaryManager();

        void saveDictionaryManager(DictonaryManager dm);

        void CloseStreamWirter(StreamWriter w);
        void CLoseStreamReader(StreamReader r);
            

	}
}

