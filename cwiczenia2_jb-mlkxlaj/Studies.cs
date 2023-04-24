using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie2
{
    internal class Studies
    {
        public string studies { get; set; }
        public string mode { get; set; }
        public Studies(string studies, string mode)
        {
            this.studies = studies;
            this.mode = mode;
        }
    }
}
