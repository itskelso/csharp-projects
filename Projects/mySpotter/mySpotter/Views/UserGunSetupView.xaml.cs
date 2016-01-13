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
using mySpotterLibrary.ViewModels;
using mySpotterLibrary.Models;
using mySpotter.Common;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace mySpotter.Views
{
    public sealed partial class UserGunSetupView : UserControl
    {
        public UserGunSetupViewModel vm;

        public UserGunSetupView()
        {
            this.InitializeComponent();
            // UserSetupViewModel.Setup = new UserGunSetup();
            //  gungroupssource.Source = UserSetupViewModel.Groups;
            vm = new UserGunSetupViewModel(1, new mySpotterLibrary.Helpers.DatabaseHelper(App.SQLITE_PLATFORM,App.DB_PATH));
            this.DataContext = vm;
            
        }

        private void GunGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            
        }

        private void Flyout_Closed(object sender, object e)
        {
            GunGridView.ItemsSource = (rootGrid.DataContext as UserGunSetupViewModel).Groups.GunGroups;
        }

        private void AmmoGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int i = 0;
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
              NavigationRootPage f = Window.Current.Content as NavigationRootPage;
            //  NavigationRootPage.RootFrame.Navigate(typeof(SessionPage));
            f.NavigateToSession(vm.Setup);
          //  NavigationRootPage.RootFrame.Navigate(typeof(SessionPage));
           // NavigationRootPage.RootFrame = new SessionPage();
           // f.ToSession(UserSetupViewModel.Setup.UserGunSetupId);
         //  MainPage.RootFrame.Navigate(typeof(SessionPage));
         //   MainPage.RootFrame.CanGoBack 
            ///  NavigationHelper nav = new NavigationHelper();
            

        }

        private void GunButton_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
