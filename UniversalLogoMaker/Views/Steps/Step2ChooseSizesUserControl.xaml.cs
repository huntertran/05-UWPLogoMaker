namespace UniversalLogoMaker.Views.Steps
{
    using ViewModels;

    public sealed partial class Step2ChooseSizesUserControl
    {
        private StartViewModel _vm;

        public Step2ChooseSizesUserControl()
        {
            _vm = (StartViewModel)DataContext;

            InitializeComponent();
        }
    }
}
