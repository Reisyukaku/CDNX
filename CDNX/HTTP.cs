using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace CDNNX {

    class HTTP {

        public static WebResponse Request(string method, string url) {
            X509Certificate2 cert = new X509Certificate2(Directory.GetCurrentDirectory() + @"\nx_tls_client_cert.pfx", "switch");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ClientCertificates.Add(cert);
            request.UserAgent = string.Format("NintendoSDK Firmware/{0} (platform:NX; did:{1}; eid:{2})", INIFile.ReadSetting("firmver"), INIFile.ReadSetting("did"), INIFile.ReadSetting("eid"));
            request.Method = method;
            ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);
            if (((HttpWebResponse)request.GetResponse()).StatusCode != HttpStatusCode.OK) return null;
            return request.GetResponse();
        }
    }
}
