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
using Microsoft.Win32;
using System.IO;

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

        #region 切り抜き領域指定
        private bool isDown;
        private System.Windows.Point p1;
        private System.Windows.Point p2;

        private System.Windows.Rect GetClipRect()
        {
            double x1 = Math.Max(0, Math.Min(p1.X, p2.X));
            double y1 = Math.Max(0, Math.Min(p1.Y, p2.Y));
            double x2 = Math.Max(x1, Math.Max(p1.X, p2.X));
            double y2 = Math.Max(y1, Math.Max(p1.Y, p2.Y));
            double w = Math.Abs(x2 - x1);
            double h = Math.Abs(y2 - y1);
            return new System.Windows.Rect(x1, y1, w, h);
        }
        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Canvas canvas = (Canvas)sender;
            p1 = e.GetPosition(canvas);
            isDown = true;
            Mouse.Capture((Canvas)sender);
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDown)
            {
                Canvas canvas = (Canvas)sender;
                p2 = e.GetPosition(canvas);

                System.Windows.Rect rect = GetClipRect();
                Canvas.SetLeft(clipRect, rect.Left);
                Canvas.SetTop(clipRect, rect.Top);
                clipRect.Width = rect.Width;
                clipRect.Height = rect.Height;
            }
        }
        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(null);
            isDown = false;
        }

        private void slScale_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.clipRect != null)
            {
                //切り取り領域の点線の太さを縮尺の逆数で
                clipRect.StrokeThickness = 2 / e.NewValue;
            }
        }

        #endregion

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

        //マウスホイールで画像の拡大縮小
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

        //グレースケール
        private void Btn_Gray_Click(object sender, RoutedEventArgs e)
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

        //セピア
        private void Btn_Sepia_Click(object sender, RoutedEventArgs e)
        {
            //グレースケール変換
            var src = new Mat(_ImageFilePath);
            var dst = new Mat();
            Cv2.CvtColor(src, dst, ColorConversionCodes.BGRA2GRAY);
            Cv2.ApplyColorMap(src, dst, ColormapTypes.Pink);
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

        //戻す
        private void Btn_Reset_Click(object sender, RoutedEventArgs e)
        {
            ImageEdit1.Source = _bmp;
        }

        //保存
        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            // ファイル保存ダイアログを生成します。
            var dialog = new SaveFileDialog();

            // フィルターを設定します。
            // この設定は任意です。
            dialog.Filter = "画像ファイル|*.jpg|*.png|*.bmp|全てのファイル(*.*)|*.*";

            // ファイル保存ダイアログを表示します。
            var result = dialog.ShowDialog() ?? false;

            // 保存ボタン以外が押下された場合
            if (!result)
            {
                // 終了します。
                return;
            }


        }

        //顔検出
        private void Btn_FaceDetect_Click(object sender, RoutedEventArgs e)
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

        /// <summary> 画像を切り出す </summary>
        /// <param name="bmpBase">元画像</param>
        /// <param name="rect">切り出し範囲</param>
        /// <returns>切り出した画像</returns>
        public static Bitmap CreateClipBitmap(Bitmap bmpBase, Rectangle rect)
        {
            Bitmap bmp = new Bitmap(rect.Width, rect.Height, bmpBase.PixelFormat);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.DrawImage(bmpBase, new Rectangle(0, 0, bmp.Width, bmp.Height), rect, GraphicsUnit.Pixel);
            }
            return bmp;
        }

        //切取
        private void Btn_Clip_Click(object sender, RoutedEventArgs e)
        {
            Bitmap bmpBase = BitmapImage2Bitmap((BitmapImage)ImageEdit1.Source);

            if ( bmpBase != null )
            {
                //クリップ領域を表示画像領域(imageGrid)の座標に変換
                System.Windows.Rect r = GetClipRect();
                System.Windows.Point p1 = clipCanvas.TranslatePoint(r.TopLeft, imageGrid);
                System.Windows.Point p2 = clipCanvas.TranslatePoint(r.BottomRight, imageGrid);
                double wpfWidth = imageGrid.ActualWidth;
                double wpfHeight = imageGrid.ActualHeight;
                {

                    double x1 = Math.Min((double)bmpBase.Width * p1.X / wpfWidth, bmpBase.Width);
                    double y1 = Math.Min((double)bmpBase.Height * p1.Y / wpfHeight, bmpBase.Height);
                    double x2 = Math.Min((double)bmpBase.Width * p2.X / wpfWidth, bmpBase.Width);
                    double y2 = Math.Min((double)bmpBase.Height * p2.Y / wpfHeight, bmpBase.Height);
                    x1 = x1 * (bmpBase.Width / wpfWidth);
                    y1 = y1 * (bmpBase.Height / wpfHeight);
                    x2 = x2 * (bmpBase.Width / wpfWidth);
                    y2 = y2 * (bmpBase.Height / wpfHeight);
                    double w = x2 - x1;
                    double h = y2 - y1;
                    System.Drawing.Rectangle rect
                       = new System.Drawing.Rectangle((int)x1, (int)y1, (int)w, (int)h);

                    //回転後画像を切り抜き
                    var bmpCliped = CreateClipBitmap(bmpBase, rect);
                    IntPtr hbitmap = bmpCliped.GetHbitmap();
                    ImageEdit1.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hbitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions()); ;

                }
            }
        }

        private Bitmap BitmapImage2Bitmap(BitmapImage bitmapImage)
        {
            // BitmapImage bitmapImage = new BitmapImage(new Uri("../Images/test.png", UriKind.Relative));

            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(outStream);

                return new Bitmap(bitmap);
            }
        }
    }
}
