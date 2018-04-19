using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTComander.MyData
{
    /// <summary>
    /// Klasa abstrakcyjna będąca klasą nadrzędna dla elementów dysku
    /// </summary>
    public abstract class DiscElement
    {
        string path;

        public DiscElement(string path)
        {
            this.path = path;
        }
        public string Path
        {
            get
            {
                return path;
            }

        }
        public abstract DateTime CreationTime
        {
            get;
        }
        public abstract string Name
        {
            get;
        }
    }
}
