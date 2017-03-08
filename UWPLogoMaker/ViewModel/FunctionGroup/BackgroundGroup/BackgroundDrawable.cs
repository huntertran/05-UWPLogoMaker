using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI;

namespace UWPLogoMaker.ViewModel.FunctionGroup.BackgroundGroup
{
    public abstract class BackgroundDrawable : IBackgroundDrawable, INotifyPropertyChanged
    {
        protected BackgroundDrawable(BackgroundViewModel backgroundViewModel)
        {
            BackgroundVm = backgroundViewModel;
        }

        public BackgroundViewModel BackgroundVm { get; set; }

        public abstract Color CurrentColor { get; set; }

        public void Update()
        {
            // empty method for inherited class can implement themself
        }

        public void ChangeColor()
        {
            // empty method for inherited class can implement themself
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
