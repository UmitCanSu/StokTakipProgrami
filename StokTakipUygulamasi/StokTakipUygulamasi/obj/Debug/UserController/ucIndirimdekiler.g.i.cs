﻿#pragma checksum "..\..\..\UserController\ucIndirimdekiler.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "B86ED995B6A879E40A3C27E6EACC237BF6568CAD3945140B8CEE74F85E5EC5C4"
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
    /// ucIndirimdekiler
    /// </summary>
    public partial class ucIndirimdekiler : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 19 "..\..\..\UserController\ucIndirimdekiler.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dtg_IndirimdekilerListesi;
        
        #line default
        #line hidden
        
        
        #line 190 "..\..\..\UserController\ucIndirimdekiler.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnGuncelle;
        
        #line default
        #line hidden
        
        
        #line 191 "..\..\..\UserController\ucIndirimdekiler.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCikar;
        
        #line default
        #line hidden
        
        
        #line 192 "..\..\..\UserController\ucIndirimdekiler.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox check_Indirimde_Olmayanlar;
        
        #line default
        #line hidden
        
        
        #line 196 "..\..\..\UserController\ucIndirimdekiler.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnIndirimliUrunEkle;
        
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
            System.Uri resourceLocater = new System.Uri("/StokTakipUygulamasi;component/usercontroller/ucindirimdekiler.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UserController\ucIndirimdekiler.xaml"
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
            
            #line 8 "..\..\..\UserController\ucIndirimdekiler.xaml"
            ((StokTakipUygulamasi.UserController.ucIndirimdekiler)(target)).Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.dtg_IndirimdekilerListesi = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 3:
            this.btnGuncelle = ((System.Windows.Controls.Button)(target));
            
            #line 190 "..\..\..\UserController\ucIndirimdekiler.xaml"
            this.btnGuncelle.Click += new System.Windows.RoutedEventHandler(this.btnGuncelle_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnCikar = ((System.Windows.Controls.Button)(target));
            
            #line 191 "..\..\..\UserController\ucIndirimdekiler.xaml"
            this.btnCikar.Click += new System.Windows.RoutedEventHandler(this.btnCikar_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.check_Indirimde_Olmayanlar = ((System.Windows.Controls.CheckBox)(target));
            
            #line 192 "..\..\..\UserController\ucIndirimdekiler.xaml"
            this.check_Indirimde_Olmayanlar.Checked += new System.Windows.RoutedEventHandler(this.check_Indirimde_Olmayanlar_Checked);
            
            #line default
            #line hidden
            
            #line 192 "..\..\..\UserController\ucIndirimdekiler.xaml"
            this.check_Indirimde_Olmayanlar.Unchecked += new System.Windows.RoutedEventHandler(this.check_Indirimde_Olmayanlar_Unchecked);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnIndirimliUrunEkle = ((System.Windows.Controls.Button)(target));
            
            #line 196 "..\..\..\UserController\ucIndirimdekiler.xaml"
            this.btnIndirimliUrunEkle.Click += new System.Windows.RoutedEventHandler(this.btnIndirimliUrunEkle_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

