using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
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
using TableCrib.Common;
using System.Windows.Input;
using Windows.UI.ViewManagement;
using System.ComponentModel;
using Windows.UI.Core;
using Windows.System.Profile;
using GalaSoft.MvvmLight.Command;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TableCrib
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MyTitleBar : UserControl, INotifyPropertyChanged

    {
        private CoreApplicationViewTitleBar _coreTitleBar = CoreApplication.GetCurrentView().TitleBar;

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(object), typeof(MyTitleBar), new PropertyMetadata(null));
        public object Title
        {
            get { return GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty WideLayoutThresholdProperty = DependencyProperty.Register("WideLayoutThreshold", typeof(double), typeof(MyTitleBar), new PropertyMetadata(600));
        public double WideLayoutThreshold
        {
            get { return (double)GetValue(WideLayoutThresholdProperty); }
            set
            {
                SetValue(WideLayoutThresholdProperty, value);
                WideLayoutTrigger.MinWindowWidth = value;
            }
        }

        //CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;

        public MyTitleBar()
        {
            this.InitializeComponent();


            var a = AnalyticsInfo.VersionInfo.DeviceFamily;
            _coreTitleBar.ExtendViewIntoTitleBar = true;
           // if (ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
          //  {
                //Remove the backbutton because physical buttons are present
            //    backButton.Visibility = Visibility.Collapsed;
           // }
        }


        private RelayCommand _goBackCommand;
        public RelayCommand GoBackCommand
        {
            get
            {
                if (_goBackCommand == null)
                {
                    _goBackCommand = new RelayCommand(() =>
                    {
                        NavigationRootPage.RootFrame.GoBack();
                        GoBackCommand.RaiseCanExecuteChanged();
                    }, () =>
                    {
                        return NavigationRootPage.RootFrame != null &&
                        NavigationRootPage.RootFrame.CanGoBack;
                    });
                }
                return _goBackCommand;
            }
        }

        private void splitViewToggle_Click(object sender, RoutedEventArgs e)
        {
            NavigationRootPage.RootSplitView.IsPaneOpen = !NavigationRootPage.RootSplitView.IsPaneOpen;
          //  CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
        }

       

       


        void MyTitleBar_Loaded(object sender, RoutedEventArgs e)
        {
            _coreTitleBar.LayoutMetricsChanged += OnLayoutMetricsChanged;
            _coreTitleBar.IsVisibleChanged += OnIsVisibleChanged;

            // The SizeChanged event is raised when the view enters or exits full screen mode.
            Window.Current.SizeChanged += OnWindowSizeChanged;

            UpdateLayoutMetrics();
            UpdatePositionAndVisibility();
        }

        void MyTitleBar_Unloaded(object sender, RoutedEventArgs e)
        {
            _coreTitleBar.LayoutMetricsChanged -= OnLayoutMetricsChanged;
            _coreTitleBar.IsVisibleChanged -= OnIsVisibleChanged;
            Window.Current.SizeChanged -= OnWindowSizeChanged;
        }

        void OnLayoutMetricsChanged(CoreApplicationViewTitleBar sender, object e)
        {
            UpdateLayoutMetrics();
        }

        void UpdateLayoutMetrics()
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("CoreTitleBarHeight"));
                PropertyChanged(this, new PropertyChangedEventArgs("CoreTitleBarPadding"));
            }
        }

        void OnIsVisibleChanged(CoreApplicationViewTitleBar sender, object e)
        {
            UpdatePositionAndVisibility();
        }

        void OnWindowSizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            UpdatePositionAndVisibility();
        }

        // We wrap the normal contents of the MainPage inside a grid
        // so that we can place a title bar on top of it.
        //
        // When not in full screen mode, the grid looks like this:
        //
        //      Row 0: Custom-rendered title bar
        //      Row 1: Rest of content
        //
        // In full screen mode, the the grid looks like this:
        //
        //      Row 0: (empty)
        //      Row 1: Custom-rendered title bar
        //      Row 1: Rest of content
        //
        // The title bar is either Visible or Collapsed, depending on the value of
        // the IsVisible property.
        void UpdatePositionAndVisibility()
        {
            if (ApplicationView.GetForCurrentView().IsFullScreenMode)
            {
                // In full screen mode, the title bar overlays the content.
                // and might or might not be visible.
                TitleBar.Visibility = _coreTitleBar.IsVisible ? Windows.UI.Xaml.Visibility.Visible : Windows.UI.Xaml.Visibility.Collapsed;
                Grid.SetRow(TitleBar, 1);
            }
            else
            {
                // When not in full screen mode, the title bar is visible and does not overlay content.
                TitleBar.Visibility = Windows.UI.Xaml.Visibility.Visible;
                Grid.SetRow(TitleBar, 0);
            }
        }

        UIElement pageContent = null;

        public UIElement SetPageContent(UIElement newContent)
        {
            UIElement oldContent = pageContent;
            if (oldContent != null)
            {
                pageContent = null;
                RootGrid.Children.Remove(oldContent);
            }
            pageContent = newContent;
            if (newContent != null)
            {
                RootGrid.Children.Add(newContent);
                // The page content is row 1 in our grid. (See diagram above.)
                Grid.SetRow((FrameworkElement)pageContent, 1);
            }
            return oldContent;
        }

        #region Data binding
        public event PropertyChangedEventHandler PropertyChanged;

        public Thickness CoreTitleBarPadding
        {
            get
            {
                // The SystemOverlayLeftInset and SystemOverlayRightInset values are
                // in terms of physical left and right. Therefore, we need to flip
                // then when our flow direction is RTL.
                if (FlowDirection == FlowDirection.LeftToRight)
                {
                    return new Thickness() { Left = _coreTitleBar.SystemOverlayLeftInset, Right = _coreTitleBar.SystemOverlayRightInset };
                }
                else
                {
                    return new Thickness() { Left = _coreTitleBar.SystemOverlayRightInset, Right = _coreTitleBar.SystemOverlayLeftInset };
                }
            }
        }

        public double CoreTitleBarHeight
        {
            get
            {
                return _coreTitleBar.Height;
            }
        }
        #endregion

        public void EnableControlsInTitleBar(bool enable)
        {
            if (enable)
            {
                // TitleBarControl.Visibility = Visibility.Visible;
                // Clicks on the BackgroundElement will be treated as clicks on the title bar.
                double x = _coreTitleBar.Height;
                
                Window.Current.SetTitleBar(BackgroundElement);
            }
            else
            {
                //TitleBarControl.Visibility = Visibility.Collapsed;
                Window.Current.SetTitleBar(null);
            }
        }

    }
}
