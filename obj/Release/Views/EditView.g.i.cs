﻿#pragma checksum "..\..\..\Views\EditView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "A47AD3A1380222D8613F96887E9EC984E963A01473E1E1C34446EB27FEAAD3FF"
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
    /// EditView
    /// </summary>
    public partial class EditView : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\..\Views\EditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid imageGrid;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\Views\EditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image ImageEdit1;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\Views\EditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas clipCanvas;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\Views\EditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle clipRect;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\Views\EditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView ListView_ExifInfo;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\Views\EditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Btn_Rotate;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\Views\EditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Btn_Gray;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\Views\EditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Btn_Sepia;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\Views\EditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Btn_FaceDetect;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\Views\EditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Btn_Clip;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\Views\EditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Btn_Reset;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\Views\EditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Btn_Save;
        
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
            System.Uri resourceLocater = new System.Uri("/ImageViewer;component/views/editview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\EditView.xaml"
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
            this.imageGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.ImageEdit1 = ((System.Windows.Controls.Image)(target));
            
            #line 17 "..\..\..\Views\EditView.xaml"
            this.ImageEdit1.MouseWheel += new System.Windows.Input.MouseWheelEventHandler(this.ImageEdit1_MouseWheel);
            
            #line default
            #line hidden
            return;
            case 3:
            this.clipCanvas = ((System.Windows.Controls.Canvas)(target));
            
            #line 25 "..\..\..\Views\EditView.xaml"
            this.clipCanvas.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Canvas_MouseDown);
            
            #line default
            #line hidden
            
            #line 26 "..\..\..\Views\EditView.xaml"
            this.clipCanvas.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.Canvas_MouseUp);
            
            #line default
            #line hidden
            
            #line 27 "..\..\..\Views\EditView.xaml"
            this.clipCanvas.MouseMove += new System.Windows.Input.MouseEventHandler(this.Canvas_MouseMove);
            
            #line default
            #line hidden
            return;
            case 4:
            this.clipRect = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 5:
            this.ListView_ExifInfo = ((System.Windows.Controls.ListView)(target));
            return;
            case 6:
            this.Btn_Rotate = ((System.Windows.Controls.Button)(target));
            
            #line 53 "..\..\..\Views\EditView.xaml"
            this.Btn_Rotate.Click += new System.Windows.RoutedEventHandler(this.Rotate_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.Btn_Gray = ((System.Windows.Controls.Button)(target));
            
            #line 54 "..\..\..\Views\EditView.xaml"
            this.Btn_Gray.Click += new System.Windows.RoutedEventHandler(this.Btn_Gray_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.Btn_Sepia = ((System.Windows.Controls.Button)(target));
            
            #line 55 "..\..\..\Views\EditView.xaml"
            this.Btn_Sepia.Click += new System.Windows.RoutedEventHandler(this.Btn_Sepia_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.Btn_FaceDetect = ((System.Windows.Controls.Button)(target));
            
            #line 56 "..\..\..\Views\EditView.xaml"
            this.Btn_FaceDetect.Click += new System.Windows.RoutedEventHandler(this.Btn_FaceDetect_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.Btn_Clip = ((System.Windows.Controls.Button)(target));
            
            #line 57 "..\..\..\Views\EditView.xaml"
            this.Btn_Clip.Click += new System.Windows.RoutedEventHandler(this.Btn_Clip_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.Btn_Reset = ((System.Windows.Controls.Button)(target));
            
            #line 58 "..\..\..\Views\EditView.xaml"
            this.Btn_Reset.Click += new System.Windows.RoutedEventHandler(this.Btn_Reset_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.Btn_Save = ((System.Windows.Controls.Button)(target));
            
            #line 59 "..\..\..\Views\EditView.xaml"
            this.Btn_Save.Click += new System.Windows.RoutedEventHandler(this.Btn_Save_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

