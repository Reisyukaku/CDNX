using System;
using System.Collections.Generic;
using System.IO;

namespace CDNX.Mono.FileFormats {

	#region NCA3
	class NCA3 {

		public byte[] RSASig_0 = { };
		public byte[] RSASig_1 = { };
		public string Magic;
		public bool IsGameCard;
		public byte ContentType;
		public byte CryptoType;
		public byte KaekIndex;
		public UInt64 Size;
		public UInt64 TitleID;
		public UInt32 SDKVersion;
		public byte CryptoType2;
		public byte[] RightsID = { };

		public List<byte[]> HashTable;
		public List<byte[]> KeyArea;
		public PFS0 pfs0;

		public NCA3(string filename) : this(AES.DecryptHeader(File.ReadAllBytes(filename))) {}

		public NCA3(BinaryReader br) {
			this.RSASig_0 = br.ReadBytes(0x100);
			this.RSASig_1 = br.ReadBytes(0x100);
			if ((this.Magic = new string(br.ReadChars(4))) != "NCA3") throw new ApplicationException("Magic NCA3 not found!");
			this.IsGameCard = br.ReadBoolean();
			this.ContentType = br.ReadByte();
			this.CryptoType = br.ReadByte();
			this.KaekIndex = br.ReadByte();
			this.Size = br.ReadUInt64();
			this.TitleID = br.ReadUInt64();
			br.ReadBytes(4); //padding?
			this.SDKVersion = br.ReadUInt32();
			this.CryptoType2 = br.ReadByte();
			br.ReadBytes(15); //padding?
			this.RightsID = br.ReadBytes(0x10);

			List<SectionTableEntry> SectionTableEntries = new List<SectionTableEntry>();
			for (var i = 0; i < 4; i++) SectionTableEntries.Add(new SectionTableEntry(br));

			this.HashTable = new List<byte[]>();
			for (var i = 0; i < 4; i++) this.HashTable.Add(br.ReadBytes(0x20));

            var keyblob = AES.DecryptKeyArea(br, this.KaekIndex, this.CryptoType) ?? new byte[0x10];

            this.KeyArea = new List<byte[]>();
            for (var i = 0; i < 4; i++) {
                byte[] temp = new byte[0x10];
                Array.Copy(keyblob, (i*0x10), temp, 0, 0x10);
                this.KeyArea.Add(temp);
            }

			//Section header block
			br.BaseStream.Position = 0x400;
			br.ReadBytes(3); //???
			var fsType = br.ReadByte();
			var cryptoType = br.ReadByte();
			br.ReadBytes(3);

			//dont really need this stuff. Might finish later for completion
			UInt64 offsetRel = 0;
			UInt64 pfs0ActualSize = 0;
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
			var dec_cont = AES.CTRMode(br, contSize, this.KeyArea[cryptoType == 3 ? 2 : 0], iv);
			dec_cont.ReadBytes((int)offsetRel);
			this.pfs0 = new PFS0(dec_cont);
		}
	}

	class SectionTableEntry {

		public UInt32 MediaOffset;
		public UInt32 MediaEndOffset;

		public SectionTableEntry(BinaryReader br) {
			this.MediaOffset = br.ReadUInt32() * 0x200;
			this.MediaEndOffset = br.ReadUInt32() * 0x200;
			br.ReadUInt32();
			br.ReadUInt32();
		}
	}
	#endregion

	#region PFS0
	class PFS0 {

		public byte[] Hash = { };
		public string Magic;

		public List<FileEntry> Files;

		public PFS0(BinaryReader br) {
			long startOffset = br.BaseStream.Position;
			if ((this.Magic = new string(br.ReadChars(4))) != "PFS0") throw new ApplicationException("Magic PFS0 not found!");
			uint fileCnt = br.ReadUInt32();
			uint stringTableSize = br.ReadUInt32();
			br.ReadBytes(4);
			this.Files = new List<FileEntry>();
			for (var i = 0; i < fileCnt; i++) {
				this.Files.Add(new FileEntry(br, startOffset+0x10+(fileCnt*0x18), stringTableSize));
			}
		}
	}

	class FileEntry {

		public byte[] RawData;
		public string FileName;

		public FileEntry(BinaryReader br, long strTableOff, long strTableSize) {
			var dataOff = br.ReadUInt64();
			var dataSize = br.ReadUInt64();
			var strOff = br.ReadUInt32();
			br.ReadUInt32();
			var currPos = br.BaseStream.Position;

			br.BaseStream.Position = strTableOff + strOff;
			char ch; while ((ch = br.ReadChar()) != 0) this.FileName += ch;

			br.BaseStream.Position = strTableOff + strTableSize + (long)dataOff;
			this.RawData = br.ReadBytes((int)dataSize);

			br.BaseStream.Position = currPos;
		}
	}
	#endregion
}
