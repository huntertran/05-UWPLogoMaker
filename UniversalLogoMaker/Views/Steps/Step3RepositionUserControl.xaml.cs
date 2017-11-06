namespace UniversalLogoMaker.Views.Steps
{
    using Models;

    public sealed partial class Step3RepositionUserControl
    {
        public ReviewSource Vm => (ReviewSource)DataContext;

        public Step3RepositionUserControl()
        {
            InitializeComponent();
        }
    }
}