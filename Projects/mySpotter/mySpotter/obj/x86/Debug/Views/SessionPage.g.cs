﻿#pragma checksum "C:\Users\Kellan\Documents\Visual Studio 2015\Projects\mySpotter\mySpotter\Views\SessionPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "77DA7A47BB2E82C25288F213C34FEF27"
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
    partial class SessionPage : 
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
                    this.SessionViewModel = (global::mySpotterLibrary.ViewModels.SessionViewModel)(target);
                }
                break;
            case 2:
                {
                    this.currentImageinView = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 3:
                {
                    this.ButtonStackPanel = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 4:
                {
                    this.LogBlock = (global::Windows.UI.Xaml.Controls.ListView)(target);
                }
                break;
            case 5:
                {
                    this.ConnectButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                }
                break;
            case 6:
                {
                    global::Windows.UI.Xaml.Controls.Button element6 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 45 "..\..\..\Views\SessionPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)element6).Click += this.Button_Click;
                    #line default
                }
                break;
            case 7:
                {
                    this.TestButton1 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 46 "..\..\..\Views\SessionPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.TestButton1).Click += this.TestButton1_Click;
                    #line default
                }
                break;
            case 8:
                {
                    this.TestButton2 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 47 "..\..\..\Views\SessionPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.TestButton2).Click += this.TestButton2_Click;
                    #line default
                }
                break;
            case 9:
                {
                    this.model = (global::mySpotter.Views.UserGunSetupView)(target);
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
