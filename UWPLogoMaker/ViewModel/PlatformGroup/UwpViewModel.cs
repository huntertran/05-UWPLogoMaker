using System;
using System.Diagnostics;
using System.Globalization;
using System.Numerics;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
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

        //Source
        private WriteableBitmap _widelogoImage; // Load source image, show to user the wide logo.
        private WriteableBitmap _squareLogoImage; // Load source image, show to user the square logo.
        private WriteableBitmap _saveBitmap; // temp to process bitmap

        //Result
        private WriteableBitmap _backgroundWideBitmap; // Complted part of Wide Image to save
        private WriteableBitmap _backgroundLogoBitmap; // Complted part of Logo Image to save
        private WriteableBitmap _backgroundSplashBitmap;

        //Display to screen for user
        private BitmapImage _wideImage;
        private BitmapImage _squareImage;

        public StorageFile File;

        //Default Size
        private Size _sizeWideLogo = new Size(620, 300);
        private Size _sizeSquareLogo = new Size(300, 300);

        private bool _isShowingProgress;

        #region Test Win2D

        private float _x;
        private float _y;

        private double _recX;
        private double _recY;
        private double _recW;
        private double _recH;

        private float _zoomF;

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

        public WriteableBitmap WideLogoImage
        {
            get { return _widelogoImage; }
            set
            {
                if (value == _widelogoImage) return;
                _widelogoImage = value;
                OnPropertyChanged();
            }
        }

        public WriteableBitmap SquareLogoImage
        {
            get { return _squareLogoImage; }
            set
            {
                if (Equals(value, _squareLogoImage)) return;
                _squareLogoImage = value;
                OnPropertyChanged();
            }
        }

        public WriteableBitmap SaveBitmap
        {
            get { return _saveBitmap; }
            set
            {
                if (Equals(value, _saveBitmap)) return;
                _saveBitmap = value;
            }
        }

        public WriteableBitmap BackgroundLogoBitmap
        {
            get { return _backgroundLogoBitmap; }
            set
            {
                if (Equals(value, _backgroundLogoBitmap)) return;
                _backgroundLogoBitmap = value;
                OnPropertyChanged();
            }
        }

        public WriteableBitmap BackgroundWideBitmap
        {
            get { return _backgroundWideBitmap; }
            set
            {
                if (Equals(value, _backgroundWideBitmap)) return;
                _backgroundWideBitmap = value;
                OnPropertyChanged();
            }
        }

        public WriteableBitmap BackgroundSplashBitmap
        {
            get { return _backgroundSplashBitmap; }
            set
            {
                if (Equals(value, _backgroundSplashBitmap)) return;
                _backgroundSplashBitmap = value;
                OnPropertyChanged();
            }
        }

        public BitmapImage WideImage
        {
            get { return _wideImage; }
            set
            {
                if (Equals(value, _wideImage)) return;
                _wideImage = value;
                OnPropertyChanged();
            }
        }

        public BitmapImage SquareImage
        {
            get { return _squareImage; }
            set
            {
                if (Equals(value, _squareImage)) return;
                _squareImage = value;
                OnPropertyChanged();
            }
        }

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

        public ScrollViewer WScrollViewer { get; set; }

        public ScrollViewer SScrollViewer { get; set; }

        private CanvasRenderTarget _renderTarget;

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

        public void ResetBackgroundLogoColor() // Reset background color before save image to file.
        {
            BackgroundWideBitmap.Clear(Color.FromArgb((byte) A, (byte) R, (byte) G, (byte) B));
            BackgroundLogoBitmap.Clear(Color.FromArgb((byte) A, (byte) R, (byte) G, (byte) B));
            BackgroundSplashBitmap.Clear(Color.FromArgb((byte) A, (byte) R, (byte) G, (byte) B));
        }

        private byte[] GetPixelToArray(WriteableBitmap writableBitmap)
        {
            byte[] pixels = writableBitmap.ToByteArray();
            return pixels;
        }

        public WriteableBitmap ResizeTo(WriteableBitmap writableBitmap, int newWidth, int newHeight)
        {
            if (writableBitmap == null)
            {
                return null;
            }
            int bmpWith = writableBitmap.PixelWidth;
            int bmpHeight = writableBitmap.PixelHeight;
            if (newWidth == bmpWith && newHeight == bmpHeight)
            {
                return writableBitmap;
            }
            return writableBitmap.Resize(newWidth, newHeight, WriteableBitmapExtensions.Interpolation.Bilinear);
        }

        public async Task DoTheGenerate()
        {
            //Init background
            BackgroundWideBitmap = BitmapFactory.New(2480, 1200);
            BackgroundLogoBitmap = BitmapFactory.New(1200, 1200);
            BackgroundSplashBitmap = BitmapFactory.New(600, 1240);

            // Reset background color with current brush.
            ResetBackgroundLogoColor();

            //Get zoomfator of scrollviewer
            float zoomF = WScrollViewer.ZoomFactor;

            //Rect(x,y,width,height)
            Rect sourceCrop = new Rect(WScrollViewer.HorizontalOffset / zoomF,
                WScrollViewer.VerticalOffset / zoomF,
                _sizeWideLogo.Width, _sizeWideLogo.Height);

            //Init
            Rect blitdestRect = new Rect
            {
                X = 0,
                Y = 0,
                Width = BackgroundWideBitmap.PixelWidth,
                Height = BackgroundWideBitmap.PixelHeight
            };

            // Nếu X bé hơn khung hình, crop theo chiều dài tối đa, vùng ghép tính từ trung tâm, chiều dài ghép scale theo tỉ lệ
            if (WScrollViewer.ExtentWidth < _sizeWideLogo.Width)
            {
                sourceCrop.Width = WScrollViewer.ExtentWidth;
                blitdestRect.Width = WScrollViewer.ExtentWidth / _sizeWideLogo.Width * blitdestRect.Width;
                blitdestRect.X = (BackgroundWideBitmap.PixelWidth - blitdestRect.Width) / 2;
            }
            // tương tự với Y
            if (WScrollViewer.ExtentHeight < _sizeWideLogo.Height)
            {
                sourceCrop.Height = WScrollViewer.ExtentHeight;
                blitdestRect.Height = WScrollViewer.ExtentHeight / _sizeWideLogo.Height * blitdestRect.Height;
                blitdestRect.Y = (BackgroundWideBitmap.PixelHeight - blitdestRect.Height) / 2;
            }

            sourceCrop.Width = sourceCrop.Width / zoomF;
            sourceCrop.Height = sourceCrop.Height / zoomF;

            SaveBitmap = WideLogoImage.Crop(sourceCrop);
            Rect blitsourceRect = new Rect(0, 0, SaveBitmap.PixelWidth, SaveBitmap.PixelHeight);

            if (WScrollViewer.ExtentWidth > _sizeWideLogo.Width && WScrollViewer.ExtentHeight > _sizeWideLogo.Height)
                BackgroundWideBitmap = SaveBitmap;
            else
                BackgroundWideBitmap.Blit(blitdestRect, SaveBitmap, blitsourceRect);

            /////////////// End Wide process, now work on Square Logo

            zoomF = SScrollViewer.ZoomFactor;

            sourceCrop.X = SScrollViewer.HorizontalOffset / zoomF;
            sourceCrop.Y = SScrollViewer.VerticalOffset / zoomF;
            sourceCrop.Width = _sizeSquareLogo.Width;
            sourceCrop.Height = _sizeSquareLogo.Height;

            blitdestRect.X = 0;
            blitdestRect.Y = 0;
            blitdestRect.Width = BackgroundLogoBitmap.PixelWidth;
            blitdestRect.Height = BackgroundLogoBitmap.PixelHeight;

            if (SScrollViewer.ExtentWidth < _sizeSquareLogo.Width)
            {
                sourceCrop.Width = SScrollViewer.ExtentWidth;
                blitdestRect.Width = SScrollViewer.ExtentWidth / _sizeSquareLogo.Width * blitdestRect.Width;
                blitdestRect.X = (BackgroundLogoBitmap.PixelWidth - blitdestRect.Width) / 2;
            }

            if (SScrollViewer.ExtentHeight < _sizeSquareLogo.Height)
            {
                sourceCrop.Height = SScrollViewer.ExtentHeight;
                blitdestRect.Height = SScrollViewer.ExtentHeight / _sizeSquareLogo.Height * blitdestRect.Height;
                blitdestRect.Y = (BackgroundLogoBitmap.PixelHeight - blitdestRect.Height) / 2;
            }

            sourceCrop.Width = sourceCrop.Width / zoomF;
            sourceCrop.Height = sourceCrop.Height / zoomF;

            SaveBitmap = SquareLogoImage.Crop(sourceCrop);
            blitsourceRect.X = 0;
            blitsourceRect.Y = 0;
            blitsourceRect.Width = SaveBitmap.PixelWidth;
            blitsourceRect.Height = SaveBitmap.PixelHeight;

            if (SScrollViewer.ExtentWidth > _sizeSquareLogo.Width &&
                SScrollViewer.ExtentHeight > _sizeSquareLogo.Height)
                BackgroundLogoBitmap = SaveBitmap;
            else
                BackgroundLogoBitmap.Blit(blitdestRect, SaveBitmap, blitsourceRect);

            /////////////// End Wide process, now work on Splash Logo (Window Phone)
            // #if have platform
            blitsourceRect.Width = BackgroundLogoBitmap.PixelWidth;
            blitsourceRect.Height = BackgroundLogoBitmap.PixelHeight;
            double scalePercent = 92.0 / 100.0; //92% of width.
            blitdestRect.Width = BackgroundSplashBitmap.PixelWidth * scalePercent;
            blitdestRect.Height = blitdestRect.Width;
            blitdestRect.X = (BackgroundSplashBitmap.PixelWidth - blitdestRect.Width) / 2;
            blitdestRect.Y = (BackgroundSplashBitmap.PixelHeight - blitdestRect.Height) / 2;
            BackgroundSplashBitmap.Blit(blitdestRect, BackgroundLogoBitmap, blitsourceRect);

            await SavePhoto();
        }

        public async Task SavePhoto()
        {
            if (SaveBitmap == null || BackgroundLogoBitmap == null) return;

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

            Guid bitmapEncoderGuid = BitmapEncoder.PngEncoderId;

            foreach (Platform platform in StaticData.StartVm.Data.PlatformList)
            {
                if (!platform.IsEnabled)
                {
                    continue;
                }

                //Send google analytics
                EasyTracker.GetTracker().SendEvent("platform", platform.Name, null, 0);

                //create folder
                StorageFolder platformFolder = await StaticData.SaveFolder.CreateFolderAsync(platform.Name,
                    CreationCollisionOption.OpenIfExists);

                foreach (LogoObject lObj in platform.SaveLogoList)
                {
                    var savedFile =
                        await platformFolder.CreateFileAsync(lObj.FileName + ".scale-" + lObj.Scale + ".png",
                            CreationCollisionOption.ReplaceExisting);
                    using (IRandomAccessStream stream = await savedFile.OpenAsync(FileAccessMode.ReadWrite))
                    {
                        // Create a bitmap encoder
                        BitmapEncoder bmpEncoder = await BitmapEncoder.CreateAsync(bitmapEncoderGuid, stream);
                        var newBitmap =  ResizeTo(lObj.Width == lObj.Height ? BackgroundLogoBitmap :
                            ((lObj.FileName.Equals("SplashScreen") && platform.Name.Equals("Windows Phone 8.1")) ?
                            BackgroundSplashBitmap : BackgroundWideBitmap), lObj.Width, lObj.Height);
                        byte[] pixels = GetPixelToArray(newBitmap); // await
                        bmpEncoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Straight,
                            (uint) newBitmap.PixelWidth, (uint) newBitmap.PixelHeight, 300.0, 300.0, pixels);
                        await bmpEncoder.FlushAsync();
                    }
                }
            }

            if (StaticData.StartVm.CustomData != null)
            {
                foreach (Platform platform in StaticData.StartVm.CustomData.PlatformList)
                {
                    if (!platform.IsEnabled)
                    {
                        continue;
                    }

                    //Send google analytics
                    EasyTracker.GetTracker().SendEvent("platform", platform.Name, null, 0);

                    //create folder
                    StorageFolder platformFolder = await StaticData.SaveFolder.CreateFolderAsync(platform.Name,
                        CreationCollisionOption.OpenIfExists);

                    foreach (LogoObject lObj in platform.SaveLogoList)
                    {
                        var savedFile =
                            await platformFolder.CreateFileAsync(lObj.FileName + ".scale-" + lObj.Scale + ".png",
                                CreationCollisionOption.ReplaceExisting);
                        using (IRandomAccessStream stream = await savedFile.OpenAsync(FileAccessMode.ReadWrite))
                        {
                            // Create a bitmap encoder
                            BitmapEncoder bmpEncoder = await BitmapEncoder.CreateAsync(bitmapEncoderGuid, stream);
                            var newBitmap = ResizeTo(lObj.Width == lObj.Height
                                ? BackgroundLogoBitmap
                                : ((lObj.FileName.Equals("SplashScreen") && platform.Name.Equals("Windows Phone 8.1"))
                                    ? BackgroundSplashBitmap
                                    : BackgroundWideBitmap), lObj.Width, lObj.Height);
                            byte[] pixels = GetPixelToArray(newBitmap); // await
                            bmpEncoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Straight,
                                (uint) newBitmap.PixelWidth, (uint) newBitmap.PixelHeight, 300.0, 300.0, pixels);
                            await bmpEncoder.FlushAsync();
                        }
                    }
                }
            }

            IsShowingProgress = false;
        }

        public async Task DoTheGenerateWin2DTask()
        {
            CanvasDevice device = CanvasDevice.GetSharedDevice();

            //Get current color
            Color c = new Color
            {
                    A = (byte) A,
                    R = (byte) R,
                    G = (byte) G,
                    B = (byte) B
            };
            
            CanvasBitmap userBitmap;
            using (IRandomAccessStream fileStream = await File.OpenAsync(FileAccessMode.Read))
            {
                //User Bitmap
                userBitmap = await CanvasBitmap.LoadAsync(device, fileStream);
            }

            if (userBitmap.SizeInPixels.Width <= userBitmap.SizeInPixels.Height)
            {
                ZoomF = (float)300 / userBitmap.SizeInPixels.Height;
            }
            else
            {
                ZoomF = (float)620 / userBitmap.SizeInPixels.Width;
            }

            ScaleEffect scaleEffect = new ScaleEffect
            {
                Source = userBitmap,
                InterpolationMode = CanvasImageInterpolation.Cubic,
                Scale = new Vector2()
                {
                    X = ZoomF,
                    Y = ZoomF
                }
            };

            if (IsCaculation)
            {
                //Send message to output
                Debug.WriteLine("Re caculate param");

                X = 310 - ((userBitmap.SizeInPixels.Width * ZoomF) / 2);
                Y = 150 - ((userBitmap.SizeInPixels.Height * ZoomF) / 2);

                RecW = userBitmap.SizeInPixels.Width * ZoomF;
                RecH = userBitmap.SizeInPixels.Height * ZoomF;

                IsCaculation = false;
            }

            //Render target: Main render
            RenderTarget = new CanvasRenderTarget(device, 620, 300, 96);
            using (var ds = RenderTarget.CreateDrawingSession())
            {
                //Clear the color
                ds.Clear(c);

                //Draw the user image to target
                ds.DrawImage(scaleEffect, X, Y, new Rect(RecX, RecY, RecW, RecH), 1.0f, CanvasImageInterpolation.Cubic);
            }

            //TODO: continue working here
            //SquareImage.SetSource();
        }

        public async Task DisplayPreview()
        {
            CanvasDevice device = CanvasDevice.GetSharedDevice();

            //Get current color
            Color c = new Color
            {
                A = (byte)A,
                R = (byte)R,
                G = (byte)G,
                B = (byte)B
            };
            
            CanvasBitmap userBitmap;
            using (IRandomAccessStream fileStream = await File.OpenAsync(FileAccessMode.Read))
            {
                //User Bitmap
                userBitmap = await CanvasBitmap.LoadAsync(device, fileStream);
            }

            //Width > Height
            if (userBitmap.SizeInPixels.Width <= userBitmap.SizeInPixels.Height)
            {
                ZoomF = (float)300/userBitmap.SizeInPixels.Height;
            }
            else
            {
                ZoomF = (float)620/userBitmap.SizeInPixels.Width;
            }

            ScaleEffect scaleEffect = new ScaleEffect
            {
                Source = userBitmap,
                InterpolationMode = CanvasImageInterpolation.Cubic,
                Scale = new Vector2()
                {
                    X = ZoomF,
                    Y = ZoomF
                }
            };

            if (IsCaculation)
            {
                X = 310 - ((userBitmap.SizeInPixels.Width * ZoomF) / 2);
                Y = 150 - ((userBitmap.SizeInPixels.Height * ZoomF) / 2);
                //X = (float)WScrollViewer.HorizontalOffset;
                //Y = (float)WScrollViewer.VerticalOffset;
                RecW = userBitmap.SizeInPixels.Width * ZoomF;
                RecH = userBitmap.SizeInPixels.Height * ZoomF;
            }

            //Render target: Main render
            RenderTarget = new CanvasRenderTarget(device, 620, 300, 96);
            using (var ds = RenderTarget.CreateDrawingSession())
            {
                //Clear the color
                ds.Clear(c);

                //Draw the user image to target
                ds.DrawImage(scaleEffect, X, Y, new Rect(RecX, RecY, RecW, RecH), 1.0f, CanvasImageInterpolation.Cubic);
            }
        }

        public enum FileFormat
        {
            Jpeg,
            Png,
            Bmp,
            Tiff,
            Gif
        }
    }
}