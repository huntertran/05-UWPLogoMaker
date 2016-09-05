using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using UWPLogoMaker.Model;
using UWPLogoMaker.Utilities;
using Platform = UWPLogoMaker.Model.Platform;

namespace UWPLogoMaker.ViewModel.NewSizeGroup
{
    public class NewSizeViewModel : BaseViewModel
    {
        private ObservableCollection<LogoObject> _logoObjectList;

        public ObservableCollection<LogoObject> LogoObjectList
        {
            get { return _logoObjectList; }
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