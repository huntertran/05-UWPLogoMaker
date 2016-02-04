using System;
using System.Diagnostics;
using System.Globalization;
using System.Numerics;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media;
using GoogleAnalytics;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using UWPLogoMaker.Model;
using UWPLogoMaker.Utilities;
// ReSharper disable CompareOfFloatsByEqualityOperator

namespace UWPLogoMaker.ViewModel.PlatformGroup
{
    public class UwpViewModel : BaseViewModel
    {
        private Brush _currentBrush;

        private double _r = 255;
        private double _g = 255;
        private double _b = 255;
        private double _a = 255;
        private string _hexaCode;
        
        public StorageFile File;

        private bool _isShowingProgress;

        private bool _isManualAdjustSquareImage;

        #region Test Win2D

        private readonly CanvasDevice _device = CanvasDevice.GetSharedDevice();
        private CanvasBitmap _userBitmap;
        private CanvasBitmap _transperentBitmap;

        private float _x;
        private float _y;

        private float _plexibleX;

        private double _recX;
        private double _recY;
        private double _recW;
        private double _recH;

        private float _zoomF;
        private float _zoomFBefore;

        private double _maxWidth;
        private double _maxHeight;

        private bool _isCaculation;

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

        public double RecW
        {
            get { return _recW; }
            set
            {
                if (value.Equals(_recW)) return;
                _recW = value;
                OnPropertyChanged();
            }
        }

