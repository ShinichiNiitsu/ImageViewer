using ImageViewer.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImageViewer.Views
{
    /// <summary>
    /// ImageView.xaml の相互作用ロジック
    /// </summary>
    public partial class ImageView : Page
    {

        private ObservableCollection<BitmapImage> imageArray = new ObservableCollection<BitmapImage>();
        private ObservableCollection<String> uriArray = new ObservableCollection<String>();
        public ObservableCollection<BitmapImage> ImageArray { get => imageArray; set => imageArray = value; }

        public ImageView()
        {
            InitializeComponent();

            Text_Folder.Text = Properties.Settings.Default.RootPath;

            //TreeViewにルートフォルダ設定
            if (Directory.Exists(Text_Folder.Text)) { 
                var item = new DirectoryTreeItem(new System.IO.DirectoryInfo(Text_Folder.Text));
                TreeView_Folder.Items.Clear();
                TreeView_Folder.Items.Add(item);
            }
        }

        //フォルダ選択ボタン
        private void Btn_Folder_Click(object sender, RoutedEventArgs e)
        {

            // フォルダー参照ダイアログのインスタンスを生成
            var dlg = new System.Windows.Forms.FolderBrowserDialog();

            // 説明文を設定
            dlg.Description = "フォルダーを選択してください。";

            //デフォルト設定
            if (Directory.Exists(Text_Folder.Text)) { 
                dlg.SelectedPath = Text_Folder.Text;
            }

            // フォルダ選択ダイアログを表示
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Text_Folder.Text = dlg.SelectedPath;

                if (Directory.Exists(Text_Folder.Text))
                {
                    var item = new DirectoryTreeItem(new System.IO.DirectoryInfo(Text_Folder.Text));
                    TreeView_Folder.Items.Clear();
                    TreeView_Folder.Items.Add(item);

                    Application.Current.Properties["FolderPath"] = Text_Folder.Text;
                }
            }
        }

        //TreViewの選択状態が変わったらListView更新
        private void SelectionChanged(object sender, RoutedPropertyChangedEventArgs<Object> e)
        {
            if ( e.NewValue == null ) { return; }

            try 
            {
                string strFolder = ((TreeViewItem)e.NewValue).ToString();
                Console.WriteLine(strFolder);
                List<String> imageList = GetImagesPath(strFolder);
                ListView_thumbnail.Items.Clear();
                ImageView1.Source = null;
                foreach (var item in imageList)
                {
                    PhotoData phData = new PhotoData();
                    phData.FilePath = item;
                    phData.Title = Path.GetFileName(item);
                    phData.ImageData = createThumbnail(item,50,50);
                    ListView_thumbnail.Items.Add(phData);
                }
            }
            catch(Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        //ListViewの選択状態が変わったらプレビュー更新
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListView_thumbnail.SelectedItem == null) { return; }// ListViewで何も選択されていない場合は何もしない
            PhotoData item = (PhotoData)ListView_thumbnail.SelectedItem; // ListViewで選択されている項目を取り出す
            SetImage(item.FilePath);
        }

        //ImageViewに画像セット
        private bool SetImage(string filePath)
        {
            // パスが空
            if (string.IsNullOrEmpty(filePath)){ return false; }

            var bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.UriSource = new Uri(filePath);
            bmp.EndInit();
            // 画像設定
            ImageView1.Source = bmp;
            ImageView1.Stretch = Stretch.Fill;

            // タイトルバーにファイル名設定
            this.Title = System.IO.Path.GetFileName(filePath);

            return true;
        }

        //サムネイル画像の作成
        // 幅w、高さhのImageオブジェクトを作成
        BitmapImage createThumbnail(String filePath, int w, int h)
        {
            var bmpImg = new BitmapImage();
            bmpImg.BeginInit();

            // オプション : 読込元のファイルがロックされない。
            bmpImg.CacheOption = BitmapCacheOption.OnLoad;
            bmpImg.CreateOptions = BitmapCreateOptions.None;

            // オプション : decoded image の幅を指定できる。サムネイル用に小さく表示する場合などのメモリ節約に有効。
            bmpImg.DecodePixelWidth = w;
            bmpImg.DecodePixelHeight = h;

            bmpImg.UriSource = new Uri(filePath);
            bmpImg.EndInit();
            bmpImg.Freeze();

            return bmpImg;
        }

        private List<String> GetImagesPath(String folderName)
        {
            string ImagesExtensions = "jpg,jpeg,png,gif,bmp,tif,tiff";
            string[] imageValues = ImagesExtensions.Split(',');
            List<String> imagesList = new List<String>();

            string[] dirs = Directory.GetFiles(folderName);

            foreach (var filepath in dirs)
            {
                if (!string.IsNullOrEmpty(filepath.Trim()))
                {
                    foreach (var ext in imageValues)
                    {
                        if (!string.IsNullOrEmpty(ext.Trim()))
                        {
                            if (filepath.IndexOf(ext, StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                imagesList.Add(filepath);
                                break;
                            }
                        }
                    }
                }
            }
            return imagesList;
        }

        //Editボタン
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (ListView_thumbnail.SelectedItem == null) return; // ListViewで何も選択されていない場合は何もしない

            PhotoData item = (PhotoData)ListView_thumbnail.SelectedItem; // ListViewで選択されている項目を取り出す
            
            Application.Current.Properties["ImagePath"] = item.FilePath;

            // Pageインスタンスを渡して遷移
            var nextPage = new EditView(item.FilePath);
            NavigationService.Navigate(nextPage);

        }

    }

    //フォルダツリーViewの作成
    public class DirectoryTreeItem : TreeViewItem
    {
        public readonly System.IO.DirectoryInfo DirectoryInfo;
        private bool IsAdd;//サブフォルダを作成済みかどうか
        private TreeViewItem Dummy;//ダミーアイテム


        public DirectoryTreeItem(System.IO.DirectoryInfo info)
        {
            DirectoryInfo = info;
            Header = info.Name;

            //サブフォルダが1つでもあれば
            if (info.GetDirectories().Length > 0)
            //展開できることを示す▷を表示するためにダミーのTreeViewItemを追加する
            {
                Console.WriteLine(info.GetDirectories().Length.ToString());
                Dummy = new TreeViewItem();
                Items.Add(Dummy);
            }

            //イベント、ツリー展開時
            //サブフォルダを追加
            this.Expanded += (s, e) =>
            {
                if (IsAdd) return;//追加済みなら何もしない
                AddSubDirectory();
            };
        }

        //サブフォルダツリー追加
        public void AddSubDirectory()
        {
            Items.Remove(Dummy);//ダミーのTreeViewItemを削除

            //すべてのサブフォルダを追加
            System.IO.DirectoryInfo[] directories = DirectoryInfo.GetDirectories();
            for (int i = 0; i < directories.Length; i++)
            {
                //隠しフォルダ、システムフォルダは除外する
                var fileAttributes = directories[i].Attributes;
                if ((fileAttributes & System.IO.FileAttributes.Hidden) == System.IO.FileAttributes.Hidden ||
                        (fileAttributes & System.IO.FileAttributes.System) == System.IO.FileAttributes.System)
                {
                    continue;
                }
                //追加
                Items.Add(new DirectoryTreeItem(directories[i]));
            }
            IsAdd = true;//サブフォルダ作成済みフラグ
        }

        public override string ToString()
        {
            return DirectoryInfo.FullName;
        }
    }

}
