using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using IniParser;
using IniParser.Model;

namespace CDNX.Mono
{
    public class INIFile
    {
        public static void Write(string category, string key, string value)
        {
            FileIniDataParser parser = new FileIniDataParser();
            IniData iniData = parser.ReadFile(Path.Combine(Application.StartupPath, "config.ini"));

            if (iniData.Sections.ContainsSection(category))
            {
                iniData[category].AddKey(key, value);
            }
            else
            {
                iniData.Sections.AddSection(category);
                iniData[category].AddKey(key, value);
            }
        }

        public static string Read(string category, string key)
        {
            FileIniDataParser parser = new FileIniDataParser();
            IniData iniData = parser.ReadFile(Path.Combine(Application.StartupPath, "config.ini"));
            return iniData[category][key];
        }
    }
}