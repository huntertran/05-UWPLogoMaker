namespace UniversalLogoMaker.ViewModels
{
    using Infrastructure;
    using Models;

    public class StartViewModel : NotifyPropertyChangedImplementation
    {
        private Database _data;
        private Database _customDatabase;

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

        public Database CustomDatabase
        {
            get => _customDatabase;
            set
            {
                if (Equals(value, _customDatabase)) return;
                _customDatabase = value;
                OnPropertyChanged();
            }
        }
    }
}
