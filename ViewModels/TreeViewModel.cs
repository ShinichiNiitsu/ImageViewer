using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using ImageViewer.Models;

namespace ImageViewer.ViewModels
{
    public class DirectoryViewModel : INotifyPropertyChanged
    {
        // サブディレクトリの取得を遅らせるときに使うダミー
        private static DirectoryViewModel Dummy = new DirectoryViewModel();

        // Dummy 専用のコンストラクタ
        private DirectoryViewModel()
        {
        }

        // コンストラクタ
        public DirectoryViewModel(DirectoryViewModel parent, string path)
        {
            if (Directory.Exists(path) == false)
            {
                throw new ArgumentException(path + " は存在しません。", "path");
            }
            _model = new DirectoryInfo(path);
            _parent = parent;
            _children = new ReadOnlyCollection<DirectoryViewModel>(new DirectoryViewModel[]{ Dummy });
        }

        // モデル
        private DirectoryInfo _model;

        public string Name
        {
            get { return _model.Name; }
        }

        private bool _isExpanded;

        // ノードが開いているかどうか
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if (_isExpanded == value)
                {
                    return;
                }
                _isExpanded = value;
                OnPropertyChanged("IsExpanded");
            }
        }

        private bool _isSelected;

        // ノードが選択されているかどうか
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected == value)
                {
                    return;
                }
                _isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        private DirectoryViewModel _parent;

        // 親ディレクトリのビューモデル
        public DirectoryViewModel Parent
        {
            get { return _parent; }
        }

        // サブディレクトリを取得済みかどうかを判断
        public bool HasDummy
        {
            get
            {
                return (_children.Count == 1) && (_children[0] == Dummy);
            }
        }

        private ReadOnlyCollection<DirectoryViewModel> _children;

        // サブディレクトリの一覧。
        // 必要になるまで取得を遅らせている。
        public ReadOnlyCollection<DirectoryViewModel> Children
        {
            get
            {
                if (HasDummy)
                {
                    List<DirectoryViewModel> list = new List<DirectoryViewModel>();
                    foreach (var info in _model.GetDirectories())
                    {
                        list.Add(new DirectoryViewModel(this, info.FullName));
                    }
                    _children = new ReadOnlyCollection<DirectoryViewModel>(list);
                }
                return _children;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }
    }

}
