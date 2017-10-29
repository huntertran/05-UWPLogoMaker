namespace UniversalLogoMaker.Infrastructure
{
    using Annotations;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class NotifyPropertyChangedImplementation : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
