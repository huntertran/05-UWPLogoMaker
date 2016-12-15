using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPLogoMaker.Model
{
    public class AppItem
    {
        public string name { get; set; }
        public string logo { get; set; }
        public string link { get; set; }
        public string desc { get; set; }
    }

    public class MoreAppsRootObject
    {
        public string author { get; set; }
        public string publisherName { get; set; }
        public List<AppItem> app { get; set; }
    }
}
