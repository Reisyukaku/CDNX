using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CDNNX {

	class AES {

		[DllImport("NXCrypt.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
		private extern static void decryptHeader([Out] byte[] dst, byte[] src, int fileSize, byte[] headerKey);

		[DllImport("NXCrypt.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
		private extern static void decryptKeyArea([Out] byte[] dst, byte[] src, byte[] areaKey);

		[DllImport("NXCrypt.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
		private extern static void AesCtr([Out] byte[] dst, byte[] src, uint size, byte[] key, byte[] iv);

        [DllImport("NXCrypt.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        private extern static void generate_kek(byte[] dst, byte[] src, byte[] master_key, byte[] kek_seed, byte[] key_seed);

        public static BinaryReader DecryptHeader(byte[] file) {
			byte[] res = null;
			try {
                byte[] headerKey = Utils.StringToByteArray(INIFile.Read("keys", "headkey"));
				res = new byte[file.Length];
				decryptHeader(res, file, file.Length, headerKey);
			} catch (Exception e) {
				Console.WriteLine(e.StackTrace);
			}
			return new BinaryReader(new MemoryStream(res));
		}

		public static byte[] DecryptKeyArea(BinaryReader br, uint kaekInd, uint mkInd) {
            if (INIFile.Read("keys", "MK" + mkInd.ToString("D2")) == string.Empty) {
                MessageBox.Show(string.Format("Missing the required Masterkey {0} for this title!", mkInd.ToString("D2")));
                return null;
            }

			byte[] res = null;
			try {
				byte[] areakey = {0};
                byte[] kaekSrc = {0};
				switch (kaekInd) {
                    default:
					case 0: kaekSrc = Utils.StringToByteArray(INIFile.Read("keys", "akaeksrc")); break;
					case 1: kaekSrc = Utils.StringToByteArray(INIFile.Read("keys", "okaeksrc")); break;
					case 2: kaekSrc = Utils.StringToByteArray(INIFile.Read("keys", "skaeksrc")); break;
				}
                generate_kek(
                    areakey, 
                    kaekSrc, 
                    Utils.StringToByteArray(INIFile.Read("keys", "MK"+mkInd.ToString("D2"))), 
                    Utils.StringToByteArray(INIFile.Read("keys", "kekseed")), 
                    Utils.StringToByteArray(INIFile.Read("keys", "keyseed"))
                );
                res = new byte[0x40];
				decryptKeyArea(res, br.ReadBytes(0x40), areakey);
			} catch (Exception e) {
				Console.WriteLine(e.StackTrace);
			}
			return res;
		}

		public static BinaryReader CTRMode(BinaryReader br, int contSize, byte[] key, byte[] iv) {
			byte[] res = null;
			try {
				res = new byte[contSize];
				var temp = br.ReadBytes(contSize);
				AesCtr(res, temp, (uint)contSize, key, iv);
				br.Close();
			} catch (Exception e) {
				Console.WriteLine(e.StackTrace);
			}
			return new BinaryReader(new MemoryStream(res));
		}
	}
}
