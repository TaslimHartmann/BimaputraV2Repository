﻿#pragma checksum "..\..\..\MasterData\MstPembayaran.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E08C2C6A8D7E781EFF26E8C5BA723C0D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using ExtendedButton;
using RootLibrary.WPF.Localization;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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


namespace BPDMH.MasterData {
    
    
    /// <summary>
    /// MstPembayaran
    /// </summary>
    public partial class MstPembayaran : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\..\MasterData\MstPembayaran.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TbId;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\MasterData\MstPembayaran.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TbKet;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\MasterData\MstPembayaran.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ExtendedButton.ImageButton BtnBaru;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\MasterData\MstPembayaran.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ExtendedButton.ImageButton BtnSimpan;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\MasterData\MstPembayaran.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ExtendedButton.ImageButton BtnHapus;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\MasterData\MstPembayaran.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ExtendedButton.ImageButton BtnClose;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\MasterData\MstPembayaran.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView ListViewPembayaran;
        
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
            System.Uri resourceLocater = new System.Uri("/BPDMH;component/masterdata/mstpembayaran.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\MasterData\MstPembayaran.xaml"
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
            
            #line 7 "..\..\..\MasterData\MstPembayaran.xaml"
            ((BPDMH.MasterData.MstPembayaran)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.TbId = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.TbKet = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.BtnBaru = ((ExtendedButton.ImageButton)(target));
            
            #line 22 "..\..\..\MasterData\MstPembayaran.xaml"
            this.BtnBaru.Click += new System.Windows.RoutedEventHandler(this.BtnBaru_OnClick);
            
            #line default
            #line hidden
            return;
            case 5:
            this.BtnSimpan = ((ExtendedButton.ImageButton)(target));
            
            #line 23 "..\..\..\MasterData\MstPembayaran.xaml"
            this.BtnSimpan.Click += new System.Windows.RoutedEventHandler(this.BtnSave_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.BtnHapus = ((ExtendedButton.ImageButton)(target));
            
            #line 24 "..\..\..\MasterData\MstPembayaran.xaml"
            this.BtnHapus.Click += new System.Windows.RoutedEventHandler(this.BtnHapus_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.BtnClose = ((ExtendedButton.ImageButton)(target));
            
            #line 25 "..\..\..\MasterData\MstPembayaran.xaml"
            this.BtnClose.Click += new System.Windows.RoutedEventHandler(this.BtnClose_OnClick);
            
            #line default
            #line hidden
            return;
            case 8:
            this.ListViewPembayaran = ((System.Windows.Controls.ListView)(target));
            
            #line 30 "..\..\..\MasterData\MstPembayaran.xaml"
            this.ListViewPembayaran.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ListViewPembayaran_SelectionChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

