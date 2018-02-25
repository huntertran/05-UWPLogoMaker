namespace UniversalLogoMaker3.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using Helpers;
    using Models;
    using Services;

    public class AddSizeViewModel : Observable
    {
        private ObservableCollection<LogoObject> _logoObjectList;
        private Database _customData;

        public ObservableCollection<LogoObject> LogoObjectList
        {
            get => _logoObjectList;
            set => Set(ref _logoObjectList, value);
        }

        public Database CustomData
        {
            get => _customData;
            set => Set(ref _customData, value);
        }

        public async Task LoadData()
        {
            CustomData = await StorageService.Json2Object<Database>("custom.dat") ?? new Database
            {
                PlatformList = new ObservableCollection<Platform>()
            };
        }

        public async Task AddNewPlatform(string platformName, string platformAbbreviation, string size)
        {
            Platform p = new Platform
            {
                Name = platformName,
                Icon = platformAbbreviation,
                SaveLogoList = new List<LogoObject>()
            };

            string[] text = size.Split(';');
            foreach (string s in text)
            {
                if (!string.IsNullOrEmpty(s))
                {
                    string[] t = s.Trim().Split(':');
                    LogoObject l = new LogoObject(t[0], Convert.ToInt32(t[1]), Convert.ToInt32(t[2]));
                    p.SaveLogoList.Add(l);
                }
            }

            CustomData.PlatformList.Add(p);
            //Save all change to file
            await StorageService.Object2Json(CustomData, "custom.dat");
        }

        public void ParseLogoObject(string data)
        {
            if (LogoObjectList == null)
            {
                LogoObjectList = new ObservableCollection<LogoObject>();
            }
            LogoObjectList.Clear();
            string[] text = data.Split(';');
            foreach (string s in text)
            {
                try
                {
                    if (!string.IsNullOrEmpty(s))
                    {
                        string[] t = s.Trim().Split(':');
                        LogoObject l = new LogoObject(t[0], Convert.ToInt32(t[1]), Convert.ToInt32(t[2]));
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
