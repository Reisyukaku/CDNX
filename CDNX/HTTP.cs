using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace CDNNX {

    class HTTP {

        public static WebResponse Request(string method, string url) {
            X509Certificate2 cert = new X509Certificate2(INIFile.Read("settings", "cert"));
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ClientCertificates.Add(cert);
            request.UserAgent = string.Format("NintendoSDK Firmware/{0} (platform:NX; did:{1}; eid:{2})", INIFile.Read("settings", "firmver"), INIFile.Read("settings", "did"), INIFile.Read("settings", "eid"));
            request.Method = method;
            ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);
            if (((HttpWebResponse)request.GetResponse()).StatusCode != HttpStatusCode.OK) { System.Console.WriteLine("http error"); return null; }
            return request.GetResponse();
        }
    }
}
