using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBase
{
    //public class Manager
    //{
    //    public void AddRecord(Record record)
    //    {
    //        IndexManager im = new IndexManager();
    //        DataManager dm = new DataManager();
    //        if (IndexBlock.ExistsBlock() == false)
    //        {
    //            try
    //            {
    //                IndexBlock.CreateIndexBlock(record.Value);
    //                dm.CreateDataBlock(record);
    //            }
    //            catch (Exception)
    //            {
    //                //回滚
    //                throw;
    //            }
    //        }
    //        else
    //        {
    //            var ib = IndexBlock.FirstIndexBlock;
    //            var leafIndex = GetLeafIndex(im, dm, record, ib);
    //            Block dataBlock = leafIndex.DataBlock;
    //            dataBlock.AppendRecord(record);
    //        }
    //    }

    //    Index GetLeafIndex(IndexManager im, DataManager dm, Record record, IndexBlock indexBlock)
    //    {
    //        Index target = null;
    //        foreach (var index in indexBlock.IndexList)
    //        {
    //            if (index.IndexValue.CompareTo(record.Value) >= 1)
    //            {
    //                target = index;
    //                break;
    //            }
    //        }

    //        if (target == null)
    //        {
    //            //找不到
    //            target = indexBlock.IndexList[indexBlock.IndexList.Count - 1];//qu最后一个
    //        }

    //        if (target.IsLeaf)
    //        {
    //            return target;
    //        }
    //        else
    //        {
    //            return GetLeafIndex(im, dm, record, target.SubBlock);
    //        }
    //    }
    //}

    //public class IndexManager
    //{
    //}

    //public class DataManager
    //{
    //    public void CreateDataBlock(Record record)
    //    {
    //        Block b = new Block(new List<Record> { record });
    //        using (FileStream fs = new FileStream("data", FileMode.OpenOrCreate, FileAccess.ReadWrite))
    //        {
    //            byte[] bs = b.GetBytes().ToArray();
    //            fs.Write(bs, 0, bs.Length);
    //        }
    //    }
    //}
}
