using System;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml.Input;
using GoogleAnalytics;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.Geometry;
using UWPLogoMaker.Interfaces;
using UWPLogoMaker.Model;
using UWPLogoMaker.Utilities;
using UWPLogoMaker.ViewModel.FunctionGroup.BackgroundGroup;
using UWPLogoMaker.ViewModel.StartGroup;

namespace UWPLogoMaker.ViewModel.FunctionGroup
{
    public class MainViewModel : PropertyChangedImplementation
    {
        public StorageFile File;

        public StartViewModel StartVm = StaticData.StartVm;

        private bool _isShowingProgress;

        private bool _isManualAdjustSquareImage;

        private bool _isShowCenterLine;

        #region Test Win2D

        private readonly CanvasDevice _device = CanvasDevice.GetSharedDevice();
        private CanvasBitmap _userBitmap;
        private CanvasBitmap _transperentBitmap;

        private float _x;
        private float _y;

        private float _plexibleX;

        private double _recX;
        private double _recY;
        private double _rectangleWidth;
        private double _rectangleHeight;

        private float _zoomF;
        private float _zoomFBefore;

        private double _maxWidth;
        private double _maxHeight;

        private bool _isCaculation;

        public CanvasBitmap UserBitmap
        {
            get { return _userBitmap; }
            set
            {
                if (value.Equals(_userBitmap)) return;
                _userBitmap = value;
                OnPropertyChanged();
            }
        }

        public float X
        {
            get { return _x; }
            set
            {
                if (value.Equals(_x)) return;
                _x = value;
                OnPropertyChanged();
            }
        }

        public float Y
        {
            get { return _y; }
            set
            {
                if (value.Equals(_y)) return;
                _y = value;
                OnPropertyChanged();
            }
        }

        public float PlexibleX
        {
            get { return _plexibleX; }
            set
            {
                if (value.Equals(_plexibleX)) return;
                _plexibleX = value;
                OnPropertyChanged();
            }
        }

        public double RecX
        {
            get { return _recX; }
            set
            {
                if (value.Equals(_recX)) return;
                _recX = value;
                OnPropertyChanged();
            }
        }

        public double RecY
        {
            get { return _recY; }
            set
            {
                if (value.Equals(_recY)) return;
                _recY = value;
                OnPropertyChanged();
            }
        }

        public double RectangleWidth
        {
            get { return _rectangleWidth; }
            set
            {
                if (value.Equals(_rectangleWidth)) return;
                _rectangleWidth = value;
                OnPropertyChanged();
            }
        }

        public double RectangleHeight
        {
            get { return _rectangleHeight; }
            set
            {
                if (value.Equals(_rectangleHeight)) return;
                _rectangleHeight = value;
                OnPropertyChanged();
            }
        }

        public float ZoomF
        {
            get { return _zoomF; }
            set
            {
                if (value.Equals(_zoomF)) return;
                _zoomF = value;
                ZoomFBefore = _zoomF*100;
                OnPropertyChanged();
            }
        }

        public float ZoomFBefore
        {
            get { return _zoomFBefore; }
            set
            {
                if (value.Equals(_zoomFBefore)) return;
                _zoomFBefore = value;
                ZoomF = _zoomFBefore/100;
                OnPropertyChanged();
            }
        }

        public double MaxWidth
        {
            get { return _maxWidth; }
            set
            {
                if (value.Equals(_maxWidth)) return;
                _maxWidth = value;
                OnPropertyChanged();
            }
        }

        public double MaxHeight
        {
            get { return _maxHeight; }
            set
            {
                if (value.Equals(_maxHeight)) return;
                _maxHeight = value;
                OnPropertyChanged();
            }
        }

