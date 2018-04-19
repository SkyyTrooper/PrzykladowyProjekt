using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTComander.MyData
{
    /// <summary>
    /// Klasa dziedzicząca po DiscElement
    /// </summary>
    class MyFile : DiscElement
    {
        private string name;
        public MyFile (string path) : base(path)
        {

        }
        

        public override DateTime CreationTime
        {
            get
            {
                return File.GetCreationTime(Path);
            }
        }
        public override string Name
        {
            get
            {
                return name = System.IO.Path.GetFileName(Path);
            }
        }

    }
}
