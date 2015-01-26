﻿using System;
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
        public IndexBlock SubBlock {
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
        readonly IndexFile _File;
        public IndexFile IndexFile { get; }

        //创建
        public IndexBlock(bool isLeaf,IndexFile file)
        {
            this._IsLeaf = isLeaf;
            this._File = file;
        }

        //获取
        public IndexBlock(IndexFile file,long lngPosition)
        {
            IndexFile.File.Position = lngPosition;

            byte[] bs = new byte[4];
            IndexFile.File.Read(bs, 0, bs.Length);
            _BlockLength = bs.ToInt32();

            bs = new byte[8];
            IndexFile.File.Read(bs, 0, bs.Length);
            _NextBlockPosition = bs.ToInt64();

            bs = new byte[1];
            IndexFile.File.Read(bs, 0, bs.Length);
            _IsLeaf = bs.ToBoolean();
        }

        readonly int _BlockLength;

        readonly long _NextBlockPosition;

        IndexBlock _NextBlock;
        IndexBlock NextBlock
        {
            get
            {
                if (_NextBlock==null)
                {
                    _NextBlock = new IndexBlock(this.IndexFile,_NextBlockPosition);
                }
                return _NextBlock;
            }
        }

        //public static IndexBlock CreateIndexBlock(string value, IndexFile file)
        //{
        //    IndexBlock ib = new IndexBlock(isLeaf: true, file: file);
        //    ib.IndexList.Add(new Index(indexValue: value, parentBlock: ib));
        //    using (FileStream fs = new FileStream(Config.INDEX_FILE_NAME, FileMode.OpenOrCreate, FileAccess.ReadWrite))
        //    {
        //        byte[] bs = ib.GetBytes().ToArray();
        //        fs.Write(bs, 0, bs.Length);
        //    }
        //    return ib;
        //}

        List<Index> _IndexList;
        public List<Index> IndexList {
            get
            {
                return _IndexList;
            }
        }

        public int IndexCount { get; set; }

        //public long NextIndexBlockPosition { get; set; }

        readonly bool _IsLeaf;
        public bool IsLeaf { get;}

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
                    _FirstBlock = new IndexBlock(this, 0);
                }
                return _FirstBlock;
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
        
        IndexBlock GetFirstIndexBlock()
        {
            File.Position = 0;
            byte[] bs = new byte[8];
            File.Read(bs, 0, bs.Length);
            long blnBlockLen = bs;

        }

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
