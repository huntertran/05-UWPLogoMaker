namespace UniversalLogoMaker.Models
{
    using Windows.ApplicationModel.DataTransfer;

    public class DragDropData
    {
        public DataPackageOperation AcceptedOperation { get; set; }

        public DataPackageView DataView { get; set; }
    }
}
