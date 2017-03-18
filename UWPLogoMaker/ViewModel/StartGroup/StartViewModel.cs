namespace UWPLogoMaker.ViewModel.StartGroup
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Windows.ApplicationModel.Resources.Core;
    using Windows.UI.Xaml.Controls;
    using Model;
    using Newtonsoft.Json.Linq;
    using Utilities;
    using View.FunctionGroup;
    using View.NewSizeGroup;
    using View.SettingGroup;

    public class StartViewModel : PropertyChangedImplementation
    {
        private IList<MenuListItem> _bottomFunctionList;
        private IList<MenuListItem> _topFunctionList;
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

        public IList<MenuListItem> TopFunctionList
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
                PageType = typeof(MainPage),
                IsEnabled = true,
                Icon = ResourceManager.Current.MainResourceMap.GetValue("CommonResources/RenderAllSizeIcon", new ResourceContext()).ValueAsString
            };
            TopFunctionList.Add(m);

            m = new MenuListItem
            {
                Name = ResourceManager.Current.MainResourceMap.GetValue("Resources/StartViewModel_AddBottomFunctionList_Add_new_size", new ResourceContext()).ValueAsString,
                PageType = typeof(NewSizePage),
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
                PageType = typeof(SettingPage),
                IsEnabled = false,
                Icon = ResourceManager.Current.MainResourceMap.GetValue("CommonResources/SettingIcon", new ResourceContext()).ValueAsString
            };
            
            BottomFunctionList.Add(m);
        }

        private void InitializeData()
        {
            JObject jObject = JObject.Parse(ResourceManager.Current.MainResourceMap.GetValue("CommonResources/DefaultSizes",
                new ResourceContext()).ValueAsString);
            Data = jObject.ToObject<Database>();
        }

        public void NavigateToFunction(Frame frame, Type pageType)
        {
            frame.Navigate(pageType);
        }
    }
}