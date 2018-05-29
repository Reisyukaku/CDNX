using System;
using System.Collections.Generic;
using System.IO;

namespace CDNNX {

	#region NCA3
	internal class NCA3 {

		public byte[] RSASig_0 = { };
		public byte[] RSASig_1 = { };
		public string Magic;
		public bool IsGameCard;
		public byte ContentType;
		public byte CryptoType;
		public byte KaekIndex;
		public ulong Size;
		public ulong TitleID;
		public uint SDKVersion;
		public byte CryptoType2;
		public byte[] RightsID = { };

		public List<byte[]> HashTable;
		public List<byte[]> KeyArea;
		public PFS0 pfs0;

		public NCA3(string filename) : this(AES.DecryptHeader(File.ReadAllBytes(filename))) {}

		public NCA3(BinaryReader br) {
			RSASig_0 = br.ReadBytes(0x100);
			RSASig_1 = br.ReadBytes(0x100);
			if ((Magic = new string(br.ReadChars(4))) != "NCA3") throw new ApplicationException("Magic NCA3 not found!");
			IsGameCard = br.ReadBoolean();
			ContentType = br.ReadByte();
			CryptoType = br.ReadByte();
			KaekIndex = br.ReadByte();
			Size = br.ReadUInt64();
			TitleID = br.ReadUInt64();
			br.ReadBytes(4); //padding?
			SDKVersion = br.ReadUInt32();
			CryptoType2 = br.ReadByte();
			br.ReadBytes(15); //padding?
			RightsID = br.ReadBytes(0x10);

			List<SectionTableEntry> SectionTableEntries = new List<SectionTableEntry>();
			for (int i = 0; i < 4; i++) SectionTableEntries.Add(new SectionTableEntry(br));

			HashTable = new List<byte[]>();
			for (int i = 0; i < 4; i++) HashTable.Add(br.ReadBytes(0x20));

            byte[] keyblob = AES.DecryptKeyArea(br, KaekIndex, CryptoType) ?? new byte[0x10];

            KeyArea = new List<byte[]>();
            for (int i = 0; i < 4; i++) {
                byte[] temp = new byte[0x10];
                Array.Copy(keyblob, (i*0x10), temp, 0, 0x10);
                KeyArea.Add(temp);
            }

			//Section header block
			br.BaseStream.Position = 0x400;
			br.ReadBytes(3); //???
			byte fsType = br.ReadByte();
			byte cryptoType = br.ReadByte();
			br.ReadBytes(3);

			//dont really need this stuff. Might finish later for completion
			ulong offsetRel = 0;
			ulong pfs0ActualSize = 0;
			if (fsType == 2) {
				//PFS0 superblock
				br.ReadBytes(0x38);
				offsetRel = br.ReadUInt64();
				pfs0ActualSize = br.ReadUInt64();
			} else if (fsType == 3) {
				//ROMFS superblock
				br.ReadBytes(0xE8);
			}

			br.BaseStream.Position = SectionTableEntries[0].MediaOffset;
			
			int contSize = (int)(SectionTableEntries[0].MediaEndOffset - SectionTableEntries[0].MediaOffset);
			byte[] iv = new byte[0x10];
			Array.Copy(BitConverter.GetBytes(SectionTableEntries[0].MediaOffset >> 4), 0, iv, 0, 4);
			Array.Reverse(iv);
			BinaryReader dec_cont = AES.CTRMode(br, contSize, KeyArea[cryptoType == 3 ? 2 : 0], iv);
			dec_cont.ReadBytes((int)offsetRel);
			pfs0 = new PFS0(dec_cont);
		}
	}

	internal class SectionTableEntry {

		public uint MediaOffset;
		public uint MediaEndOffset;

		public SectionTableEntry(BinaryReader br) {
			MediaOffset = br.ReadUInt32() * 0x200;
			MediaEndOffset = br.ReadUInt32() * 0x200;
			br.ReadUInt32();
			br.ReadUInt32();
		}
	}
	#endregion

	#region PFS0
	internal class PFS0 {

		public byte[] Hash = { };
		public string Magic;

		public List<FileEntry> Files;

		public PFS0(BinaryReader br) {
			long startOffset = br.BaseStream.Position;
			if ((Magic = new string(br.ReadChars(4))) != "PFS0") throw new ApplicationException("Magic PFS0 not found!");
			uint fileCnt = br.ReadUInt32();
			uint stringTableSize = br.ReadUInt32();
			br.ReadBytes(4);
			Files = new List<FileEntry>();
			for (int i = 0; i < fileCnt; i++) {
				Files.Add(new FileEntry(br, startOffset+0x10+(fileCnt*0x18), stringTableSize));
			}
		}
	}

	internal class FileEntry {

		public byte[] RawData;
		public string FileName;

		public FileEntry(BinaryReader br, long strTableOff, long strTableSize) {
			ulong dataOff = br.ReadUInt64();
			ulong dataSize = br.ReadUInt64();
			uint strOff = br.ReadUInt32();
			br.ReadUInt32();
			long currPos = br.BaseStream.Position;

			br.BaseStream.Position = strTableOff + strOff;
			char ch; while ((ch = br.ReadChar()) != 0) FileName += ch;

			br.BaseStream.Position = strTableOff + strTableSize + (long)dataOff;
			RawData = br.ReadBytes((int)dataSize);

			br.BaseStream.Position = currPos;
		}
	}
	#endregion
}
