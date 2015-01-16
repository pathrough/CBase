using System;
using System.Collections.Generic;
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
            if(im.HaveIndex==false)
            {
                im.CreateIndexBlock();
            }
            else
            {
                var ib = im.GetFirstIndexBlock();
                Index target=null;
                foreach(var index in ib.IndexList)
                {
                    if(index.IndexValue==record.Value)
                    {
                        target = index;
                        break;
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

        public void CreateIndexBlock()
        {
            
        }
    }
}
