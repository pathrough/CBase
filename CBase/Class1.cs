using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBase
{
    public class Record
    {
        public long ID { get; set; }
        //public int KeyLen { get; set; }
        public string Key { get; set; }
        //public int ValueLen { get; set; }
        public string Value { get; set; }
    }

    public class Block
    {
        //public long StartPosition { get; set; }
        //public int Length { get; set; }

        public List<Record> RecordList { get; set; }

        public static int MaxLenghth=100;
    }

    public class DataFile
    {
        public List<Block> BlockList { get; set; }
    }

    public class Index
    {

    }

    public class IndexBlock
    {
        public List<Index> IndexList{ get; set; }
    }
    public class IndexFile
    {
        public List<IndexBlock> IndexBlockList { get; set; }
    }
        
    public interface IRecordBytesConverter
    {
        List<byte> ConvertToByes(Record record);
    }

    public class RecordBytesConverter:IRecordBytesConverter
    {
        public List<byte> ConvertToByes(Record record)
        {
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

    public static class BytesConvertUtility
    {
        public static byte[] ToBytes(this string input)
        {
            return Encoding.UTF8.GetBytes(input);
        }

        public static byte[] ToBytes(this int input)
        {
            return BitConverter.GetBytes(input);
        }

        public static byte[] ToBytes(this long input)
        {
            return BitConverter.GetBytes(input);
        }
    }

}
