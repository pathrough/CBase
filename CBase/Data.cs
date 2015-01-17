using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBase
{
    public interface IByteConvertor
    {
        List<byte> GetBytes();
    }
    public class Record : IByteConvertor
    {
        public long ID { get; set; }
        //public int KeyLen { get; set; }
        public string Key { get; set; }
        //public int ValueLen { get; set; }
        public string Value { get; set; }

        public List<byte> GetBytes()
        {
            var record = this;
            List<byte> list = new List<byte>();
            list.AddRange(record.ID.ToBytes());
            var keyBytes = record.Key.ToBytes();
            list.AddRange(keyBytes.Length.ToBytes());
            list.AddRange(keyBytes);
            var valueBytes = record.Value.ToBytes();
            list.AddRange(valueBytes.Length.ToBytes());
            list.AddRange(valueBytes);
            return list;
        }
    }

    public class Block : IByteConvertor
    {
        public Block( List<Record> recordList)
        {
            RecordList = recordList;
        }
        public List<Record> RecordList { get; set; }

        public static int MaxLenghth=100;

        public List<byte> GetBytes()
        {
            return new List<byte>();
        }
    }

    public class DataFile
    {
        public List<Block> BlockList { get; set; }
    } 
}
