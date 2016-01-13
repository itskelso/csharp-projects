﻿using mySpotter.Common;
using mySpotterLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace mySpotter.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GunCatalogPage : Page
    {
        private NavigationHelper navigationHelper;


        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }


        public GunCatalogPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
        }

        public async void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
           // var t = e.NavigationParameter as UserGunSetup;
            // this.DataContext = t;
          //  this.Setup.Setup = t;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is GunDataGroup)
            {
                (sender as GridView).ItemsSource = (e.ClickedItem as GunDataGroup).Guns;
            }
        }

        private void GridView_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            (sender as GridView).ItemsSource = this.Catalog.Guns.GunGroups;
        }
    }
}