        public bool IsCaculation
        {
            get { return _isCaculation; }
            set
            {
                if (value == _isCaculation) return;
                _isCaculation = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Square Data

        private float _sX;
        private float _sY;
        private double _sRecX;
        private double _sRecY;
        private double _squareRectangleWidth;
        private double _squareRectangleHeight;

        private float _sZoomF;
        private float _sZoomFBefore;

        private double _sMaxWidth;
        private double _sMaxHeight;

        private bool _sIsCaculation;

        // ReSharper disable once InconsistentNaming
        public float SX
        {
            get { return _sX; }
            set
            {
                if (value.Equals(_sX)) return;
                _sX = value;
                OnPropertyChanged();
            }
        }

        // ReSharper disable once InconsistentNaming
        public float SY
        {
            get { return _sY; }
            set
            {
                if (value.Equals(_sY)) return;
                _sY = value;
                OnPropertyChanged();
            }
        }

        public double SRecX
        {
            get { return _sRecX; }
            set
            {
                if (value.Equals(_sRecX)) return;
                _sRecX = value;
                OnPropertyChanged();
            }
        }

        public double SRecY
        {
            get { return _sRecY; }
            set
            {
                if (value.Equals(_sRecY)) return;
                _sRecY = value;
                OnPropertyChanged();
            }
        }

        public double SquareRectangleWidth
        {
            get { return _squareRectangleWidth; }
            set
            {
                if (value.Equals(_squareRectangleWidth)) return;
                _squareRectangleWidth = value;
                OnPropertyChanged();
            }
        }

        public double SquareRectangleHeight
        {
            get { return _squareRectangleHeight; }
            set
            {
                if (value.Equals(_squareRectangleHeight)) return;
                _squareRectangleHeight = value;
                OnPropertyChanged();
            }
        }

        public float SZoomF
        {
            get { return _sZoomF; }
            set
            {
                if (value.Equals(_sZoomF)) return;
                _sZoomF = value;
                SZoomFBefore = _sZoomF*100;
                OnPropertyChanged();
            }
        }

        public float SZoomFBefore
        {
            get { return _sZoomFBefore; }
            set
            {
                if (value.Equals(_sZoomFBefore)) return;
                _sZoomFBefore = value;
                SZoomF = _sZoomFBefore/100;
                OnPropertyChanged();
            }
        }

        public double SMaxWidth
        {
            get { return _sMaxWidth; }
            set
            {
                if (value.Equals(_sMaxWidth)) return;
                _sMaxWidth = value;
                OnPropertyChanged();
            }
        }

        public double SMaxHeight
        {
            get { return _sMaxHeight; }
            set
            {
                if (value.Equals(_sMaxHeight)) return;
                _sMaxHeight = value;
                OnPropertyChanged();
            }
        }

        public bool SIsCaculation
        {
            get { return _sIsCaculation; }
            set
            {
                if (value == _sIsCaculation) return;
                _sIsCaculation = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region ViewModels

        private BackgroundViewModel _backgroundVm;

        public BackgroundViewModel BackgroundVm
        {
            get { return _backgroundVm; }
            set
            {
                if (Equals(value, _backgroundVm)) return;
                _backgroundVm = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public bool IsShowingProgress
        {
            get { return _isShowingProgress; }
            set
            {
                if (value == _isShowingProgress) return;
                _isShowingProgress = value;
                OnPropertyChanged();
            }
        }

        public bool IsManualAdjustSquareImage
        {
            get { return _isManualAdjustSquareImage; }
            set
            {
                if (value == _isManualAdjustSquareImage) return;
                _isManualAdjustSquareImage = value;
                OnPropertyChanged();
            }
        }

        public bool IsShowCenterLine
        {
            get { return _isShowCenterLine; }
            set
            {
                if (value == _isShowCenterLine) return;
                _isShowCenterLine = value;
                OnPropertyChanged();
            }
        }

        private CanvasRenderTarget _renderTarget;
        private CanvasRenderTarget _sRenderTarget;
        private CanvasRenderTarget _customRenderTarget;

        public CanvasRenderTarget RenderTarget
        {
            get { return _renderTarget; }
            set
            {
                if (Equals(value, _renderTarget)) return;
                _renderTarget = value;
                OnPropertyChanged();
            }
        }

        public CanvasRenderTarget SRenderTarget
        {
            get { return _sRenderTarget; }
            set
            {
                if (Equals(value, _sRenderTarget)) return;
                _sRenderTarget = value;
                OnPropertyChanged();
            }
        }

        public CanvasRenderTarget CustomRenderTarget
        {
            get { return _customRenderTarget; }
            set
            {
                if (Equals(value, _customRenderTarget)) return;
                _customRenderTarget = value;
                OnPropertyChanged();
            }
        }

        public IPreviewView View { get; set; }

        #region interfaces

        public void InvalidateCanvasControl()
        {
            View?.InvalidateCanvasControl();
        }

        #endregion

        public MainViewModel()
        {
            BackgroundVm = new BackgroundViewModel(this);
        }

        private Color GetCurentColor()
        {
            //Get current color
            Color color = new Color
            {
                A = BackgroundVm.ColorBackgroundVm.CurrentColor.A,
                R = BackgroundVm.ColorBackgroundVm.CurrentColor.R,
                G = BackgroundVm.ColorBackgroundVm.CurrentColor.G,
                B = BackgroundVm.ColorBackgroundVm.CurrentColor.B
            };

            return color;
        }

        private void SetRectangleDimension()
        {
            RectangleWidth = UserBitmap.SizeInPixels.Width * ZoomF;
            RectangleHeight = UserBitmap.SizeInPixels.Height * ZoomF;

            if (IsManualAdjustSquareImage)
            {
                SquareRectangleWidth = UserBitmap.SizeInPixels.Width * SZoomF;
                SquareRectangleHeight = UserBitmap.SizeInPixels.Height * SZoomF;
            }
            else
            {
                SquareRectangleWidth = RectangleWidth;
                SquareRectangleHeight = RectangleHeight;
            }
        }

        public async Task DoTheGenerateWin2DTask()
        {
            Color currentColor = GetCurentColor();

            SetRectangleDimension();

            //Get save mode
            int saveMode = SettingManager.GetSaveMode();
            if (saveMode == 0 || saveMode == 1)
            {
                //Default
                SettingManager.SetSaveMode(2);
                saveMode = 2;
            }

            if (saveMode == 3)
            {
                //Check if folder is deleted
                try
                {
                    string token = SettingManager.GetSaveToken();
                    StaticData.SaveFolder = await StorageApplicationPermissions.FutureAccessList.GetFolderAsync(token);
                }
                catch
                {
                    //Set default save mode
                    SettingManager.SetSaveMode(2);
                    saveMode = 2;

                    MessageDialog msg = new MessageDialog("Your selected folder has been deleted or moved", "Warning");
                    await msg.ShowAsync();
                }
            }

            switch (saveMode)
            {
                case 1:
                    //Save in the same folder. Not working
                    StaticData.SaveFolder = await File.GetParentAsync();
                    break;
                case 2:
                    //Choose where to save
                    FolderPicker folderPicker = new FolderPicker
                    {
                        SuggestedStartLocation = PickerLocationId.PicturesLibrary
                    };
                    folderPicker.FileTypeFilter.Add(".jpeg");
                    folderPicker.FileTypeFilter.Add(".jpg");
                    folderPicker.FileTypeFilter.Add(".png");
                    folderPicker.FileTypeFilter.Add(".bmp");
                    folderPicker.FileTypeFilter.Add(".tiff");
                    folderPicker.FileTypeFilter.Add(".gif");
                    StaticData.SaveFolder = await folderPicker.PickSingleFolderAsync();
                    if (StaticData.SaveFolder == null)
                    {
                        MessageDialog msg = new MessageDialog("Please choose a folder to save");
                        await msg.ShowAsync();
                        IsShowingProgress = false;
                        return;
                    }
                    break;
                case 3:
                    //Save in specific folder
                    string token = SettingManager.GetSaveToken();
                    StaticData.SaveFolder = await StorageApplicationPermissions.FutureAccessList.GetFolderAsync(token);
                    break;
            }

            foreach (var platform in StaticData.StartVm.Data.PlatformList.Where(platform => platform.IsEnabled))
            {
                //Send google analytics
                EasyTracker.GetTracker().SendEvent("platform", platform.Name, null, 0);

                //Create folder
                var platformFolder = await StaticData.SaveFolder.CreateFolderAsync(platform.Name,
                    CreationCollisionOption.OpenIfExists);

                await RenderPlatform(platform, currentColor, platformFolder);
            }
            if (StaticData.StartVm.CustomData != null && StaticData.StartVm.CustomData.PlatformList != null)
            {
                foreach (
                    var platform in StaticData.StartVm.CustomData.PlatformList.Where(platform => platform.IsEnabled))
                {
                    //Send google analytics
                    EasyTracker.GetTracker().SendEvent("platform", platform.Name, null, 0);

                    //Create folder
                    var platformFolder =
                        await
                            StaticData.SaveFolder.CreateFolderAsync(platform.Name,
                                CreationCollisionOption.OpenIfExists);

                    await RenderPlatform(platform, currentColor, platformFolder);
                }
            }

            IsShowingProgress = false;
        }

        private async Task RenderPlatform(Platform p, Color c, StorageFolder platformFolder)
        {
            foreach (LogoObject logoObject in p.SaveLogoList)
            {
                // ReSharper disable once PossibleLossOfFraction
                double ratio = (double) logoObject.Width/logoObject.Height;
                // ReSharper disable once PossibleLossOfFraction
                bool isCustom = ratio != ((double) 620/300) && ratio != 1;
                if (isCustom)
                {
                    RenderCustomImage(c, logoObject.Width, logoObject.Height);
                }
                else
                {
                    RenderImage(c, logoObject.Width, logoObject.Height);
                }
                var savedFile =
                    await
                        platformFolder.CreateFileAsync(logoObject.FileName + ".scale-" + logoObject.Scale + ".png",
                            CreationCollisionOption.ReplaceExisting);
                if (isCustom)
                {
                    using (var outStream = await savedFile.OpenAsync(FileAccessMode.ReadWrite))
                    {
                        await CustomRenderTarget.SaveAsync(outStream, CanvasBitmapFileFormat.Png);
                    }
                    continue;
                }
                if (logoObject.Width == logoObject.Height && IsManualAdjustSquareImage)
                {
                    using (var outStream = await savedFile.OpenAsync(FileAccessMode.ReadWrite))
                    {
                        await SRenderTarget.SaveAsync(outStream, CanvasBitmapFileFormat.Png);
                    }
                }
                else
                {
                    using (var outStream = await savedFile.OpenAsync(FileAccessMode.ReadWrite))
                    {
                        await RenderTarget.SaveAsync(outStream, CanvasBitmapFileFormat.Png);
                    }
                }
            }
        }

        private void RenderImage(Color c, double width, double height)
        {
            double ratio = height/300;
            var isSq = width == height;

            Vector2 vector2 = new Vector2((float) (ZoomF*ratio));

            var effect = new Transform2DEffect
            {
                Source = UserBitmap,
                InterpolationMode = CanvasImageInterpolation.HighQualityCubic,
                TransformMatrix = Matrix3x2.CreateScale(vector2)
            };
            
            if (isSq && IsManualAdjustSquareImage)
            {
                effect = new Transform2DEffect
                {
                    Source = UserBitmap,
                    InterpolationMode = CanvasImageInterpolation.HighQualityCubic,
                    TransformMatrix = Matrix3x2.CreateScale(new Vector2((float)(SZoomF * ratio)))
                };
            }

            //Render target: Main render
            if (isSq)
            {
                PlexibleX = X - 160;
                RenderTarget = new CanvasRenderTarget(_device, (float) (300*ratio), (float) (300*ratio), 96);
                if (IsManualAdjustSquareImage)
                {
                    //Square
                    SRenderTarget = new CanvasRenderTarget(_device, (float) (300*ratio), (float) (300*ratio), 96);
                }
            }
            else if (width > height)
            {
                RenderTarget = new CanvasRenderTarget(_device, (float) (620*ratio), (float) (300*ratio), 96);
                PlexibleX = X;
            }

            if (isSq && IsManualAdjustSquareImage)
            {
                using (var ds = SRenderTarget.CreateDrawingSession())
                {
                    //Clear the color
                    ds.Clear(c);

                    //Draw the user image to target
                    ds.DrawImage(effect, (float) (SX*ratio), (float) (SY*ratio),
                        new Rect(SRecX, SRecY, SquareRectangleWidth*ratio, SquareRectangleHeight*ratio), 1.0f,
                        CanvasImageInterpolation.HighQualityCubic);
                }
            }
            else
            {
                using (var ds = RenderTarget.CreateDrawingSession())
                {
                    //Clear the color
                    ds.Clear(c);

                    //Draw the user image to target
                    ds.DrawImage(effect, (float) (PlexibleX*ratio), (float) (Y*ratio),
                        new Rect(RecX, RecY, RectangleWidth*ratio, RectangleHeight*ratio), 1.0f, CanvasImageInterpolation.HighQualityCubic);
                }
            }
        }

        private void RenderCustomImage(Color c, double width, double height)
        {
            Debug.WriteLine("Render custom image | Width: " + width + " | Height: " + height);
            double ratio;
            float x;
            float y;
            if (width > height)
            {
                //Use height
                ratio = height/300;
                x = (float) ((width/2) - (height/2));
                y = 0;
            }
            else
            {
                //Use width
                ratio = width/300;
                y = (float) ((height/2) - (width/2));
                x = 0;
            }

            var effect = new Transform2DEffect
            {
                Source = UserBitmap,
                InterpolationMode = CanvasImageInterpolation.HighQualityCubic,
                TransformMatrix = Matrix3x2.CreateScale(new Vector2((float)(SZoomF * ratio)))
            };
            
            CanvasRenderTarget sqRenderTarget = new CanvasRenderTarget(_device, (float) (300*ratio), (float) (300*ratio),
                96);
            PlexibleX = X - 160;
            if (IsManualAdjustSquareImage)
            {
                using (var ds = sqRenderTarget.CreateDrawingSession())
                {
                    //Clear the color
                    ds.Clear(c);

                    //Draw the user image to target
                    ds.DrawImage(effect, (float) (SX*ratio), (float) (SY*ratio),
                        new Rect(SRecX, SRecY, SquareRectangleWidth*ratio, SquareRectangleHeight*ratio), 1.0f,
                        CanvasImageInterpolation.HighQualityCubic);
                }
            }
            else
            {
                using (var ds = sqRenderTarget.CreateDrawingSession())
                {
                    //Clear the color
                    ds.Clear(c);

                    //Draw the user image to target
                    ds.DrawImage(effect, (float) (PlexibleX*ratio), (float) (Y*ratio),
                        new Rect(RecX, RecY, RectangleWidth*ratio, RectangleHeight*ratio), 1.0f, CanvasImageInterpolation.HighQualityCubic);
                }
            }

            CustomRenderTarget = new CanvasRenderTarget(_device, (float) width, (float) height, 96);
            using (var ds = CustomRenderTarget.CreateDrawingSession())
            {
                ds.Clear(c);
                ds.DrawImage(sqRenderTarget, x, y,
                    new Rect(SRecX, SRecY, (float) (300*ratio), (float) (300*ratio)), 1.0f,
                    CanvasImageInterpolation.HighQualityCubic);
            }
        }

        public void DisplayPreview()
        {
            if (File == null)
            {
                return;
            }

            //Get current color
            Color currentColor = GetCurentColor();

            if (IsCaculation)
            {
                //Send message to output
                Debug.WriteLine("Re caculate param");

                if (UserBitmap.SizeInPixels.Width <= UserBitmap.SizeInPixels.Height)
                {
                    ZoomF = (float) 300/UserBitmap.SizeInPixels.Height;
                }
                else
                {
                    ZoomF = (float) 620/UserBitmap.SizeInPixels.Width;
                }

                X = 310 - ((UserBitmap.SizeInPixels.Width*ZoomF)/2);
                Y = 150 - ((UserBitmap.SizeInPixels.Height*ZoomF)/2);

                IsCaculation = false;
            }

            RectangleWidth = UserBitmap.SizeInPixels.Width*ZoomF;
            RectangleHeight = UserBitmap.SizeInPixels.Height*ZoomF;

            var effect = new Transform2DEffect
            {
                Source = UserBitmap,
                InterpolationMode = CanvasImageInterpolation.HighQualityCubic,
                TransformMatrix = Matrix3x2.CreateScale(new Vector2(ZoomF))
            };

            //Render target: Main render
            RenderTarget = new CanvasRenderTarget(_device, 620, 300, 96);
            using (var drawingSession = RenderTarget.CreateDrawingSession())
            {
                //Clear the color
                drawingSession.Clear(currentColor);

                //Draw transperent bitmap
                drawingSession.DrawImage(_transperentBitmap, 0, 0, new Rect(0, 0, 620, 300), 1.0f);

                //Fill the rectangle with color
                drawingSession.FillRectangle(0, 0, 620, 300, currentColor);

                //CreatePathLoop(ds);

                //ds.DrawGeometry(geometry, Colors.Red, 0);

                //Draw the user image to target
                drawingSession.DrawImage(effect, X, Y, new Rect(RecX, RecY, RectangleWidth, RectangleHeight), 1.0f,
                    CanvasImageInterpolation.HighQualityCubic);
            }
        }

        private Color GetRandomColor(int seed = 0)
        {
            var random = seed != 0 ? new Random(seed) : new Random();

            return Color.FromArgb((byte)random.Next(0, 255), (byte)random.Next(0, 255), (byte)random.Next(0, 255), (byte)random.Next(0, 255));
        }

        private CanvasGeometry CreatePath(Point point1, Point point2, Point point3)
        {
            var pathBuilder = new CanvasPathBuilder(_device);
            pathBuilder.BeginFigure((float) point1.X, (float) point1.Y);
            pathBuilder.AddLine((float)point2.X, (float)point2.Y);
            pathBuilder.AddLine((float)point3.X, (float)point3.Y);
            pathBuilder.EndFigure(CanvasFigureLoop.Closed);

            return CanvasGeometry.CreatePath(pathBuilder);
        }

        private void CreatePathLoop(CanvasDrawingSession ds)
        {
            int distance = 29;
            
            for (int j = 0; j < 25; j++)
            {
                for (int i = 1; i < 300; i = i + distance)
                {
                    Point point1 = new Point(i, i+(distance*j));
                    Point point2 = new Point(i + distance, i + distance + (distance * j));
                    Point point3 = new Point(i, i + distance + (distance * j));

                    var geometry = CreatePath(point1, point2, point3);
                    ds.FillGeometry(geometry, GetRandomColor(i));

                    point1 = new Point(i + (distance * j), i);
                    point2 = new Point(i + distance + (distance * j), i);
                    point3 = new Point(i + distance + (distance * j), i + distance);

                    geometry = CreatePath(point1, point2, point3);
                    ds.FillGeometry(geometry, GetRandomColor(j));
                }
            }
            for (int j = 0; j < 25; j++)
            {
                for (int i = 1; i < 300; i = i + distance)
                {
                    Point point1 = new Point(i + (distance * j), i);
                    Point point2 = new Point(i + distance + (distance * j), i + distance);
                    Point point3 = new Point(i + (distance * j), i + distance);

                    var geometry = CreatePath(point1, point2, point3);
                    ds.FillGeometry(geometry, GetRandomColor(i));

                    point1 = new Point(i, i + (distance * j));
                    point2 = new Point( i + distance, i + distance + (distance * j));
                    point3 = new Point( i + distance, i + (distance * j));

                    geometry = CreatePath(point1, point2, point3);
                    ds.FillGeometry(geometry, GetRandomColor(j));
                }
            }

            //var g = CreatePath();
            //ds.FillGeometry(g, Colors.Red);
        }

        public void DisplaySquarePreview()
        {
            if (!IsManualAdjustSquareImage)
            {
                SX = X - 160;
                SY = Y;

                SRecX = RecX;
                SRecY = RecY;
                SquareRectangleWidth = RectangleWidth;
                SquareRectangleHeight = RectangleHeight;

                SZoomF = ZoomF;

                SMaxWidth = MaxWidth;
                SMaxHeight = MaxHeight;
            }

            if (File == null)
            {
                return;
            }

            //Get current color
            Color currentColor = GetCurentColor();

            if (SIsCaculation)
            {
                //Send message to output
                Debug.WriteLine("Re caculate param");

                if (UserBitmap.SizeInPixels.Width <= UserBitmap.SizeInPixels.Height)
                {
                    SZoomF = (float) 300/UserBitmap.SizeInPixels.Height;
                }
                else
                {
                    SZoomF = (float) 300/UserBitmap.SizeInPixels.Width;
                }

                SX = 150 - ((UserBitmap.SizeInPixels.Width*SZoomF)/2);
                SY = 150 - ((UserBitmap.SizeInPixels.Height*SZoomF)/2);

                SIsCaculation = false;
            }

            SquareRectangleWidth = UserBitmap.SizeInPixels.Width*SZoomF;
            SquareRectangleHeight = UserBitmap.SizeInPixels.Height*SZoomF;

            var effect = new Transform2DEffect
            {
                Source = UserBitmap,
                InterpolationMode = CanvasImageInterpolation.HighQualityCubic,
                TransformMatrix = Matrix3x2.CreateScale(new Vector2(SZoomF))
            };

            //Render target: Main render
            SRenderTarget = new CanvasRenderTarget(_device, 300, 300, 96);
            using (var drawingSession = SRenderTarget.CreateDrawingSession())
            {
                //Clear the color
                drawingSession.Clear(currentColor);

                //Draw transperent bitmap
                drawingSession.DrawImage(_transperentBitmap, 0, 0, new Rect(0, 0, 300, 300), 1.0f);

                //Fill the rectangle with color
                drawingSession.FillRectangle(0, 0, 300, 300, currentColor);

                //Draw the user image to target
                drawingSession.DrawImage(effect, SX, SY, new Rect(SRecX, SRecY, SquareRectangleWidth, SquareRectangleHeight), 1.0f,
                    CanvasImageInterpolation.HighQualityCubic);
            }
        }

        public async Task LoadBitmap()
        {
            using (IRandomAccessStream fileStream = await File.OpenAsync(FileAccessMode.Read))
            {
                //User Bitmap
                UserBitmap = await CanvasBitmap.LoadAsync(_device, fileStream);
            }

            StorageFile file =
                await
                    StorageFile.GetFileFromApplicationUriAsync(new Uri(@"ms-appx:///Assets/Resources/checkerboard.png"));
            using (IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read))
            {
                //Transperent Bitmap
                _transperentBitmap = await CanvasBitmap.LoadAsync(_device, fileStream);
            }
        }

        public void OpenImage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            View?.OpenImage_Tapped(sender,e);
        }
    }
}