        public double RecH
        {
            get { return _recH; }
            set
            {
                if (value.Equals(_recH)) return;
                _recH = value;
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
        private double _sRecW;
        private double _sRecH;

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

        public double SRecW
        {
            get { return _sRecW; }
            set
            {
                if (value.Equals(_sRecW)) return;
                _sRecW = value;
                OnPropertyChanged();
            }
        }

        public double SRecH
        {
            get { return _sRecH; }
            set
            {
                if (value.Equals(_sRecH)) return;
                _sRecH = value;
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
                SZoomFBefore = _sZoomF * 100;
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
                SZoomF = _sZoomFBefore / 100;
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

        #region Background Coloring

        public Brush CurrentBrush
        {
            get { return _currentBrush; }
            set
            {
                if (Equals(value, _currentBrush)) return;
                _currentBrush = value;
                //ChangeBackgroundColor();
                OnPropertyChanged();
            }
        }

        public double R
        {
            get { return _r; }
            set
            {
                if (value == _r) return;
                _r = value;

                OnPropertyChanged();
                ChangeColor();
            }
        }

        public double G
        {
            get { return _g; }
            set
            {
                if (value == _g) return;
                _g = value;

                OnPropertyChanged();
                ChangeColor();
            }
        }

        public double B
        {
            get { return _b; }
            set
            {
                if (value == _b) return;
                _b = value;

                OnPropertyChanged();
                ChangeColor();
            }
        }

        public double A
        {
            get { return _a; }
            set
            {
                if (value == _a) return;
                _a = value;

                OnPropertyChanged();
                ChangeColor();
            }
        }

        public string HexaCode
        {
            get { return _hexaCode; }
            set
            {
                if (value == _hexaCode) return;
                _hexaCode = value;

                OnPropertyChanged();
                ChangeColorFromHexa();
            }
        }

        public void ChangeColor()
        {
            CurrentBrush = new SolidColorBrush(Color.FromArgb((byte) A, (byte) R, (byte) G, (byte) B));
            HexaCode = "#" + ((byte) A).ToString("X2") + ((byte) R).ToString("X2") + ((byte) G).ToString("X2") +
                       ((byte) B).ToString("X2");
        }

        public void ChangeColorFromHexa()
        {
            //Remove # if present
            if (HexaCode.IndexOf('#') != -1)
                HexaCode = HexaCode.Replace("#", "");

            if (HexaCode.Length == 8)
            {
                //#AARRGGBB
                A = int.Parse(HexaCode.Substring(0, 2), NumberStyles.AllowHexSpecifier);
                R = int.Parse(HexaCode.Substring(2, 2), NumberStyles.AllowHexSpecifier);
                G = int.Parse(HexaCode.Substring(4, 2), NumberStyles.AllowHexSpecifier);
                B = int.Parse(HexaCode.Substring(6, 2), NumberStyles.AllowHexSpecifier);
            }
            if (HexaCode.Length == 6)
            {
                //#RRGGBB
                R = int.Parse(HexaCode.Substring(0, 2), NumberStyles.AllowHexSpecifier);
                G = int.Parse(HexaCode.Substring(2, 2), NumberStyles.AllowHexSpecifier);
                B = int.Parse(HexaCode.Substring(4, 2), NumberStyles.AllowHexSpecifier);
            }
            else if (HexaCode.Length == 3)
            {
                //#RGB
                R = int.Parse(HexaCode[0].ToString() + HexaCode[0].ToString(), NumberStyles.AllowHexSpecifier);
                G = int.Parse(HexaCode[1].ToString() + HexaCode[1].ToString(), NumberStyles.AllowHexSpecifier);
                B = int.Parse(HexaCode[2].ToString() + HexaCode[2].ToString(), NumberStyles.AllowHexSpecifier);
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

        private CanvasRenderTarget _renderTarget;

        private CanvasRenderTarget _sRenderTarget;

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

        public async Task DoTheGenerateWin2DTask()
        {
            //Get current color
            Color c = new Color
            {
                A = (byte) A,
                R = (byte) R,
                G = (byte) G,
                B = (byte) B
            };

            if (IsCaculation)
            {
                //Send message to output
                Debug.WriteLine("Re caculate param");

                if (_userBitmap.SizeInPixels.Width <= _userBitmap.SizeInPixels.Height)
                {
                    ZoomF = (float) 300/_userBitmap.SizeInPixels.Height;
                }
                else
                {
                    ZoomF = (float) 620/_userBitmap.SizeInPixels.Width;
                }

                X = 310 - ((_userBitmap.SizeInPixels.Width*ZoomF)/2);
                Y = 150 - ((_userBitmap.SizeInPixels.Height*ZoomF)/2);

                IsCaculation = false;
            }

            RecW = _userBitmap.SizeInPixels.Width*ZoomF;
            RecH = _userBitmap.SizeInPixels.Height*ZoomF;

            //Get save mode
            int saveMode = SettingManager.GetSaveMode();
            if (saveMode == 0 || saveMode == 1)
            {
                //Default
                SettingManager.SetSaveMode(2);
                saveMode = 2;
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

            foreach (Platform platform in StaticData.StartVm.Data.PlatformList)
            {
                if (!platform.IsEnabled)
                {
                    continue;
                }

                //Send google analytics
                EasyTracker.GetTracker().SendEvent("platform", platform.Name, null, 0);

                //Create folder
                StorageFolder platformFolder = await StaticData.SaveFolder.CreateFolderAsync(platform.Name,
                    CreationCollisionOption.OpenIfExists);

                foreach (LogoObject logoObject in platform.SaveLogoList)
                {
                    RenderImage(c, logoObject.Width, logoObject.Height);
                    var savedFile =
                        await
                            platformFolder.CreateFileAsync(logoObject.FileName + ".scale-" + logoObject.Scale + ".png",
                                CreationCollisionOption.ReplaceExisting);
                    using (var outStream = await savedFile.OpenAsync(FileAccessMode.ReadWrite))
                    {
                        await RenderTarget.SaveAsync(outStream, CanvasBitmapFileFormat.Png);
                    }
                }
            }

            IsShowingProgress = false;
        }

        private void RenderImage(Color c, double width, double height)
        {
            double ratio = height/300;
            ScaleEffect scaleEffect = new ScaleEffect
            {
                Source = _userBitmap,
                InterpolationMode = CanvasImageInterpolation.HighQualityCubic,
                Scale = new Vector2()
                {
                    X = (float) (ZoomF*ratio),
                    Y = (float) (ZoomF*ratio)
                }
            };

            //Render target: Main render
            if (width == height)
            {
                //Square
                RenderTarget = new CanvasRenderTarget(_device, (float) (300*ratio), (float) (300*ratio), 96);
                PlexibleX = X - 160;
            }
            else if (width > height)
            {
                RenderTarget = new CanvasRenderTarget(_device, (float) (620*ratio), (float) (300*ratio), 96);
                PlexibleX = X;
            }

            using (var ds = RenderTarget.CreateDrawingSession())
            {
                //Clear the color
                ds.Clear(c);

                //Draw the user image to target
                ds.DrawImage(scaleEffect, (float) (PlexibleX * ratio), (float) (Y*ratio),
                    new Rect(RecX, RecY, RecW*ratio, RecH*ratio), 1.0f, CanvasImageInterpolation.HighQualityCubic);
            }
        }

        public void DisplayPreview()
        {
            if (File == null)
            {
                return;
            }

            //Get current color
            Color c = new Color
            {
                A = (byte) A,
                R = (byte) R,
                G = (byte) G,
                B = (byte) B
            };

            if (IsCaculation)
            {
                //Send message to output
                Debug.WriteLine("Re caculate param");

                if (_userBitmap.SizeInPixels.Width <= _userBitmap.SizeInPixels.Height)
                {
                    ZoomF = (float) 300/_userBitmap.SizeInPixels.Height;
                }
                else
                {
                    ZoomF = (float) 620/_userBitmap.SizeInPixels.Width;
                }

                X = 310 - ((_userBitmap.SizeInPixels.Width*ZoomF)/2);
                Y = 150 - ((_userBitmap.SizeInPixels.Height*ZoomF)/2);

                IsCaculation = false;
            }

            RecW = _userBitmap.SizeInPixels.Width*ZoomF;
            RecH = _userBitmap.SizeInPixels.Height*ZoomF;
            
            ScaleEffect scaleEffect = new ScaleEffect
            {
                Source = _userBitmap,
                InterpolationMode = CanvasImageInterpolation.HighQualityCubic,
                Scale = new Vector2
                {
                    X = ZoomF,
                    Y = ZoomF
                }
            };

            //Render target: Main render
            RenderTarget = new CanvasRenderTarget(_device, 620, 300, 96);
            using (var ds = RenderTarget.CreateDrawingSession())
            {
                //Clear the color
                ds.Clear(c);

                //Draw transperent bitmap
                ds.DrawImage(_transperentBitmap, 0, 0, new Rect(0, 0, 620, 300), 1.0f);

                //Fill the rectangle with color
                ds.FillRectangle(0, 0, 620, 300, c);

                //Draw the user image to target
                ds.DrawImage(scaleEffect, X, Y, new Rect(RecX, RecY, RecW, RecH), 1.0f,
                    CanvasImageInterpolation.HighQualityCubic);
            }
        }

        public void DisplaySquarePreview()
        {
            if (!IsManualAdjustSquareImage)
            {
                SX = X - 160;
                SY = Y;

                SRecX = RecX;
                SRecY = RecY;
                SRecW = RecW;
                SRecH = RecH;

                SZoomF = ZoomF;

                SMaxWidth = MaxWidth;
                SMaxHeight = MaxHeight;
            }

            if (File == null)
            {
                return;
            }

            //Get current color
            Color c = new Color
            {
                A = (byte)A,
                R = (byte)R,
                G = (byte)G,
                B = (byte)B
            };

            if (SIsCaculation)
            {
                //Send message to output
                Debug.WriteLine("Re caculate param");

                if (_userBitmap.SizeInPixels.Width <= _userBitmap.SizeInPixels.Height)
                {
                    SZoomF = (float)300 / _userBitmap.SizeInPixels.Height;
                }
                else
                {
                    SZoomF = (float)300 / _userBitmap.SizeInPixels.Width;
                }

                SX = 150 - ((_userBitmap.SizeInPixels.Width * SZoomF) / 2);
                SY = 150 - ((_userBitmap.SizeInPixels.Height * SZoomF) / 2);

                SIsCaculation = false;
            }
            
            SRecW = _userBitmap.SizeInPixels.Width * SZoomF;
            SRecH = _userBitmap.SizeInPixels.Height * SZoomF;

            ScaleEffect scaleEffect = new ScaleEffect
            {
                Source = _userBitmap,
                InterpolationMode = CanvasImageInterpolation.HighQualityCubic,
                Scale = new Vector2
                {
                    X = SZoomF,
                    Y = SZoomF
                }
            };

            //Render target: Main render
            SRenderTarget = new CanvasRenderTarget(_device, 300, 300, 96);
            using (var ds = SRenderTarget.CreateDrawingSession())
            {
                //Clear the color
                ds.Clear(c);

                //Draw transperent bitmap
                ds.DrawImage(_transperentBitmap, 0, 0, new Rect(0, 0, 300, 300), 1.0f);

                //Fill the rectangle with color
                ds.FillRectangle(0, 0, 300, 300, c);

                //Draw the user image to target
                ds.DrawImage(scaleEffect, SX, SY, new Rect(SRecX, SRecY, SRecW, SRecH), 1.0f,
                    CanvasImageInterpolation.HighQualityCubic);
            }
        }

        public async Task LoadBitmap()
        {
            using (IRandomAccessStream fileStream = await File.OpenAsync(FileAccessMode.Read))
            {
                //User Bitmap
                _userBitmap = await CanvasBitmap.LoadAsync(_device, fileStream);
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
    }
}