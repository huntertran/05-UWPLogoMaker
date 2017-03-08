using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources.Core;
using Windows.UI.Xaml.Controls;
using UWPLogoMaker.Model;
using UWPLogoMaker.Utilities;
using UWPLogoMaker.View.FunctionGroup;
using UWPLogoMaker.View.NewSizeGroup;
using UWPLogoMaker.View.SettingGroup;

namespace UWPLogoMaker.ViewModel.StartGroup
{
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
                Name =
                    ResourceManager.Current.MainResourceMap.GetValue(
                        "Resources/StartViewModel_AddTopFunctionList_Render_all_Sizes", new ResourceContext())
                        .ValueAsString,
                MenuF = MenuFunc.RenderSizes,
                IsEnabled = true,
                Icon =
                    "M21.994995,11.757997L12.500992,22.164003 21.994995,22.164003z M9.9360046,9.0230113L9.9360046,20.296007 20.220001,9.0230113z M6.647995,0L9.9360046,0 9.9360046,6.035996 22.942993,6.035996 28.280991,0.18600463 30.813995,2.0930024 25.285004,8.1530009 25.285004,22.164003 32,22.164003 32,25.151003 25.350998,25.151003 25.350998,31.187 22.065002,31.187 22.065002,25.151003 6.647995,25.151003 6.647995,9.0230113 0,9.0230113 0,6.035996 6.647995,6.035996z"
            };
            TopFunctionList.Add(m);

            m = new MenuListItem
            {
                Name = ResourceManager.Current.MainResourceMap.GetValue("Resources/StartViewModel_AddBottomFunctionList_Add_new_size", new ResourceContext()).ValueAsString,
                MenuF = MenuFunc.Add,
                IsEnabled = false,
                Icon =
                    "M12.099999,0L18.700002,0 18.700002,12.6 30.8,12.6 30.8,19.499998 18.800008,19.499998 18.800008,31.999998 12.200005,31.999998 12.200005,19.4 0,19.4 0,12.500001 12.099999,12.500001z"
            };
            TopFunctionList.Add(m);

            //m = new MenuListItem
            //{
            //    Name = ResourceManager.Current.MainResourceMap.GetValue("Resources/StartViewModel_AddTopFunctionList_Add_from_SVG", new ResourceContext()).ValueAsString,
            //    MenuF = MenuFunc.AddSvg,
            //    Icon =
            //        "M28,28L28,30.699951 30.700012,30.699951 30.700012,28z M1.3000488,1.5L1.3000488,4.1999512 4,4.1999512 4,1.5z M16,0C17.5,0 18.700012,1.1999512 18.700012,2.6999512 18.700012,3.8999634 17.900024,4.8999634 16.800049,5.2999878L16.700012,5.2999878 16.700012,13.399963 18.800049,13.399963 18.800049,19.099976 16.700012,19.099976 16.800049,19.399963C17.600037,22.399963,19.900024,27,26.5,28.199951L26.800049,28.199951 26.800049,26.699951 32,26.699951 32,32 26.800049,32 26.800049,29.5C21.400024,28.699951,18.5,25.699951,16.900024,22.899963L16.800049,22.699951 16.800049,26.699951 16.900024,26.699951C18,27 18.800049,28.099976 18.800049,29.299988 18.800049,30.799988 17.600037,32 16.100037,32 14.600037,32 13.400024,30.799988 13.400024,29.299988 13.400024,28.099976 14.200012,27.099976 15.300049,26.699951L15.300049,19.099976 13.300049,19.099976 13.300049,13.399963 15,13.399963 14.900024,13.099976C13.5,6.1999512,8.1000366,4.1999512,5.3000488,3.6999512L5.2000122,3.6999512 5.2000122,5.5999756 0,5.5999756 0,0.19995117 5.2000122,0.19995117 5.2000122,2.2999878C7.7000122,2.7999878,12.700012,4.3999634,15.100037,9.5999756L15.200012,9.7999878 15.200012,5.1999512C14.100037,4.8999634 13.300049,3.7999878 13.300049,2.5999756 13.300049,1.1999512 14.5,0 16,0z"
            //};
            //TopFunctionList.Add(m);
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
                Icon = "M16,8.5C11.899994,8.5 8.5,11.899994 8.5,16 8.5,20.100006 11.899994,23.5 16,23.5 20.099976,23.5 23.5,20.100006 23.5,16 23.5,11.899994 20.099976,8.5 16,8.5z M14.099976,0L17.899994,0 17.899994,4.8999939C19.599976,5.1999817,21.199982,5.8999939,22.5,6.7999878L26,3.2999878 28.699982,6 25.199982,9.5C26.199982,10.799988,26.799988,12.399994,27.099976,14.100006L32,14.100006 32,17.899994 27.099976,17.899994C26.799988,19.600006,26.099976,21.199982,25.199982,22.5L28.699982,26 26,28.699982 22.5,25.199982C21.199982,26.199982,19.599976,26.799988,17.899994,27.100006L17.899994,32 14.099976,32 14.099976,27.100006C12.399994,26.799988,10.799988,26.100006,9.5,25.199982L6,28.699982 3.2999878,26 6.7999878,22.5C5.7999878,21.199982,5.1999817,19.600006,4.8999939,17.899994L0,17.899994 0,14.100006 4.8999939,14.100006C5.1999817,12.399994,5.8999939,10.799988,6.7999878,9.5L3.2999878,6 6,3.2999878 9.5,6.7999878C10.799988,5.7999878,12.399994,5.1999817,14.099976,4.8999939z"
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