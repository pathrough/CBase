using CBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Manager m = new Manager();
            //m.AddRecord(new Record { ID=1,Key="name",Value="hugo"});
            IndexFile indexFile = new IndexFile(Config.INDEX_FILE_NAME);
        }
    }
}
