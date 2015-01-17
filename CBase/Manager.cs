using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBase
{
    public class Manager
    {
        public void AddRecord(Record record)
        {
            IndexManager im = new IndexManager();
            DataManager dm = new DataManager();
            if(im.HaveIndex==false)
            {
                im.CreateIndexBlock(record.Value);
                dm.CreateDataBlock(record);
            }
            else
            {
                var ib = im.GetFirstIndexBlock();
                Index target=null;
                foreach(var index in ib.IndexList)
                {
                    if(index.IndexValue.CompareTo(record.Value)>=1)
                    {
                        target = index;
                        break;
                    }
                }

                if(target==null)
                {
                    //找不到
                }
                else
                {
                    if(target.Block.IsLeaf)
                    {

                    }
                }
            }
        }
    }

    public class IndexManager
    {
        public bool HaveIndex { get; set; }
        public IndexBlock GetFirstIndexBlock()
        {
            return new IndexBlock();
        }

        public IndexBlock GetNextIndexBlock(IndexBlock current)
        {
            return new IndexBlock();
        }

        public void CreateIndexBlock(string value)
        {
            IndexBlock ib = new IndexBlock();
            ib.IsLeaf = true;
            using(FileStream fs = new FileStream("index",FileMode.OpenOrCreate,FileAccess.ReadWrite))
            {
                byte[] bs= ib.GetBytes().ToArray();
                fs.Write(bs, 0, bs.Length);
            }
        }
    }

    public class DataManager
    {
        public void CreateDataBlock(Record record)
        {
            Block b = new Block(new List<Record> { record });
            using (FileStream fs = new FileStream("data", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                byte[] bs = b.GetBytes().ToArray();
                fs.Write(bs, 0, bs.Length);
            }
        }
    }
}
