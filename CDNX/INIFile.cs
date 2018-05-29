using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using IniParser;
using IniParser.Model;

namespace CDNNX {

    public class INIFile {

        public static void Write(string category, string key, string value){
            if (!File.Exists(Path.Combine(Application.StartupPath, "config.ini"))){
                File.Create(Path.Combine(Application.StartupPath, "config.ini")).Dispose();
            }
            FileIniDataParser parser = new FileIniDataParser();
            IniData iniData = parser.ReadFile(Path.Combine(Application.StartupPath, "config.ini"));
            if (iniData[category] == null || !iniData[category].Any()){
                iniData.Sections.AddSection(category);
            }
            iniData[category].AddKey(key, value);
            parser.WriteFile(Path.Combine(Application.StartupPath, "config.ini"), iniData);
        }

        public static string Read(string category, string key){
            FileIniDataParser parser = new FileIniDataParser();
            IniData iniData = parser.ReadFile(Path.Combine(Application.StartupPath, "config.ini"));
            return iniData?[category]?[key];
        }
    }
}