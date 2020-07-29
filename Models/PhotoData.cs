using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ImageViewer.Models
{
    class RootFolderData
    {
        private string _RootFolderPath;
        public string RootFolderPath
        {
            get { return this._RootFolderPath; }
            set { this._RootFolderPath = value; }
        }
    }

    class PhotoData
    {
        private string _Title;
        public string Title
        {
            get { return this._Title; }
            set { this._Title = value; }
        }

        private string _FilePath;
        public string FilePath
        {
            get { return this._FilePath; }
            set { this._FilePath = value; }
        }

        private BitmapImage _ImageData;
    
        public BitmapImage ImageData
        {
            get { return this._ImageData; }
            set { this._ImageData = value; }
        }
    }
}
