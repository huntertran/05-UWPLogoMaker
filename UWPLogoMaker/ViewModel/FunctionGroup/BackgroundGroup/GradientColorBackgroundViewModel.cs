using Microsoft.Graphics.Canvas.Brushes;

namespace UWPLogoMaker.ViewModel.FunctionGroup.BackgroundGroup
{
    public class GradientColorBackgroundViewModel : BaseViewModel
    {
        private CanvasGradientStop[] _canvasGradientStops;
        private CanvasGradientStop _selectedGradientStop;

        public CanvasGradientStop[] CanvasGradientStops
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
    }
}