using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImageViewer.Views
{
    /// <summary>
    /// EditView.xaml の相互作用ロジック
    /// </summary>
    public partial class EditView : Page
    {
        public EditView(string filePath)
        {
            InitializeComponent();

            //string filePath = (string)Application.Current.Properties["ImagePath"];
            SetImage(filePath);
            GetExif(filePath);
        }

        //ImageViewに画像セット
        private bool SetImage(string filePath)
        {
            // パスが空
            if (string.IsNullOrEmpty(filePath))
            {
                return false;
            }

            var bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.UriSource = new Uri(filePath);
            bmp.EndInit();
            // 画像設定
            ImageEdit1.Source = bmp;
            ImageEdit1.Stretch = Stretch.Fill;

            // タイトルバーにファイル名設定
            this.Title = System.IO.Path.GetFileName(filePath);

            return true;
        }

        //Exif情報取得
        private void GetExif(string filePath)
        {
            // パスが空
            if (string.IsNullOrEmpty(filePath))
            {
                return;
            }

            //画像を読み込む
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(filePath);
            
            //Exif情報を列挙する
            foreach (System.Drawing.Imaging.PropertyItem item in bmp.PropertyItems)
            {
                //データの型を判断
                if (item.Type == 2)
                {
                    //ASCII文字の場合は、文字列に変換する
                    string val = System.Text.Encoding.ASCII.GetString(item.Value);
                    val = val.Trim(new char[] { '\0' });
                    //表示する
                    Console.WriteLine("{0:X}:{1}:{2}", item.Id, item.Type, val);
                }
                else
                {
                    //表示する
                    Console.WriteLine("{0:X}:{1}:{2}", item.Id, item.Type, item.Len);
                }
            }
            bmp.Dispose();
        }

    }
}
