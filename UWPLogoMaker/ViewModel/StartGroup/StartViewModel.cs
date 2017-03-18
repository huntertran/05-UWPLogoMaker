namespace UWPLogoMaker.ViewModel.StartGroup
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Windows.ApplicationModel.Resources.Core;
    using Windows.UI.Xaml.Controls;
    using Model;
    using Utilities;
    using View.FunctionGroup;
    using View.NewSizeGroup;
    using View.SettingGroup;

    public class StartViewModel : PropertyChangedImplementation
    {
        private IList<MenuListItem> _bottomFunctionList;
        private ObservableCollection<MenuListItem> _topFunctionList;
        private string _pageName;
        private Database _data;
        private Database _customData;

        public IList<MenuListItem> BottomFunctionList
        {
            get { return _bottomFunctionList; }
            set
            {
                if (Equals(value, _bottomFunctionList)) return;
                _bottomFunctionList = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<MenuListItem> TopFunctionList
        {
            get { return _topFunctionList; }
            set
            {
                if (Equals(value, _topFunctionList)) return;
                _topFunctionList = value;
                OnPropertyChanged();
            }
        }

        public string PageName
        {
            get { return _pageName; }
            set
            {
                if (value == _pageName) return;
                _pageName = value;
                OnPropertyChanged();
            }
        }

        public Database Data
        {
            get { return _data; }
            set
            {
                if (Equals(value, _data)) return;
                _data = value;
                OnPropertyChanged();
            }
        }

        public Database CustomData
        {
            get { return _customData; }
            set
            {
                if (Equals(value, _customData)) return;
                _customData = value;
                OnPropertyChanged();
            }
        }

        public async Task Initialize()
        {
            Debug.WriteLine("Initialize size");
            
            Data = await StorageHelper.Json2Object<Database>("data.dat") ?? new Database
            {
                PlatformList = new ObservableCollection<Platform>()
            };

            CustomData = await StorageHelper.Json2Object<Database>("custom.dat");

            if (Data.PlatformList.Count == 0)
            {
                InitializeData();
                await StorageHelper.Object2Json(Data, "data.dat");
            }

            AddTopFunctionList();
            AddBottomFunctionList();
            
            Debug.WriteLine("End Initialize size");
        }

        private void AddTopFunctionList()
        {
            TopFunctionList = new ObservableCollection<MenuListItem>();
            MenuListItem m = new MenuListItem
            {
                Name = ResourceManager.Current.MainResourceMap.GetValue("Resources/StartViewModel_AddTopFunctionList_Render_all_Sizes", new ResourceContext()).ValueAsString,
                MenuF = MenuFunc.RenderSizes,
                IsEnabled = true,
                Icon = ResourceManager.Current.MainResourceMap.GetValue("CommonResources/RenderAllSizeIcon", new ResourceContext()).ValueAsString
            };
            TopFunctionList.Add(m);

            m = new MenuListItem
            {
                Name = ResourceManager.Current.MainResourceMap.GetValue("Resources/StartViewModel_AddBottomFunctionList_Add_new_size", new ResourceContext()).ValueAsString,
                MenuF = MenuFunc.Add,
                IsEnabled = false,
                Icon = ResourceManager.Current.MainResourceMap.GetValue("CommonResources/AddNewSizeIcon", new ResourceContext()).ValueAsString
            };
            TopFunctionList.Add(m);
        }

        private void AddBottomFunctionList()
        {
            BottomFunctionList = new ObservableCollection<MenuListItem>();

            MenuListItem m = new MenuListItem
            {
                Name = ResourceManager.Current.MainResourceMap.GetValue(
                    "Resources/StartViewModel_AddBottomFunctionList_Setting", 
                    new ResourceContext())
                    .ValueAsString,
                MenuF = MenuFunc.Settings,
                IsEnabled = false,
                Icon = ResourceManager.Current.MainResourceMap.GetValue("CommonResources/SettingIcon", new ResourceContext()).ValueAsString
            };
            
            BottomFunctionList.Add(m);
        }

        private void InitializeData()
        {
            /////////UWP//////////

            #region UWP

            Data.PlatformList = new ObservableCollection<Platform>();
            Platform p = new Platform
            {
                Name = "UWP",
                Icon = "W10",
                SaveLogoList = new List<LogoObject>(),
                IsEnabled = false
            };
            string fName = "Square71x71Logo";
            p.SaveLogoList.Add(new LogoObject(fName, 400, 71, true));
            p.SaveLogoList.Add(new LogoObject(fName, 200, 71, true));
            p.SaveLogoList.Add(new LogoObject(fName, 150, 71, true));
            p.SaveLogoList.Add(new LogoObject(fName, 125, 71, true));
            p.SaveLogoList.Add(new LogoObject(fName, 100, 71, true));
            //
            fName = "Square150x150Logo";
            p.SaveLogoList.Add(new LogoObject(fName, 400, 150, true));
            p.SaveLogoList.Add(new LogoObject(fName, 200, 150, true));
            p.SaveLogoList.Add(new LogoObject(fName, 150, 150, true));
            p.SaveLogoList.Add(new LogoObject(fName, 125, 150, true));
            p.SaveLogoList.Add(new LogoObject(fName, 100, 150, true));
            //
            fName = "Square310x310Logo";
            p.SaveLogoList.Add(new LogoObject(fName, 400, 310, true));
            p.SaveLogoList.Add(new LogoObject(fName, 200, 310, true));
            p.SaveLogoList.Add(new LogoObject(fName, 150, 310, true));
            p.SaveLogoList.Add(new LogoObject(fName, 125, 310, true));
            p.SaveLogoList.Add(new LogoObject(fName, 100, 310, true));
            //
            fName = "Square44x44Logo";
            p.SaveLogoList.Add(new LogoObject(fName, 400, 44, true));
            p.SaveLogoList.Add(new LogoObject(fName, 200, 44, true));
            p.SaveLogoList.Add(new LogoObject(fName, 150, 44, true));
            p.SaveLogoList.Add(new LogoObject(fName, 125, 44, true));
            p.SaveLogoList.Add(new LogoObject(fName, 100, 44, true));

            //
            fName = "Target";
            p.SaveLogoList.Add(new LogoObject(fName, 100, 16, true));
            p.SaveLogoList.Add(new LogoObject(fName, 150, 16, true));
            p.SaveLogoList.Add(new LogoObject(fName, 300, 16, true));
            p.SaveLogoList.Add(new LogoObject(fName, 1600, 16, true));
            //
            fName = "StoreLogo";
            p.SaveLogoList.Add(new LogoObject(fName, 400, 50, true));
            p.SaveLogoList.Add(new LogoObject(fName, 200, 50, true));
            p.SaveLogoList.Add(new LogoObject(fName, 150, 50, true));
            p.SaveLogoList.Add(new LogoObject(fName, 125, 50, true));
            p.SaveLogoList.Add(new LogoObject(fName, 100, 50, true));
            //
            fName = "Wide310x150Logo";
            p.SaveLogoList.Add(new LogoObject(fName, 400, 310, false));
            p.SaveLogoList.Add(new LogoObject(fName, 200, 310, false));
            p.SaveLogoList.Add(new LogoObject(fName, 150, 310, false));
            p.SaveLogoList.Add(new LogoObject(fName, 125, 310, false));
            p.SaveLogoList.Add(new LogoObject(fName, 100, 310, false));

            fName = "SplashScreen";
            p.SaveLogoList.Add(new LogoObject(fName, 400, 620, false));
            p.SaveLogoList.Add(new LogoObject(fName, 200, 620, false));
            p.SaveLogoList.Add(new LogoObject(fName, 150, 620, false));
            p.SaveLogoList.Add(new LogoObject(fName, 125, 620, false));
            p.SaveLogoList.Add(new LogoObject(fName, 100, 620, false));

            Data.PlatformList.Add(p);

            #endregion

            /////////Windows 8.1//////////////////

            #region Windows 8.1

            p = new Platform
            {
                Name = "Windows 8.1",
                Icon = "W8.1",
                SaveLogoList = new List<LogoObject>()
            };
            fName = "Square70x70Logo";
            p.SaveLogoList.Add(new LogoObject(fName, 180, 70, true));
            p.SaveLogoList.Add(new LogoObject(fName, 140, 70, true));
            p.SaveLogoList.Add(new LogoObject(fName, 100, 70, true));
            p.SaveLogoList.Add(new LogoObject(fName, 80, 70, true));
            //
            fName = "Square150x150Logo";
            p.SaveLogoList.Add(new LogoObject(fName, 180, 150, true));
            p.SaveLogoList.Add(new LogoObject(fName, 140, 150, true));
            p.SaveLogoList.Add(new LogoObject(fName, 100, 150, true));
            p.SaveLogoList.Add(new LogoObject(fName, 80, 150, true));
            //
            fName = "Square310x310Logo";
            p.SaveLogoList.Add(new LogoObject(fName, 180, 310, true));
            p.SaveLogoList.Add(new LogoObject(fName, 140, 310, true));
            p.SaveLogoList.Add(new LogoObject(fName, 100, 310, true));
            p.SaveLogoList.Add(new LogoObject(fName, 80, 310, true));
            //
            fName = "Square30x30Logo";
            p.SaveLogoList.Add(new LogoObject(fName, 180, 30, true));
            p.SaveLogoList.Add(new LogoObject(fName, 140, 30, true));
            p.SaveLogoList.Add(new LogoObject(fName, 100, 30, true));
            p.SaveLogoList.Add(new LogoObject(fName, 80, 30, true));
            //
            fName = "StoreLogo";
            p.SaveLogoList.Add(new LogoObject(fName, 180, 90, true));
            p.SaveLogoList.Add(new LogoObject(fName, 140, 70, true));
            p.SaveLogoList.Add(new LogoObject(fName, 100, 50, true));
            //
            fName = "Wide310x150Logo";
            p.SaveLogoList.Add(new LogoObject(fName, 180, 310, false));
            p.SaveLogoList.Add(new LogoObject(fName, 140, 310, false));
            p.SaveLogoList.Add(new LogoObject(fName, 100, 310, false));
            p.SaveLogoList.Add(new LogoObject(fName, 80, 310, false));
            //
            fName = "BadgeLogo";
            p.SaveLogoList.Add(new LogoObject(fName, 180, 24, true));
            p.SaveLogoList.Add(new LogoObject(fName, 140, 24, true));
            p.SaveLogoList.Add(new LogoObject(fName, 100, 24, true));

            fName = "SplashScreen";
            p.SaveLogoList.Add(new LogoObject(fName, 180, 620, false, "620:300"));
            p.SaveLogoList.Add(new LogoObject(fName, 140, 620, false, "620:300"));
            p.SaveLogoList.Add(new LogoObject(fName, 100, 620, false, "620:300"));

            Data.PlatformList.Add(p);

            #endregion

            /////////Windows Phone 8.1////////////

            #region Windows Phone 8.1

            p = new Platform
            {
                Name = "Windows Phone 8.1",
                Icon = "WP8.1",
                SaveLogoList = new List<LogoObject>()
            };
            fName = "Square71x71Logo";
            p.SaveLogoList.Add(new LogoObject(fName, 240, 71, true));
            p.SaveLogoList.Add(new LogoObject(fName, 140, 71, true));
            p.SaveLogoList.Add(new LogoObject(fName, 100, 71, true));

            fName = "Square150x150Logo";
            p.SaveLogoList.Add(new LogoObject(fName, 240, 150, true));
            p.SaveLogoList.Add(new LogoObject(fName, 140, 150, true));
            p.SaveLogoList.Add(new LogoObject(fName, 100, 150, true));

            fName = "Square44x44Logo";
            p.SaveLogoList.Add(new LogoObject(fName, 240, 44, true));
            p.SaveLogoList.Add(new LogoObject(fName, 140, 44, true));
            p.SaveLogoList.Add(new LogoObject(fName, 100, 44, true));

            fName = "StoreLogo";
            p.SaveLogoList.Add(new LogoObject(fName, 240, 50, true));
            p.SaveLogoList.Add(new LogoObject(fName, 140, 50, true));
            p.SaveLogoList.Add(new LogoObject(fName, 100, 50, true));

            fName = "Wide310×150Logo";
            p.SaveLogoList.Add(new LogoObject(fName, 240, 310, false));
            p.SaveLogoList.Add(new LogoObject(fName, 140, 310, false));
            p.SaveLogoList.Add(new LogoObject(fName, 100, 310, false));

            fName = "SplashScreen";
            p.SaveLogoList.Add(new LogoObject(fName, 240, 480, false, "480:800"));
            p.SaveLogoList.Add(new LogoObject(fName, 140, 480, false, "480:800"));
            p.SaveLogoList.Add(new LogoObject(fName, 100, 480, false, "480:800"));

            Data.PlatformList.Add(p);

            Data.DatabaseVersion = 0;
            Data.UpdateMessage = "Welcome";

            #endregion
        }

        public void NavigateToFunction(Frame frame, MenuFunc func)
        {
            switch (func)
            {
                case MenuFunc.RenderSizes:
                {
                    frame.Navigate(typeof (MainPage));
                    break;
                }
                case MenuFunc.Settings:
                {
                    frame.Navigate(typeof (SettingPage));
                    break;
                }
                case MenuFunc.Add:
                {
                    frame.Navigate(typeof (NewSizePage));
                    break;
                }
                default:
                {
                    frame.Navigate(typeof (PreviewPage));
                    break;
                }
            }
        }
    }
}