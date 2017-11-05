namespace UniversalLogoMaker.ViewModels
{
    using Infrastructure;
    using Models;
    using Newtonsoft.Json.Linq;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Utilities;
    using Windows.ApplicationModel.Resources.Core;

    public class StartViewModel : NotifyPropertyChangedImplementation
    {
        private Database _data;
        private Database _customData;
        private string _status;
        private bool _isShowLastStep;

        public Database Data
        {
            get => _data;
            set
            {
                if (Equals(value, _data)) return;
                _data = value;
                OnPropertyChanged();
            }
        }

        public Database CustomData
        {
            get => _customData;
            set
            {
                if (Equals(value, _customData)) return;
                _customData = value;
                OnPropertyChanged();
            }
        }

        public string Status
        {
            get => _status;
            set
            {
                if (value == _status) return;
                _status = value;
                OnPropertyChanged();
            }
        }

        public bool IsShowLastStep
        {
            get => _isShowLastStep;
            set
            {
                if (value == _isShowLastStep) return;
                _isShowLastStep = value;
                OnPropertyChanged();
            }
        }

        public StartViewModel()
        {
            Status = "Choose an image to start";
            IsShowLastStep = true;
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

            Debug.WriteLine("End Initialize size");
        }

        private void InitializeData()
        {
            JObject jObject = JObject.Parse(
                ResourceManager.Current.MainResourceMap.GetValue("CommonResources/DefaultSizes",
                new ResourceContext())
                .ValueAsString);

            Data = jObject.ToObject<Database>();
        }
    }
}
