using System;
using System.IO;
using System.Runtime.InteropServices;

namespace CDNNX {

	class AES {

		[DllImport("NXCrypt.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
		private extern static void decryptHeader([Out] byte[] dst, byte[] src, int fileSize, byte[] headerKey);

		[DllImport("NXCrypt.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
		private extern static void decryptKeyArea([Out] byte[] dst, byte[] src, byte[] areaKey);

		[DllImport("NXCrypt.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
		private extern static void AesCtr([Out] byte[] dst, byte[] src, uint size, byte[] key, byte[] iv);

		public static BinaryReader DecryptHeader(byte[] file) {
			byte[] res = null;
			try {
				byte[] headerKey = Utils.StringToByteArray(Properties.Resources.HeaderKey);
				res = new byte[file.Length];
				decryptHeader(res, file, file.Length, headerKey);
			} catch (Exception e) {
				Console.WriteLine(e.StackTrace);
			}
			return new BinaryReader(new MemoryStream(res));
		}

		public static byte[] DecryptKeyArea(BinaryReader br, uint keyIndex) {
			byte[] res = null;
			try {
				byte[] areakey = null;
				switch (keyIndex) {
					case 0: areakey = Utils.StringToByteArray(Properties.Resources.AppKaek); break;
					case 1: areakey = Utils.StringToByteArray(Properties.Resources.OceanKaek); break;
					case 2: areakey = Utils.StringToByteArray(Properties.Resources.SysKaek); break;
				}
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
