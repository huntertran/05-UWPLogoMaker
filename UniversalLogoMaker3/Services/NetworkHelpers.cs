namespace UniversalLogoMaker3.Services
{
    using Windows.Networking.Connectivity;

    public class Connection
    {
        public bool HasInternetAccess { get; private set; }

        public Connection()
        {
            ////NetworkInformation.NetworkStatusChanged += NetworkInformationOnNetworkStatusChanged;
            CheckInternetAccess();
        }

        ////private void NetworkInformationOnNetworkStatusChanged(object sender)
        ////{
        ////    CheckInternetAccess();
        ////}

        private void CheckInternetAccess()
        {
            var connectionProfile = NetworkInformation.GetInternetConnectionProfile();
            HasInternetAccess = connectionProfile != null &&
                                connectionProfile.GetNetworkConnectivityLevel() ==
                                NetworkConnectivityLevel.InternetAccess;
        }
    }
}
