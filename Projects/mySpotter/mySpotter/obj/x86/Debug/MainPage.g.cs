﻿#pragma checksum "C:\Users\Kellan\Documents\Visual Studio 2015\Projects\mySpotter\mySpotter\DeviceFamily-Mobile\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "65FC9CEF134FFED152C113052F314E81"
#pragma checksum "C:\Users\Kellan\Documents\Visual Studio 2015\Projects\mySpotter\mySpotter\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "319E372B0D520AE1AFA6B20C7787D956"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace mySpotter
{
    partial class MainPage : 
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
                    this.SetupEditor = (global::mySpotter.Views.UserGunSetupView)(target);
                }
                break;
            case 2:
                {
                    this.PiManager = (global::mySpotter.Views.PiManagerView)(target);
                }
                break;
            case 3:
                {
                    global::Windows.UI.Xaml.Controls.Page element3 = (global::Windows.UI.Xaml.Controls.Page)(target);
                    #line 10 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Page)element3).Loaded += this.Page_Loaded;
                    #line default
                }
                break;
            case 4:
                {
                    this.frame = (global::Windows.UI.Xaml.Controls.Frame)(target);
                }
                break;
            case 5:
                {
                    this.MainView = (global::mySpotter.Views.MainPageView)(target);
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
