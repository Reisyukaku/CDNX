using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace CDNX {

	class Utils {

		//Conversions
		public static byte[] StringToByteArray(string hex) {
			return Enumerable.Range(0, hex.Length).Where(x => x % 2 == 0).Select(x => Convert.ToByte(hex.Substring(x, 2), 16)).ToArray();
		}

		//Sanitation
		public static bool IsValidTid(string hexstr) {
			Regex.Replace(hexstr, @"\s+", "");
			return !Regex.IsMatch(hexstr, @"0100[0-9a-zA-Z]{12}") ? false : true;
		}

		public static bool IsValidVersion(string ver) {
            return (Regex.Match(ver, @"[0-9]\.[0-9]\.[0-9]\.[0-9]*").Success || Regex.Match(ver, @"[0-9]*").Success) ? true : false;
        }
	}
}
