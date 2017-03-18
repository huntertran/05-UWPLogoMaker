/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:BrandedApp"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

namespace UWPLogoMaker.ViewModel
{
    using FunctionGroup;
    using GalaSoft.MvvmLight.Ioc;
    using Microsoft.Practices.ServiceLocation;
    using NewSizeGroup;
    using SettingGroup;
    using StartGroup;

    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<SaveLocationSettingViewModel>();
        }

        public MainViewModel MainVm => ServiceLocator.Current.GetInstance<MainViewModel>();
        public StartViewModel StartVm => StaticData.StartVm;
        public NewSizeViewModel NewSizeVm => StaticData.NewSizeVm;
        public SaveLocationSettingViewModel SaveLocationSettingVm => ServiceLocator.Current.GetInstance<SaveLocationSettingViewModel>();

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}