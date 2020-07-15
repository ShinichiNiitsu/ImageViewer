using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ImageViewer.ViewModels
{
    class ImageViewModel
    {
        public static readonly System.Collections.ObjectModel.ReadOnlyDictionary<int, string> ConstExifTagDictionary = new System.Collections.ObjectModel.ReadOnlyDictionary<int, string>(new Dictionary<int, string>()
        {
            {256, "画像の幅"},
            {257, "画像の高さ"},
            {258, "画像のビットの深さ"},
            {270, "画像タイトル"},
            {271, "画像入力機器のメーカー名"},
            {272, "画像入力機器のモデル名"},
            {273, "画像データのロケーション"},
            {274, "画像方向"},
        });

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

    class ExifInfoData
    {
        private string _ItemName;
        public string ItemName
        {
            get { return this._ItemName; }
            set { this._ItemName = value; }
        }

        private string _Data;
        public string Data
        {
            get { return this._Data; }
            set { this._Data = value; }
        }

    }
}
