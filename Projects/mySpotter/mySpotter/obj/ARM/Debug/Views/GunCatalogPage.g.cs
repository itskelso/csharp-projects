﻿#pragma checksum "C:\Users\Kellan\Documents\Visual Studio 2015\Projects\mySpotter\mySpotter\Views\GunCatalogPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "ADBFA3E365AE0F06919C8DC57A31BCDD"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace mySpotter.Views
{
    partial class GunCatalogPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    this.Catalog = (global::mySpotterLibrary.ViewModels.GunCatalogViewModel)(target);
                }
                break;
            case 2:
                {
                    this.rootGrid = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 3:
                {
                    global::Windows.UI.Xaml.Controls.GridView element3 = (global::Windows.UI.Xaml.Controls.GridView)(target);
                    #line 21 "..\..\..\Views\GunCatalogPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.GridView)element3).ItemClick += this.GridView_ItemClick;
                    #line 21 "..\..\..\Views\GunCatalogPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.GridView)element3).RightTapped += this.GridView_RightTapped;
                    #line default
                }
                break;
            case 4:
                {
                    this.AmmoGridView = (global::Windows.UI.Xaml.Controls.GridView)(target);
                }
                break;
            case 5:
                {
                    this.ScopeGridView = (global::Windows.UI.Xaml.Controls.GridView)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

