using System.IO;
using System.Windows;

namespace ImageViewer.ViewModels
{

    /// <summary>
    /// ViewModel
    /// </summary>
    public class TextBoxViewModel : ViewModelBase
    {

        #region Property

        /// <summary>
        /// TextBoxに表示する文字列のプロパティ
        /// </summary>
        public string Text
        {
            get
            {

                // 内部変数の値を戻す
                return _Text;

            }
            set
            {

                // 入力値:valueを内部変数にセット
                _Text = value;

                // プロパティ変更イベントを発生させViewに通知する
                CallPropertyChanged();

            }
        }

        /// <summary>
        /// ボタンクリック処理コマンドプロパティ
        /// </summary>
        public DelegateCommand ButtonClickCommand { get; }

        #endregion

        /// <summary>
        /// TextBoxに表示する文字列の内部変数
        /// </summary>
        private string _Text = string.Empty;

        /// <summary>
        /// ViewModel
        /// </summary>
        public TextBoxViewModel()
        {

            // コマンドの設定
            ButtonClickCommand = new DelegateCommand(
                () => { InputTextBox(); },  // 実行メソッド
                () => { return true; }      // 実行メソッド許可
                );

        }

        /// <summary>
        /// TextBoxにHello World!を表示
        /// </summary>
        public void InputTextBox()
        {

            // フォルダー参照ダイアログのインスタンスを生成
            var dlg = new System.Windows.Forms.FolderBrowserDialog();

            // 説明文を設定
            dlg.Description = "フォルダーを選択してください。";

            //デフォルト設定
            if (Directory.Exists(Text))
            {
                dlg.SelectedPath = Text;
            }

            // フォルダ選択ダイアログを表示
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Text = dlg.SelectedPath;

                if (Directory.Exists(Text))
                {
                    //var item = new DirectoryTreeItem(new System.IO.DirectoryInfo(Text_Folder.Text));
                    //TreeView_Folder.Items.Clear();
                    //TreeView_Folder.Items.Add(item);

                    Application.Current.Properties["FolderPath"] = Text;
                }
            }
            //Text = "Hello World!";

            // Textプロパティ内で処理するのでコメントアウト
            CallPropertyChanged(nameof(Text));

        }

    }

}
