using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBase
{
    public class Index : IByteConvertor
    {
        /// <summary>
        /// 所属的块
        /// </summary>
        public IndexBlock ParentBlock { get; set; }

        public bool IsLeaf
        {
            get
            {
                return ParentBlock.IsLeaf;
            }
        }

        /// <summary>
        /// [导航属性]指向的下级索引块
        /// </summary>
        public IndexBlock SubBlock { get; set; }

        /// <summary>
        /// 持久化元数据
        /// </summary>
        public string IndexValue { get; set; }
        /// <summary>
        /// 持久化元数据
        /// </summary>
        public long DataBlockStartPosition { get; set; }
        /// <summary>
        /// 持久化元数据
        /// </summary>
        public long DataBlockLength { get; set; }

        public List<byte> GetBytes()
        {
            List<byte> bsList = new List<byte>();
            bsList.AddRange(IndexValue.ToBytes());
            bsList.AddRange(DataBlockStartPosition.ToBytes());
            bsList.AddRange(DataBlockLength.ToBytes());
            return bsList;
        }

        public Block DataBlock { get; set; }
    }

    public class IndexBlock : IByteConvertor
    {
        public List<Index> IndexList { get; set; }

        public long NextIndexBlockPosition { get; set; }

        public bool IsLeaf { get; set; }

        public static int MaxIndexCount = 10;

        public static int MaxSize = 100;

        public List<byte> GetBytes()
        {
            var bsList = new List<byte>();
            bsList.AddRange(IsLeaf.ToBytes());
            foreach(var index in IndexList)
            {
                bsList.AddRange(index.GetBytes());
            }
            return bsList;
        }

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
