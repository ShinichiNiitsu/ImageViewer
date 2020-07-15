using ImageViewer.ViewModels;
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
        private string _ImageFilePath;
        private BitmapImage _bmp;
        private int _angleCount = 0;

        public EditView(string filePath)
        {
            InitializeComponent();

            _ImageFilePath = filePath;
            //string filePath = (string)Application.Current.Properties["ImagePath"];
            SetImage(_ImageFilePath);
            GetExif(_ImageFilePath);
            _angleCount = 0;
        }

        //ImageViewに画像セット
        private bool SetImage(string filePath)
        {
            // パスが空
            if (string.IsNullOrEmpty(filePath))
            {
                return false;
            }

            _bmp = new BitmapImage();
            _bmp.BeginInit();
            _bmp.UriSource = new Uri(filePath);
            _bmp.EndInit();
            // 画像設定
            ImageEdit1.Source = _bmp;
            ImageEdit1.Stretch = Stretch.Uniform;

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
                ExifInfoData exinfo = new ExifInfoData();
                Console.WriteLine("{0:X}:{1}", item.Id, item.Type);
                if (ImageViewModel.ConstExifTagDictionary.ContainsKey(item.Id)) 
                {
                    exinfo.ItemName = ImageViewModel.ConstExifTagDictionary[item.Id];
                    string val = string.Empty;

                    switch (item.Type)
                    {
                        case 2://ASCII 文字列
                            val = System.Text.Encoding.ASCII.GetString(item.Value);
                            break;
                        case 3://符号なし short (16 ビット) 整数
                            val = BitConverter.ToInt16(item.Value, 0).ToString();
                            break;
                        case 4://符号なし long (32 ビット) 整数
                            val = BitConverter.ToInt32(item.Value, 0).ToString();
                            break;
                    }
                    Console.WriteLine("{0:X}:{1}:{2}:{3}", item.Id, item.Type, val, exinfo.ItemName);
                    exinfo.Data = val;
                    //表示する
                    ListView_ExifInfo.Items.Add(exinfo);
                }
            }
            bmp.Dispose();
        }

        //回転ボタン
        private void Rotate_Click(object sender, RoutedEventArgs e)
        {
            _angleCount++;
            if (_angleCount > 3)
            {
                _angleCount = 0;
            }

            int rotate = 0;
            switch (_angleCount)
            {
                case 0:
                    rotate = 0;
                    break;
                case 1:
                    rotate = 90;
                    break;
                case 2:
                    rotate = 180;
                    break;
                case 3:
                    rotate = 270;
                    break;
                default:
                    break;
            }

            var transformedBitmap = new TransformedBitmap();
            transformedBitmap.BeginInit();
            transformedBitmap.Source = _bmp;
            transformedBitmap.Transform = new RotateTransform(rotate);
            transformedBitmap.EndInit();

            // 画像設定
            ImageEdit1.Source = transformedBitmap;
        }

        private void Btn_Expansion_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ImageEdit1_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            // スケールの値を変えることでホイールを動かした時の拡大率を制御できます
            const double scale = 1.2;

            var matrix = ImageEdit1.RenderTransform.Value;
            if (e.Delta > 0)
            {
                // 拡大処理
                matrix.ScaleAt(scale, scale, e.GetPosition(this).X, e.GetPosition(this).Y);
            }
            else
            {
                // 縮小処理
                matrix.ScaleAt(1.0 / scale, 1.0 / scale, e.GetPosition(this).X, e.GetPosition(this).Y);
            }

            ImageEdit1.RenderTransform = new MatrixTransform(matrix);
        }

        //
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            //Escキーで戻す
            if (e.Key == Key.Escape)
            {
                var matrix = ImageEdit1.RenderTransform.Value;
                matrix.M11 = 1.0;
                matrix.M12 = 0.0;
                matrix.M21 = 0.0;
                matrix.M22 = 1.0;
                matrix.OffsetX = 0.0;
                matrix.OffsetY = 0.0;
                ImageEdit1.RenderTransform = new MatrixTransform(matrix);
            }
        }
    }
}
