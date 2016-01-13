using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System.Profile;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using mySpotterLibrary.ViewModels;
using System.Threading.Tasks;
using mySpotterLibrary.Helpers;
using mySpotter.Views;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace mySpotter
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static MainPage Current;
        public static Frame RootFrame = null;

        private DatabaseHelper dbHelper;
        
        //public WorkOutManager workOutsource;

        public MainPage()
        {
            this.InitializeComponent();


            //  CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;

            // var currentView = SystemNavigationManager.GetForCurrentView();
            //currentView.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;

      //      var deviceFamily = AnalyticsInfo.VersionInfo.DeviceFamily;
       //     if (deviceFamily == "Windows.Desktop")
      //      {
      //          AddCustomTitleBar();
      //      }

       //     if (deviceFamily == "Windows.Mobile")
       //     {

        //        AlterStatusBar();
        //    }
            dbHelper = new DatabaseHelper(App.SQLITE_PLATFORM, App.DB_PATH);
            //MainView is in the xaml
            // MainView.ViewModel = new MainPageViewModel(dbHelper);
            
            NavigationCacheMode = NavigationCacheMode.Required;
            RootFrame = frame;
        }


      //  MyTitleBar customTitleBar = null;
/*
        public void AddCustomTitleBar()
        {
            if (customTitleBar == null)
            {
                customTitleBar = new MyTitleBar();
                customTitleBar.EnableControlsInTitleBar(true);

                // Make the main page's content a child of the title bar,
                // and make the title bar the new page content.
                UIElement mainContent = this.Content;
                this.Content = null;
                customTitleBar.SetPageContent(mainContent);
                this.Content = customTitleBar;
            }
        }

        public async void AlterStatusBar()
        {
            var statusBar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();
            statusBar.BackgroundColor = Windows.UI.Colors.Black;
            statusBar.ForegroundColor = Windows.UI.Colors.Blue;
            statusBar.BackgroundOpacity = 0;

            //  await Windows.UI.ViewManagement.StatusBar.GetForCurrentView().HideAsync();

        }

    */
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        
    }
}
