using Windows.ApplicationModel.DataTransfer;

namespace UniversalLogoMaker3.Models
{
    public class DragDropData
    {
        public DataPackageOperation AcceptedOperation { get; set; }

        public DataPackageView DataView { get; set; }
    }
}
