namespace UniversalLogoMaker.Helpers
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class Observable : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        ////private SynchronizationContext _synchronizationContext = SynchronizationContext.Current;

        protected void Set<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }

            storage = value;
            OnPropertyChanged(propertyName);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
