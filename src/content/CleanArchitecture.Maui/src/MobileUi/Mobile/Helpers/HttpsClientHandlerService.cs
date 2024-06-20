using System.Net.Security;

namespace CleanArchitecture.Maui.MobileUi.Mobile.Helpers;

public sealed class HttpsClientHandlerService
{
    public HttpMessageHandler GetPlatformMessageHandler()
    {
#if ANDROID
#if NET6_0
        var handler= new CustomAndroidMessageHandler();
#endif
        var handler = new Xamarin.Android.Net.AndroidMessageHandler();
        handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
        {
            if (cert is not null && cert.Issuer.Equals("CN=localhost"))
                return true;
            return errors == SslPolicyErrors.None;
        };
        return handler;
#elif IOS
            var handler = new NSUrlSessionHandler
            {
                TrustOverrideForUrl = IsHttpsLocalhost
            };
            return handler;
#elif WINDOWS || MACCATALYST
            return null;
#else
            throw new PlatformNotSupportedException("Only Android, iOS, MacCatalyst, and Windows supported.");
#endif
    }
    
#if ANDROID && NET6_0
    internal sealed class CustomAndroidMessageHandler : Xamarin.Android.Net.AndroidMessageHandler
    {
        protected override Javax.Net.Ssl.IHostnameVerifier GetSSLHostnameVerifier(Javax.Net.Ssl.HttpsURLConnection connection)
            => new CustomHostnameVerifier();

        private sealed class CustomHostnameVerifier : Java.Lang.Object, Javax.Net.Ssl.IHostnameVerifier
        {
            public bool Verify(string hostname, Javax.Net.Ssl.ISSLSession session)
            {
                return Javax.Net.Ssl.HttpsURLConnection.DefaultHostnameVerifier.Verify(hostname, session) ||
                    hostname == "10.0.2.2" && session.PeerPrincipal?.Name == "CN=localhost";
            }
        }
    }
#endif
#if IOS
        public bool IsHttpsLocalhost(NSUrlSessionHandler sender, string url, Security.SecTrust trust)
        {
            if (url.StartsWith("https://localhost"))
                return true;
            return false;
        }
#endif
}