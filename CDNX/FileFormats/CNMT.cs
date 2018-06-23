using System;
using System.Collections.Generic;
using System.IO;

namespace CDNNX {
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
            TitleId = br.ReadUInt64();
            TitleVersion = br.ReadUInt32();
            Type = ((Types)br.ReadByte()).ToString();
            br.ReadByte();
            var offset = br.ReadUInt16();
            var contCnt = br.ReadUInt16();
            var metaCnt = br.ReadUInt16();
            br.ReadBytes(12+offset);
            contEntries = new List<ContEntry>();
            for (var i = 0; i < contCnt; i++) {
                ContEntry entry = new ContEntry(br);
                Console.WriteLine("NcaID: " + entry.NcaId);
                contEntries.Add(entry);
            }
            br.Close();
        }
    }

    class ContEntry {

        public string Hash;
        public string NcaId;
        public uint Size;
        public string Type;

        public ContEntry(BinaryReader br) {
            Hash = BitConverter.ToString(br.ReadBytes(32)).Replace("-", string.Empty);
            NcaId = BitConverter.ToString(br.ReadBytes(16)).Replace("-", string.Empty);
            Size = BitConverter.ToUInt32(br.ReadBytes(6), 0);
            Type = typeToStr(br.ReadByte());
            br.ReadByte();
        }

        string typeToStr(int type) {
            string[] types = { "Meta", "Program", "Data", "Control", "Offline-Manual", "Manual" };
            return type >= types.Length ? "Type "+type.ToString() : types[type];
        }
    }
}
