﻿namespace UniversalLogoMaker.ViewModels
{
    using System.Numerics;
    using Windows.UI;
    using Helpers;
    using Microsoft.Graphics.Canvas.Effects;

    public class GenerateViewModel : Observable
    {
        private Color _selectedColor = Colors.AliceBlue;
        private float _x;
        private float _y;
        private double _rectX;
        private double _rectY;
        private double _rectWidth;
        private double _rectHeight;
        private float _zoomFactor;
        private float _zoomFactorBefore;
        private Transform2DEffect _effect = new Transform2DEffect();
        private double _maxWidth;
        private double _maxHeight;

        public Color SelectedColor
        {
            get => _selectedColor;
            set => Set(ref _selectedColor, value);
        }

        public float X
        {
            get => _x;
            set => Set(ref _x, value);
        }

        public float Y
        {
            get => _y;
            set => Set(ref _y, value);
        }

        public double RectX
        {
            get => _rectX;
            set => Set(ref _rectX, value);
        }

        public double RectY
        {
            get => _rectY;
            set => Set(ref _rectY, value);
        }

        public double RectWidth
        {
            get => _rectWidth;
            set => Set(ref _rectWidth, value);
        }

        public double RectHeight
        {
            get => _rectHeight;
            set => Set(ref _rectHeight, value);
        }

        public float ZoomFactor
        {
            get => _zoomFactor;
            set
            {
                if (value.Equals(_zoomFactor)) return;
                _zoomFactor = value;
                ZoomFactorBefore = _zoomFactor * 100;
                Effect.TransformMatrix = Matrix3x2.CreateScale(new Vector2(ZoomFactor));
                OnPropertyChanged(nameof(Effect));
                OnPropertyChanged(nameof(ZoomFactor));
            }
        }

        public float ZoomFactorBefore
        {
            get => _zoomFactorBefore;
            set
            {
                if (value.Equals(_zoomFactorBefore)) return;
                _zoomFactorBefore = value;
                ZoomFactor = _zoomFactorBefore / 100;
                OnPropertyChanged(nameof(ZoomFactorBefore));
            }
        }

        public double MaxWidth
        {
            get => _maxWidth;
            set => Set(ref _maxWidth, value);
        }

        public double MaxHeight
        {
            get => _maxHeight;
            set => Set(ref _maxHeight, value);
        }

        public Transform2DEffect Effect
        {
            get => _effect;
            set => Set(ref _effect, value);
        }
    }
}
