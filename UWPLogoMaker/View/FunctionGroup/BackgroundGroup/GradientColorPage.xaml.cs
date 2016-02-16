using Windows.UI.Xaml.Controls;
using UWPLogoMaker.ViewModel.FunctionGroup.BackgroundGroup;

namespace UWPLogoMaker.View.FunctionGroup.BackgroundGroup
{
    public sealed partial class GradientColorPage
    {
        public GradientColorBackgroundViewModel Vm => (GradientColorBackgroundViewModel)DataContext;

        public GradientColorPage()
        {
            InitializeComponent();
        }
    }
}
