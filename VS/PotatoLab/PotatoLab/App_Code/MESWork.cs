using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PotatoLab
{
    public class MESWork
    {
        public string WorkID { get; set; }
        public string WorkName { get; set; }
        public string SRNo { get; set; }
        public string SRTitle { get; set; }

        public bool SaveToFile()
        {
            return true;
        }
        public bool SaveToDB()
        {
            return true;
        }

    }
}
