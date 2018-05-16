using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace CDNNX {

    public class INIFile {

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public static void WriteSetting(string Key, string Value){
            WritePrivateProfileString("settings", Key, Value, Directory.GetCurrentDirectory()+"/config.ini");
        }

        public static string ReadSetting(string Key){
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString("settings", Key, "", temp, 255, Directory.GetCurrentDirectory() + "/config.ini");
            return temp.ToString();
        }
    }
}