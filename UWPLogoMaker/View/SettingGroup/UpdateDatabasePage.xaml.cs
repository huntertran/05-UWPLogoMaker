namespace UWPLogoMaker.View.SettingGroup
{
    using Windows.UI.Xaml.Input;
    using Utilities;
    using ViewModel.StartGroup;

    public sealed partial class UpdateDatabasePage
    {
        private readonly StartViewModel _vm;
        public UpdateDatabasePage()
        {
            InitializeComponent();
            _vm = DataContext as StartViewModel;
        }

        private async void GenerateJsonFile_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            await StorageHelper.Object2Json(_vm.Data, "data.dat");
        }

        private async void CheckNewDatabaseButton_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            await ApiService.UpdateDatabase();
        }
    }
}
