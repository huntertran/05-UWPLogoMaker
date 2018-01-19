namespace UWPLogoMaker.Utilities
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Net.Http;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Helpers;
    using Model;
    using Newtonsoft.Json.Linq;
    using NotificationsExtensions;
    using NotificationsExtensions.Toasts;
    using ViewModel;
    using Windows.ApplicationModel.Core;
    using Windows.UI.Core;
    using Windows.UI.Notifications;

    public class StaticMethod
    {
        public static async Task<string> GetHttpAsString(string uriString)
        {
            string result;

            var targetUri = new Uri(uriString);

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, targetUri);
            var response = await client.SendAsync(request);

            using (var responseStream = await response.Content.ReadAsStreamAsync())
            {
                var reader = new StreamReader(responseStream, Encoding.UTF8);
                result = reader.ReadToEnd();
            }

            return result;
        }

        public static async Task CheckForDatabase()
        {
            Debug.WriteLine("Check for database");
            if (!Connection.HasInternetAccess)
            {
                return;
            }

            var result =
                await GetHttpAsString("https://sites.google.com/site/windowsstoreapplogomaker/");

            var json = Regex.Split(result, "~~~")[1];

            var jObject = JObject.Parse(json);
            var tempDb = jObject.ToObject<Database>();

            if (tempDb.DatabaseVersion > StaticData.StartVm.Data.DatabaseVersion)
            {
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                    CoreDispatcherPriority.Normal,
                    () =>
                    {
                        //Newer database
                        StaticData.StartVm.Data = jObject.ToObject<Database>();
                    });

                //Save to roaming folder
                await StorageHelper.Object2Json(tempDb, "data.dat");

                var content = new ToastContent
                {
                    Launch = "TuanTran",

                    Visual = new ToastVisual
                    {
                        BindingGeneric = new ToastBindingGeneric
                        {
                            Children =
                            {
                                new AdaptiveText
                                {
                                    HintAlign = AdaptiveTextAlign.Auto,
                                    HintMaxLines = 1,
                                    HintWrap = true,
                                    Text = StaticData.StartVm.Data.UpdateMessage
                                }
                            },
                            AppLogoOverride = new ToastGenericAppLogo
                            {
                                Source = "ms-appx:///Assets/Resources/Toast/new.png",
                                HintCrop = ToastGenericAppLogoCrop.Circle
                            }
                        },

                        //BodyTextLine1 = new ToastText()
                        //{
                        //    Text = "NotificationsExtensions is great!"
                        //},

                        //AppLogoOverride = new ToastAppLogo()
                        //{
                        //    Crop = ToastImageCrop.Circle,
                        //    Source = new ToastImageSource("ms-appx:///Assets/Resources/Toast/new.png")
                        //}
                    },

                    //Actions = new ToastActionsCustom()
                    //{
                    //    Inputs =
                    //    {
                    //        new ToastTextBox("tbReply")
                    //        {
                    //            PlaceholderContent = "Type a response"
                    //        }
                    //    },

                    //    Buttons =
                    //    {
                    //        new ToastButton("reply", "reply")
                    //        {
                    //            ActivationType = ToastActivationType.Background,
                    //            ImageUri = "Assets/QuickReply.png",
                    //            TextBoxId = "tbReply"
                    //        }
                    //    }
                    //},

                    Audio = new ToastAudio
                    {
                        Src = new Uri("ms-winsoundevent:Notification.IM")
                    }
                };

                var doc = content.GetXml();

                // Generate WinRT notification
                var toast = new ToastNotification(doc)
                {
                    ExpirationTime = DateTime.Now.AddDays(1),
                    Tag = "1",
                    Group = "database"
                };

                ToastNotificationManager.CreateToastNotifier().Show(toast);
            }

            Debug.WriteLine("End Check for database");
        }
    }
}