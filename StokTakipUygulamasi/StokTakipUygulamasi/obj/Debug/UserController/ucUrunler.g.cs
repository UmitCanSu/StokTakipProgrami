﻿#pragma checksum "..\..\..\UserController\ucUrunler.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "A30FE26D148DA734B8EEA4214D79351B3AC62ACE6BA5EBC7224919C9ED5B53A4"
//------------------------------------------------------------------------------
// <auto-generated>
//     Bu kod araç tarafından oluşturuldu.
//     Çalışma Zamanı Sürümü:4.0.30319.42000
//
//     Bu dosyada yapılacak değişiklikler yanlış davranışa neden olabilir ve
//     kod yeniden oluşturulursa kaybolur.
// </auto-generated>
//------------------------------------------------------------------------------

using StokTakipUygulamasi.UserController;
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


namespace StokTakipUygulamasi.UserController {
    
    
    /// <summary>
    /// ucUrunler
    /// </summary>
    public partial class ucUrunler : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 18 "..\..\..\UserController\ucUrunler.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dtg_UrunlerListesi;
        
        #line default
        #line hidden
        
        
        #line 148 "..\..\..\UserController\ucUrunler.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnGuncelle;
        
        #line default
        #line hidden
        
        
        #line 149 "..\..\..\UserController\ucUrunler.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSil;
        
        #line default
        #line hidden
        
        
        #line 150 "..\..\..\UserController\ucUrunler.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox check_Satista_Olmayanlar;
        
        #line default
        #line hidden
        
        
        #line 154 "..\..\..\UserController\ucUrunler.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnUrunEkle;
        
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
            System.Uri resourceLocater = new System.Uri("/StokTakipUygulamasi;component/usercontroller/ucurunler.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UserController\ucUrunler.xaml"
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
            
            #line 8 "..\..\..\UserController\ucUrunler.xaml"
            ((StokTakipUygulamasi.UserController.ucUrunler)(target)).Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.dtg_UrunlerListesi = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 3:
            this.btnGuncelle = ((System.Windows.Controls.Button)(target));
            
            #line 148 "..\..\..\UserController\ucUrunler.xaml"
            this.btnGuncelle.Click += new System.Windows.RoutedEventHandler(this.btnGuncelle_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnSil = ((System.Windows.Controls.Button)(target));
            
            #line 149 "..\..\..\UserController\ucUrunler.xaml"
            this.btnSil.Click += new System.Windows.RoutedEventHandler(this.btnSil_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.check_Satista_Olmayanlar = ((System.Windows.Controls.CheckBox)(target));
            
            #line 150 "..\..\..\UserController\ucUrunler.xaml"
            this.check_Satista_Olmayanlar.Checked += new System.Windows.RoutedEventHandler(this.check_Satista_Olmayanlar_Checked);
            
            #line default
            #line hidden
            
            #line 150 "..\..\..\UserController\ucUrunler.xaml"
            this.check_Satista_Olmayanlar.Unchecked += new System.Windows.RoutedEventHandler(this.check_Satista_Olmayanlar_Unchecked);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnUrunEkle = ((System.Windows.Controls.Button)(target));
            
            #line 154 "..\..\..\UserController\ucUrunler.xaml"
            this.btnUrunEkle.Click += new System.Windows.RoutedEventHandler(this.btnUrunEkle_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

