using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dict.Modle
{
    
    class DictonaryManager
    {
        public static DictonaryManager instance;
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

        private static DictonaryManager loadDictonaryManager()
        {
            DictonaryManager dm=null;

            return dm;

        }
    }
}
