using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace CDNNX {

    class HTTP {

        public static WebResponse Request(string method, string url) {
            X509Certificate2 cert = new X509Certificate2(Path.Combine(Application.StartupPath, "nx_tls_client_cert.pfx"), "switch");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ClientCertificates.Add(cert);
            request.UserAgent = $"NintendoSDK Firmware/{INIFile.Read("settings", "firmver")} (platform:NX; did:{INIFile.Read("settings", "did")}; eid:{INIFile.Read("settings", "eid")})";
            request.Method = method;
            ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);
            if (((HttpWebResponse)request.GetResponse()).StatusCode != HttpStatusCode.OK) { System.Console.WriteLine("http error"); return null; }
            return request.GetResponse();
        }
    }
}
