﻿#pragma checksum "..\..\..\Views\ImageView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "5A4F08110B21AEE60CA0BB4590C9666B0210F95F7DA0036CE0F9373B04D485E6"
//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:4.0.30319.42000
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

using ImageViewer.Views;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace ImageViewer.Views {
    
    
    /// <summary>
    /// ImageView
    /// </summary>
    public partial class ImageView : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 17 "..\..\..\Views\ImageView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Text_Folder;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\Views\ImageView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Btn_Folder;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\Views\ImageView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TreeView TreeView_Folder;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\Views\ImageView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView ListView_thumbnail;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ImageViewer;component/views/imageview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\ImageView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Text_Folder = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.Btn_Folder = ((System.Windows.Controls.Button)(target));
            return;
            case 3:
            this.TreeView_Folder = ((System.Windows.Controls.TreeView)(target));
            return;
            case 4:
            this.ListView_thumbnail = ((System.Windows.Controls.ListView)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

