namespace UniversalLogoMaker.Utilities.Helpers
{
    using Windows.Networking.Connectivity;

    public static class Connection
    {
        public static bool HasInternetAccess { get; private set; }

        static Connection()
        {
            NetworkInformation.NetworkStatusChanged += NetworkInformationOnNetworkStatusChanged;
            CheckInternetAccess();
        }

        private static void NetworkInformationOnNetworkStatusChanged(object sender)
        {
            CheckInternetAccess();
        }

        private static void CheckInternetAccess()
        {
            var connectionProfile = NetworkInformation.GetInternetConnectionProfile();
            HasInternetAccess = connectionProfile != null &&
                                connectionProfile.GetNetworkConnectivityLevel() ==
                                NetworkConnectivityLevel.InternetAccess;
        }
    }
}