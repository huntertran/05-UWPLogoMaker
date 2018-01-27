namespace UniversalLogoMaker.Models
{
    using System.Collections.Generic;
    using Windows.ApplicationModel.DataTransfer;

    public class DragDropStartingData
    {
        public DataPackage Data { get; set; }

        public IList<object> Items { get; set; }
    }
}
