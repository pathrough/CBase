using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBase
{
    public class Index : IByteConvertor
    {
        //create
        public Index(IndexBlock parentBlock, string indexValue)
        {
            this.ParentBlock = parentBlock;
            this.IndexValue = indexValue;
        }

        public Index(IndexBlock parentBlock, long lngPosition)
        {
            this.ParentBlock = parentBlock;
        }

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
        public IndexBlock SubBlock
        {
            get;
        }

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
        readonly IndexFile _IndexFile;
        public IndexFile IndexFile { 
            get
            {
                return _IndexFile;
            }
        }

        ////创建
        //public IndexBlock(bool isLeaf, IndexFile file)
        //{
        //    this._IsLeaf = isLeaf;
        //    this._File = file;
        //}

        readonly byte[] _ByteArray;

        byte[] ByteArray
        {
            get { return _ByteArray; }
        }

        //获取
        public IndexBlock(IndexFile file, long lngPosition, int iBlockLength)
        {
            _IndexFile = file;
            IndexFile.File.Position = lngPosition;

            byte[] bs = new byte[iBlockLength];
            IndexFile.File.Read(bs, 0, bs.Length);
            _ByteArray = bs;
        }

        public IndexBlock (IndexFile file,bool isLeaf)
        {

        }

        //readonly int _BlockLength;

        const int ISLEAF_INDEX = 0;
        const int NEXT_BLOCK_POSITION_INDEX = 1;
        const int NEXT_BLOCK_LENGTH_INDEX = 10;

        bool? _IsLeaf;
        public bool IsLeaf
        {
            get
            {
                if (_IsLeaf == null)
                {
                    byte[] bs = new byte[1];
                    ByteArray.CopyTo(bs, ISLEAF_INDEX);
                    _IsLeaf = bs.ToBoolean();
                }
                return _IsLeaf.Value;
            }
        }

        long? _NextBlockPosition;

        public long NextBlockPosition
        {
            get 
            {
                if (_NextBlockPosition==null)
                {
                    byte[] bs = new byte[8];
                    ByteArray.CopyTo(bs, NEXT_BLOCK_POSITION_INDEX);
                    _NextBlockPosition = bs.ToInt64();
                }
                return _NextBlockPosition.Value;
            }
        }

        int? _NextBlockLength;

        public int NextBlockLength
        {
            get
            {
                if (_NextBlockLength==null)
                {
                     byte[] bs = new byte[4];
                    ByteArray.CopyTo(bs, NEXT_BLOCK_LENGTH_INDEX);
                    _NextBlockLength = bs.ToInt32();
                }
                return _NextBlockLength.Value; 
            }
        } 


        IndexBlock _NextBlock;
        IndexBlock NextBlock
        {
            get
            {
                if (_NextBlock == null)
                {
                    _NextBlock = new IndexBlock(this.IndexFile, NextBlockPosition, NextBlockLength);
                }
                return _NextBlock;
            }
        }

        List<Index> _IndexList;
        public List<Index> IndexList
        {
            get
            {
                //todo:_IndexList
                return _IndexList;
            }
        }


        public List<byte> GetBytes()
        {
            var bsList = new List<byte>();
            bsList.AddRange(IsLeaf.ToBytes());
            foreach (var index in IndexList)
            {
                bsList.AddRange(index.GetBytes());
            }
            return bsList;
        }

    }
    public class IndexFile
    {
        readonly string _FileName;
        string FileName
        {
            get { return _FileName; }
        }

        readonly FileStream _File;
        public FileStream File
        {
            get { return _File; }
        }


        public IndexFile(string fileName)
        {
            _FileName = fileName;
            _File = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);

          
        }

        IndexBlock _FirstBlock;

        public IndexBlock FirstBlock
        {
            get
            {
                if (_FirstBlock == null)
                {
                    if (this.FirstIndexBlockLength==0)
                    {
                        //创建
                        _FirstBlock = new IndexBlock(this,true);
                    }
                    else
                    {
                        _FirstBlock = new IndexBlock(this, this.File.Position, this.FirstIndexBlockLength);
                    }
                    
                }
                return _FirstBlock;
            }
        }

        int? _FirstIndexBlockLength;

        int FirstIndexBlockLength
        {
            get 
            {
                if(_FirstIndexBlockLength==null)
                {
                    //第一个索引块的长度
                    byte[] bs = new byte[4];
                    File.Read(bs, 0, bs.Length);
                    _FirstIndexBlockLength = bs.ToInt32();
                }               
                return _FirstIndexBlockLength; 
            }
        }



        //List<IndexBlock> _IndexBlockList;
        //public List<IndexBlock> IndexBlockList
        //{
        //    get
        //    {
        //        if(_IndexBlockList==null)
        //        {
        //            //todo:_IndexBlockList io
        //            if(_IndexBlockList==null)
        //            {
        //                _IndexBlockList = new List<IndexBlock>();
        //            }
        //        }
        //        return _IndexBlockList;
        //    }
        //}

        //IndexBlock GetFirstIndexBlock()
        //{
        //    File.Position = 0;
        //    byte[] bs = new byte[8];
        //    File.Read(bs, 0, bs.Length);
        //    long blnBlockLen = bs.ToInt64();

        //}

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
