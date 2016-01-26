using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using UWPLogoMaker.Annotations;
using UWPLogoMaker.Model;
using UWPLogoMaker.Utilities;
using UWPLogoMaker.View.NewSizeGroup;
using UWPLogoMaker.View.PlatformGroup;
using UWPLogoMaker.View.SettingGroup;

namespace UWPLogoMaker.ViewModel.StartGroup
{
    public class StartViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<MenuListItem> _bottomFunctionList;
        private string _pageName;
        private Database _data;
        private Database _customData;

        public ObservableCollection<MenuListItem> BottomFunctionList
        {
            get { return _bottomFunctionList; }
            set
            {
                if (Equals(value, _bottomFunctionList)) return;
                _bottomFunctionList = value;
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
            BottomFunctionList = new ObservableCollection<MenuListItem>();

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


            MenuListItem m = new MenuListItem
            {
                Name = "Add new size",
                MenuF = MenuFunc.Add,
                Icon =
                    "M19.833,0L32.5,0 32.5,19.833999 52.334,19.833999 52.334,32.500999 32.5,32.500999 32.5,52.333 19.833,52.333 19.833,32.500999 0,32.500999 0,19.833999 19.833,19.833999z"
            };
            BottomFunctionList.Add(m);

            m = new MenuListItem
            {
                Name = "Setting",
                MenuF = MenuFunc.Settings,
                Icon =
                    "M383.482,203.57C284.07,203.57 203.534,284.142 203.534,383.554 203.534,482.93 284.07,563.502 383.482,563.502 482.894,563.502 563.431,482.93 563.431,383.554 563.431,284.142 482.894,203.57 383.482,203.57z M338.073,0L428.927,0 428.927,117.641C469.52,124.544,507.055,140.471,539.377,163.41L622.574,80.1771 686.823,144.462 603.627,227.659C626.565,259.982,642.457,297.481,649.396,338.073L767,338.073 767,428.963 649.432,428.963C642.492,469.52,626.565,507.091,603.627,539.378L686.823,622.574 622.538,686.788 539.377,603.626C507.055,626.601,469.555,642.528,428.927,649.432L428.927,767 338.073,767 338.073,649.432C297.409,642.528,259.909,626.601,227.587,603.626L144.426,686.788 80.2128,622.574 163.374,539.378C140.399,507.091,124.508,469.591,117.569,428.963L0,428.963 0,338.073 117.569,338.073C124.508,297.481,140.435,259.982,163.374,227.659L80.1766,144.462 144.426,80.2133 227.623,163.41C259.909,140.471,297.445,124.544,338.073,117.641z"
            };
            BottomFunctionList.Add(m);
            Debug.WriteLine("End Initialize size");
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
                IsEnabled = true
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
                case MenuFunc.Uwp:
                {
                    frame.Navigate(typeof (UwpPage));
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
                    frame.Navigate(typeof (UwpPage));
                    break;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}