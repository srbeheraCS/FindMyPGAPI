using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMyPG.Core.PGEngine
{
    public sealed class Singleton
    {
        private static Singleton _singleton;
        private Singleton()
        {

        }
        
        public static Singleton singleton
        {
            get
            {
                if(_singleton == null)
                {
                    _singleton = new Singleton();
                }
                return _singleton;
            }
        }
    }
}
