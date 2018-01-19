namespace UWPLogoMaker.ViewModel.NewSizeGroup
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using Model;
    using Utilities;

    public class NewSizeViewModel : PropertyChangedImplementation
    {
        private ObservableCollection<LogoObject> _logoObjectList;

        public ObservableCollection<LogoObject> LogoObjectList
        {
            get => _logoObjectList;
            set
            {
                if (Equals(value, _logoObjectList)) return;
                _logoObjectList = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadData()
        {
            StaticData.StartVm.CustomData = await StorageHelper.Json2Object<Database>("custom.dat") ?? new Database
            {
                PlatformList = new ObservableCollection<Platform>()
            };
        }

        public async Task AddNewPlatform(string platformName, string platformAbbreviation, string size)
        {
            var p = new Platform
            {
                Name = platformName,
                Icon = platformAbbreviation,
                SaveLogoList = new List<LogoObject>()
            };

            var text = size.Split(';');
            foreach (var s in text)
            {
                if (!string.IsNullOrEmpty(s))
                {
                    var t = s.Trim().Split(':');
                    var l = new LogoObject(t[0], Convert.ToInt32(t[1]), Convert.ToInt32(t[2]));
                    p.SaveLogoList.Add(l);
                }
            }

            StaticData.StartVm.CustomData.PlatformList.Add(p);
            //Save all change to file
            await StorageHelper.Object2Json(StaticData.StartVm.CustomData, "custom.dat");
        }

        public void ParseLogoObject(string data)
        {
            if (LogoObjectList == null)
            {
                LogoObjectList = new ObservableCollection<LogoObject>();
            }

            LogoObjectList.Clear();
            var text = data.Split(';');
            foreach (var s in text)
            {
                try
                {
                    if (!string.IsNullOrEmpty(s))
                    {
                        var t = s.Trim().Split(':');
                        var l = new LogoObject(t[0], Convert.ToInt32(t[1]), Convert.ToInt32(t[2]));
                        LogoObjectList.Add(l);
                    }
                }
                catch
                {
                    // ignored
                }
            }
        }
    }
}