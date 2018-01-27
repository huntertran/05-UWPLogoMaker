namespace UniversalLogoMaker.Models
{
    using System.Collections.Generic;
    using Windows.ApplicationModel.DataTransfer;

    public class DragDropCompletedData
    {
        public DataPackageOperation DropResult { get; set; }

        public IReadOnlyList<object> Items { get; set; }
    }
}
