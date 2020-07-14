using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
    /// MainView.xaml の相互作用ロジック
    /// </summary>
    public partial class MainView : NavigationWindow
    {
        public MainView()
        {
            InitializeComponent();
            
            //フォルダ用ルートパスをアプリケーションコンフィグから取得
            Application.Current.Properties["FolderPath"] = Properties.Settings.Default.RootPath;
        }

        //Windowを閉じるとき
        void Window_Closing(object sender, CancelEventArgs e)
        {
            //フォルダ用ルートパスをアプリケーションコンフィグに保存
            Properties.Settings.Default.RootPath = (string)Application.Current.Properties["FolderPath"];
            Properties.Settings.Default.Save();
        }
    }
}
