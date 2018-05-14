using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace CDNNX
{
    class HTTP
    {
        public static WebResponse Request(string method, string url)
        {
            X509Certificate2 cert = new X509Certificate2(Directory.GetCurrentDirectory() + @"\nx_tls_client_cert.pfx", "switch");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ClientCertificates.Add(cert);
            request.UserAgent = "NintendoSDK Firmware/5.0.2-0 (platform:NX; did:0000000000000000; eid:lp1)";
            request.Method = method;
            ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);
            return request.GetResponse();
        }
    }
}
