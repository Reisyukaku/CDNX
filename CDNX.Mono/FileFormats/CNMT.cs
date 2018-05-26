using System;
using System.Collections.Generic;
using System.IO;

namespace CDNX.Mono.FileFormats {
	class CNMT {

		public UInt64 TitleId;
		public uint TitleVersion;
		public string Type;
		public List<ContEntry> contEntries;

		private enum Types{
			SystemPrograms=1,
			SystemDataArchives=2,
			SystemUpdate=3,
			FirmwarePackageA=4,
			FirmwarePackageB=5,
			RegularApplication=0x80,
			UpdateTitle=0x81,
			AddonContent=0x82,
			DeltaTitle=0x83
		};

		public CNMT(BinaryReader br) {
			this.TitleId = br.ReadUInt64();
			this.TitleVersion = br.ReadUInt32();
			this.Type = ((Types)br.ReadByte()).ToString();
			br.ReadByte();
			var offset = br.ReadUInt16();
			var contCnt = br.ReadUInt16();
			var metaCnt = br.ReadUInt16();
			br.ReadBytes(12+offset);
			this.contEntries = new List<ContEntry>();
			for (var i = 0; i < contCnt; i++) {
				ContEntry entry = new ContEntry(br);
				this.contEntries.Add(entry);
			}
			br.Close();
		}
	}

	class ContEntry {

		public string Hash;
		public string NcaId;
		public uint Size;
		public string Type;

		private string[] types = { "Meta", "Program", "Data", "Control", "Offline-Manual", "Manual" };

		public ContEntry(BinaryReader br) {
			this.Hash = BitConverter.ToString(br.ReadBytes(32)).Replace("-", string.Empty);
			this.NcaId = BitConverter.ToString(br.ReadBytes(16)).Replace("-", string.Empty);
			this.Size = BitConverter.ToUInt32(br.ReadBytes(6), 0);
			this.Type = this.types[br.ReadByte()];
			br.ReadByte();
		}
	}
}
