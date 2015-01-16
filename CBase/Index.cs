using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBase
{
    public class Index
    {
        public string IndexValue { get; set; }
        public long DataBlockStartPosition { get; set; }
        public long DataBlockLength { get; set; }
    }

    public class IndexBlock
    {
        public List<Index> IndexList { get; set; }

        public long NextIndexBlockPosition { get; set; }

        public bool IsLeaf { get; set; }

        public static int MaxIndexCount = 10;

    }
    public class IndexFile
    {
        public List<IndexBlock> IndexBlockList { get; set; }
    }

    public class IndexBytesConverter
    {
        public List<byte> ConvertToByes(Index index)
        {
            List<byte> list = new List<byte>();
            var indexValueBytes = index.IndexValue.ToBytes();
            list.AddRange(indexValueBytes.Length.ToBytes());
            list.AddRange(indexValueBytes);
            list.AddRange(index.DataBlockStartPosition.ToBytes());
            list.AddRange(index.DataBlockLength.ToBytes());
            return list;
        }
    }
}
