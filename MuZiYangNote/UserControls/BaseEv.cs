using Model;
using PublicHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuZiYangNote.UserControls
{
    public class BaseEv
    {
        public class DataChangeEventArgs : EventArgs
        {
            public string Str { get; set; }

            public MessageLevel ty { get; set; }

            public ShowLog.customColor _c { get; set; }

            public DataChangeEventArgs(string s1, MessageLevel s2, ShowLog.customColor c = null)
            {
                Str = s1;
                ty = s2;
                _c = c;
            }
        }
    }
}
