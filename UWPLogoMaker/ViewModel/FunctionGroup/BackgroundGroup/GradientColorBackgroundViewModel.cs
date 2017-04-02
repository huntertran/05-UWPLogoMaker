namespace UWPLogoMaker.ViewModel.FunctionGroup.BackgroundGroup
{
    using System.Collections.ObjectModel;
    using Microsoft.Graphics.Canvas.Brushes;

    public class GradientColorBackgroundViewModel : BackgroundDrawable
    {
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

        public GradientColorBackgroundViewModel(BackgroundViewModel backgroundVm) : base(backgroundVm)
        {
            CanvasGradientStops = new ObservableCollection<CanvasGradientStop>();
            IsLinear = true;
        }

        public void AddGradientStop()
        {
            CanvasGradientStop c = new CanvasGradientStop();
            CanvasGradientStops.Add(c);
        }

        public void RemoveGradientStop(CanvasGradientStop c)
        {
            CanvasGradientStops.Remove(c);
        }
    }
}