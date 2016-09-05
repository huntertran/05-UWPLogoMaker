using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Data.Xml.Dom;
using Windows.UI.Core;
using Windows.UI.Notifications;
using Newtonsoft.Json.Linq;
using NotificationsExtensions.Toasts;
using UWPLogoMaker.Model;
using UWPLogoMaker.Utilities.Helpers;
using UWPLogoMaker.ViewModel;

namespace UWPLogoMaker.Utilities
{
    public class StaticMethod
    {
        public static async Task<string> GetHttpAsString(string uriString)
        {
            string result;

            Uri targetUri = new Uri(uriString);

            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, targetUri);
            HttpResponseMessage response = await client.SendAsync(request);

            using (Stream responseStream = await response.Content.ReadAsStreamAsync())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
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
            string result =
                await GetHttpAsString("https://sites.google.com/site/windowsstoreapplogomaker/");

            string json = Regex.Split(result, "~~~")[1];

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

                //TODO: notice for user

                ToastContent content = new ToastContent()
                {
                    Launch = "TuanTran",

                    Visual = new ToastVisual()
                    {
                        TitleText = new ToastText()
                        {
                            Text = StaticData.StartVm.Data.UpdateMessage
                        },

                        //BodyTextLine1 = new ToastText()
                        //{
                        //    Text = "NotificationsExtensions is great!"
                        //},

                        AppLogoOverride = new ToastAppLogo()
                        {
                            Crop = ToastImageCrop.Circle,
                            Source = new ToastImageSource("ms-appx:///Assets/Resources/Toast/new.png")
                        }
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

                    Audio = new ToastAudio()
                    {
                        Src = new Uri("ms-winsoundevent:Notification.IM")
                    }
                };

                XmlDocument doc = content.GetXml();

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
