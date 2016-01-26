using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Input;
using Newtonsoft.Json;
using UWPLogoMaker.Utilities;
using UWPLogoMaker.ViewModel.StartGroup;

namespace UWPLogoMaker.View.SettingGroup
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
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
            await StaticMethod.CheckForDatabase();
        }
    }
}
