namespace UWPLogoMaker.Utilities
{
    using System;
    using System.Diagnostics;
    using System.Net.Http;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Windows.ApplicationModel.Core;
    using Windows.Data.Xml.Dom;
    using Windows.UI.Core;
    using Windows.UI.Notifications;
    using Helpers;
    using Model;
    using Newtonsoft.Json.Linq;
    using NotificationsExtensions;
    using NotificationsExtensions.Toasts;
    using RestClientPCL;
    using RestClientPCL.Model;
    using ViewModel;

    public class ApiService
    {
        public static Api StaticApi = new Api()
        {
            BaseUrl = "sites.google.com/site/windowsstoreapplogomaker/",
            Scheme = UriScheme.Https
        };
        
        public static async Task UpdateDatabase()
        {
            Debug.WriteLine("Check for database");
            if (!Connection.HasInternetAccess)
            {
                return;
            }

            var onlineDatabase = await GetNewDatabase();

            var isDatabaseNewer = IsDatabseNewer(onlineDatabase);
            
            if (isDatabaseNewer)
            {
                UpdateDatabase(onlineDatabase);
                SendNotificationOfNewDatabase();
            }
        }

        private static bool IsDatabseNewer(Database newDatabase)
        {
            return newDatabase.DatabaseVersion > StaticData.StartVm.Data.DatabaseVersion;
        }

        private static async Task<Database> GetNewDatabase()
        {
            ApiSegment apiSegment = new ApiSegment
            {
                Method = HttpMethod.Get
            };

            string result = await StaticApi.SendTask(apiSegment);

            string json = Regex.Split(result, "~~~")[1];

            var jObject = JObject.Parse(json);
            return jObject.ToObject<Database>();
        }

        private static async void UpdateDatabase(Database newDatabase)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                () =>
                {
                    //Newer database
                    StaticData.StartVm.Data = newDatabase;
                });

            //Save to roaming folder
            await StorageHelper.Object2Json(newDatabase, "data.dat");
        }

        private static void SendNotificationOfNewDatabase()
        {
            ToastContent content = new ToastContent()
            {
                Launch = "TuanTran",

                Visual = new ToastVisual()
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {
                        Children =
                            {
                                new AdaptiveText()
                                {
                                    HintAlign = AdaptiveTextAlign.Auto,
                                    HintMaxLines = 1,
                                    HintWrap = true,
                                    Text = StaticData.StartVm.Data.UpdateMessage
                                }
                            },
                        AppLogoOverride = new ToastGenericAppLogo()
                        {
                            Source = "ms-appx:///Assets/Resources/Toast/new.png",
                            HintCrop = ToastGenericAppLogoCrop.Circle
                        }
                    },
                },

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
    }
}
