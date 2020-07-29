using ImageViewer.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Drawing;

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

            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.UriSource = new Uri(_ImageFilePath);
            bmp.EndInit();

            var transformedBitmap = new TransformedBitmap();
            transformedBitmap.BeginInit();
            transformedBitmap.Source = bmp;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //グレースケール変換
            var src = new Mat(_ImageFilePath);
            var dst = new Mat();
            Cv2.CvtColor(src, dst, ColorConversionCodes.BGRA2GRAY);
            System.Drawing.Bitmap hBitmap = dst.ToBitmap();
            IntPtr handle = hBitmap.GetHbitmap();
            var bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                handle,
                System.IntPtr.Zero,
                System.Windows.Int32Rect.Empty,
                System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions()
            );
            ImageEdit1.Source = bitmapSource;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Bitmap hBitmap = new Bitmap(_ImageFilePath);

            //セピア調の画像の描画先となるImageオブジェクトを作成
            Bitmap newImg = new Bitmap(hBitmap.Width, hBitmap.Height);

            //newImgのGraphicsオブジェクトを取得
            Graphics g = Graphics.FromImage(newImg);

            //ColorMatrixオブジェクトの作成
            //セピア調に変換するための行列を指定する
            System.Drawing.Imaging.ColorMatrix cm =
                new System.Drawing.Imaging.ColorMatrix(
                    new float[][] {
                new float[] {.393f, .349f, .272f, 0, 0},
                new float[] {.769f, .686f, .534f, 0, 0},
                new float[] {.189f, .168f, .131f, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {0, 0, 0, 0, 1}
                    });
            //ImageAttributesオブジェクトの作成
            System.Drawing.Imaging.ImageAttributes ia = new System.Drawing.Imaging.ImageAttributes();
            //ColorMatrixを設定する
            ia.SetColorMatrix(cm);

            //ImageAttributesを使用してセピア調に描画
            g.DrawImage(hBitmap, new Rectangle(0, 0, hBitmap.Width, hBitmap.Height), 0, 0, hBitmap.Width, hBitmap.Height, GraphicsUnit.Pixel, ia);

            //リソースを解放する
            g.Dispose();

            IntPtr handle = hBitmap.GetHbitmap();
            var bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                handle,
                System.IntPtr.Zero,
                System.Windows.Int32Rect.Empty,
                System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions()
            );

            ImageEdit1.Source = bitmapSource;

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            //顔の矩形を抽出
            using (Mat mat = new Mat(_ImageFilePath))
            {
                // 分類機の用意
                using (CascadeClassifier cascade = new CascadeClassifier(@"haarcascade_frontalface_default.xml"))
                {
                    foreach (OpenCvSharp.Rect rectFace in cascade.DetectMultiScale(mat))
                    {
                        // 見つかった場所に赤枠を表示
                        OpenCvSharp.Rect rect = new OpenCvSharp.Rect(rectFace.X, rectFace.Y, rectFace.Width, rectFace.Height);
                        Cv2.Rectangle(mat, rect, new OpenCvSharp.Scalar(0, 0, 255), 2);
                    }
                }

                // ウィンドウに画像を表示
                Cv2.ImShow("face_show", mat);
            }

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ImageEdit1.Source = _bmp;
        }
    }
}
