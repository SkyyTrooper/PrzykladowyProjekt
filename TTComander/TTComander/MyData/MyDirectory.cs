using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTComander.MyData
{
    /// <summary>
    /// Klasa Dziedzicząca po DiscElement
    /// </summary>
    class MyDirectory : DiscElement
    {

        public MyDirectory (string dirPath): base(dirPath)
        {

        }
        public override string Name
        {
            get
            {
                return System.IO.Path.GetFileNameWithoutExtension(Path);
            }
        }
        public override DateTime CreationTime
        {
            get
            {
                return Directory.GetCreationTime(Path);
            }
        }

        /// <summary>
        /// Metoda tworzy listę wszystkich elementów znajdujących się w folderze
        /// </summary>
        /// <returns></returns>
        public List<DiscElement> GetDiscElements()
        {
            List<DiscElement> result = new List<DiscElement>();
            result.AddRange(GetAllFiles());
            result.AddRange(GetSubDirectories());
            return result;

        }
        /// <summary>
        /// zwraca wszystkie pliki w folderze
        /// </summary>
        /// <returns></returns>
        public List<MyFile> GetAllFiles()
        {
            string[] subFiles = Directory.GetFiles(Path);
            List<MyFile> result = new List<MyFile>();
            foreach (string file in subFiles)
            {
                result.Add(new MyFile(file));
            }
            return result;
        }

        /// <summary>
        /// zwraca wszystkie podfoldery w danym folderze
        /// </summary>
        /// <returns></returns>
        public MyDirectory[] GetSubDirectories()
        {
            string[] subDirs = Directory.GetDirectories(Path);
            List<MyDirectory> result = new List<MyDirectory>();
            foreach (string dir in subDirs)
            {
                result.Add(new MyDirectory(dir));
            }
            return result.ToArray();
        }




    }
}
