using System.Collections.ObjectModel;
using Microsoft.Graphics.Canvas.Brushes;

namespace UWPLogoMaker.ViewModel.FunctionGroup.BackgroundGroup
{
    public class GradientColorBackgroundViewModel : BaseViewModel
    {
        public BackgroundViewModel BackgroundVm;
        private ObservableCollection<CanvasGradientStop> _canvasGradientStops;
        private CanvasGradientStop _selectedGradientStop;
        private bool _isLinear;

        public ObservableCollection<CanvasGradientStop> CanvasGradientStops
        {
            get { return _canvasGradientStops; }
            set
            {
                if (Equals(value, _canvasGradientStops)) return;
                _canvasGradientStops = value;
                OnPropertyChanged();
            }
        }

        public CanvasGradientStop SelectedGradientStop
        {
            get { return _selectedGradientStop; }
            set
            {
                if (value.Equals(_selectedGradientStop)) return;
                _selectedGradientStop = value;
                OnPropertyChanged();
            }
        }

        public bool IsLinear
        {
            get { return _isLinear; }
            set
            {
                if (value == _isLinear) return;
                _isLinear = value;
                OnPropertyChanged();
            }
        }

        public GradientColorBackgroundViewModel(BackgroundViewModel backgroundVm)
        {
            BackgroundVm = backgroundVm;
            IsLinear = true;
        }
    }
}