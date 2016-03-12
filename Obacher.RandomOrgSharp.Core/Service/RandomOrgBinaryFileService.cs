using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obacher.RandomOrgSharp.Core.Service
{
    public class RandomOrgBinaryFileService : RandomOrgFileService
    {
        public void ReadFile()
        {
            BitArray bits;
            using (Stream fileStream = new FileStream(@"D:\Development\RandomOrgSharp\2016-01-27.bin", FileMode.Open, FileAccess.Read))
            using (BinaryReader reader = new BinaryReader(fileStream))
            {
                byte[] data = new BinaryReader(fileStream).ReadBytes((int)fileStream.Length);
                bits = new BitArray(data);
            }

            var count = bits.Count;
            //bits.Get();

        }
    }
}
