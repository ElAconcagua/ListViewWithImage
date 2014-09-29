
using Android.App;
using Android.Content;
using Android.Net;
using Runtime.Kernel.Services;

namespace Runtime.Kernel.Droid.Services
{
    /// <summary>
    /// <uses-permission android:name="android.permission.ACCESS_NETWORK_STATE" />
    /// </summary>
    public static class Reachability
    {

        public static ConnectivityManager ConnectivityManager
        {
            get
            {
                return Application.Context.GetSystemService(Context.ConnectivityService) as ConnectivityManager;
            }
        }


        public static bool IsNetworkAvailable()
        {
            var activeConnection = ConnectivityManager.ActiveNetworkInfo;

            if (activeConnection != null && activeConnection.IsConnected)
                return true;

            return false;
        }

        public static NetworkStatus InternetConnectionStatus()
        {
            if (IsNetworkAvailable())
            {
                var wifiState = ConnectivityManager.GetNetworkInfo(ConnectivityType.Wifi).GetState();
                if (wifiState == NetworkInfo.State.Connected) return NetworkStatus.ReachableViaWiFiNetwork;


                var mobileState = ConnectivityManager.GetNetworkInfo(ConnectivityType.Mobile).GetState();
                if (mobileState == NetworkInfo.State.Connected) return NetworkStatus.ReachableViaCarrierDataNetwork;

            }

            return NetworkStatus.NotReachable;
        }


    }
}