//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************
using GalaSoft.MvvmLight;
using TableCrib.Views;
using TableCrib.Helpers;
using TableCrib.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TableCrib
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NavigationRootPage : Page
    {
        public static NavigationRootPage Current;
        public static Frame RootFrame = null;
        public static MyTitleBar TitleBar;
        public static DatabaseHelper dbHelper;


        public bool MyArmoryIsChecked = false;

      //  private MyArmory _setups = new MyArmory();
      //  public MyArmory Setups
     //   {
     //       get { return _setups; }
     //   }

      //  public event EventHandler GroupsLoaded;

        //public static CommandBar TopCommandBar
        //{
        //    get { return Current.topCommandBar; }
        //}

        public static SplitView RootSplitView
        {
            get { return Current.rootSplitView; }
        }

        public NavigationRootPage()
        {
            this.InitializeComponent();

           // LoadGroups();
            Current = this;
            RootFrame = rootFrame;

            Loaded += NavigationRootPage_Loaded;

            //Use the hardware back button instead of showing the back button in the page
            if (ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                Windows.Phone.UI.Input.HardwareButtons.BackPressed += (s, e) =>
                {
                    if (rootFrame.CanGoBack)
                    {
                        rootFrame.GoBack();
                        e.Handled = true;
                    }
                };
            }
        }

        public void LocateViewModel()
        {
          // MainPage.Current.Frame.
        }

        

        async void NavigationRootPage_Loaded(object sender, RoutedEventArgs e)
        {
             //   this.DataContext = this;

            AddCustomTitleBar();
          //  dbHelper = new DatabaseHelper(App.SQLITE_PLATFORM, App.DB_PATH);
         //   this.Setups.MySetups = new ObservableCollection<UserGunSetup>(dbHelper.ReadAllGeneric<UserGunSetup>());
          //  this.MyArmoryListView.ItemsSource = Setups.MySetups;

        }

        public void AddCustomTitleBar()
        {
            if (TitleBar == null)
            {
                TitleBar = new MyTitleBar();
                TitleBar.EnableControlsInTitleBar(true);

                // Make the main page's content a child of the title bar,
                // and make the title bar the new page content.
                UIElement mainContent = this.Content;
                this.Content = null;
                TitleBar.SetPageContent(mainContent);
                this.Content = TitleBar;
            }
        }


        private void AppBar_Closed(object sender, object e)
        {
            //this.navControl.HideSecondLevelNav();
        }

        private void UserSetup_ItemClick(object sender, ItemClickEventArgs e)
        {
            var s = sender;
          //  this.rootFrame.Navigate(typeof(UserGunSetupDetailPage),(e.ClickedItem as UserGunSetup));
            TitleBar.GoBackCommand.RaiseCanExecuteChanged();
            // this.rootFrame.Navigate(typeof(ItemPage), (e.ClickedItem as ControlInfoDataItem).UniqueId);
          //   rootSplitView.IsPaneOpen = false;
        }

        private void homeButton_Click(object sender, RoutedEventArgs e)
        {
        //    this.rootFrame.Navigate(typeof(MainPage));
        //    rootSplitView.IsPaneOpen = false;
        }

        public void NavigateToSession()
        {


            //this.rootFrame.Navigate(typeof(SessionPage),Setup);
            TitleBar.GoBackCommand.RaiseCanExecuteChanged();
        }

        public void userSetup_Click(object sender, RoutedEventArgs e)
        {
            var s = sender;
          //  this.rootFrame.Navigate(typeof(UserGunSetupDetailPage));
            TitleBar.GoBackCommand.RaiseCanExecuteChanged();
        }

        private void MyArmoryButton_Click(object sender, RoutedEventArgs e)
        {
            MyArmoryIsChecked = true;
             
        }

        private void CatalogButton_Click(object sender, RoutedEventArgs e)
        {
          //  this.rootFrame.Navigate(typeof(GunCatalogPage));
            TitleBar.GoBackCommand.RaiseCanExecuteChanged();
        }
    }
    }
