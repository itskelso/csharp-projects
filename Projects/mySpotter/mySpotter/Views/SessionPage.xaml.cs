using mySpotter.Common;
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
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using mySpotterLibrary.ViewModels;
using mySpotterLibrary.Helpers;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Graphics.Imaging;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace mySpotter.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SessionPage : Page
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

        public SessionPage()
        {
            this.InitializeComponent();
            
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
        }

        public async void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {

            
            
             
           // this.SessionViewModel.UserSetup.Setup = t;
        }

        private void LoadDataAsync(int gunID)
        {
            this.model.DataContext = new mySpotterLibrary.ViewModels.UserGunSetupViewModel(gunID, NavigationRootPage.dbHelper);

            


        }

        

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);

           // this.DataContext = new SessionViewModel();
            if(e.Parameter != null && (e.Parameter is UserGunSetup))
            {
                var t = e.Parameter as UserGunSetup;
                LoadDataAsync(t.UserGunSetupId);
            }
                
            
            
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/P1250026.JPG"));
            var b64 = await ConvertStorageFileToBase64String(file);

            StorageFolder storageFolder = KnownFolders.PicturesLibrary;
            StorageFile sampleFile = await storageFolder.CreateFileAsync("sample264.txt", CreationCollisionOption.ReplaceExisting);

            await FileIO.WriteTextAsync(sampleFile, b64);

           // byte[] bytes = Convert.FromBase64String(b64);
         //   await Task.Run(async () => { 
         //       using (FileStream sourceStream = new FileStream(@"C:\Users\Kellan\Desktop\temp.txt",
          //              FileMode.Append, FileAccess.Write, FileShare.None,
          //              bufferSize: 4096, useAsync: true))
          //              {
         //                   await sourceStream.WriteAsync(bytes, 0, bytes.Length);
          //              };
          //  });

        }

        
        private async Task<string> ConvertStorageFileToBase64String(StorageFile File)
        {
            var stream = await File.OpenReadAsync();

            using (var dataReader = new DataReader(stream))
            {
                var bytes = new byte[stream.Size];
                await dataReader.LoadAsync((uint)stream.Size);
                dataReader.ReadBytes(bytes);

                return Convert.ToBase64String(bytes);
            }
        }

        private async void TestButton1_Click(object sender, RoutedEventArgs e)
        {
            StorageFolder storageFolder = KnownFolders.PicturesLibrary;
           // StorageFile sampleFile = await storageFolder.GetFileAsync("sample264.txt");

            StorageFile sampleFile = await storageFolder.GetFileAsync("KellandKay.jpg");


            using (var stream = await sampleFile.OpenReadAsync())
            {
                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);

                var t = await decoder.GetPixelDataAsync();

                byte[] pixels = t.DetachPixelData();

                

                await this.SessionViewModel.ImgHelper.ToGrayScale(pixels);

                this.SessionViewModel.MyImageSource = new WriteableBitmap((int)decoder.PixelWidth, (int)decoder.PixelHeight);

                //   await this.SessionViewModel.ImgHelper.BytesToWriteableBitmap(pixels, this.SessionViewModel.MyImageSource);
                //   pixels = await this.SessionViewModel.ImgHelper.ApplyTwoPassFilter(mySpotterLibrary.Helpers.Matrix.Gaussian1D3X3, mySpotterLibrary.Helpers.Matrix.Gaussian1D3X3, 1, (int)decoder.PixelWidth, pixels);
                //  pixels = await this.SessionViewModel.ImgHelper.Apply3X3Filter(mySpotterLibrary.Helpers.Matrix.GaussianBlur3x3, 1.0, (int)decoder.PixelWidth, pixels);
                //     int[] pixels1 =   await this.SessionViewModel.ImgHelper.Apply3X3Filter(mySpotterLibrary.Helpers.Matrix.SobelX3x3, 1.0, (int)decoder.PixelWidth, pixels);
                //     int[] pixels2 =   await this.SessionViewModel.ImgHelper.Apply3X3Filter(mySpotterLibrary.Helpers.Matrix.SobelY3x3, 1.0, (int)decoder.PixelWidth, pixels);

                //  byte[] newpixels = await this.SessionViewModel.ImgHelper.ApplyTwoPassFilter(mySpotterLibrary.Helpers.Matrix.Gaussian1D3X3, mySpotterLibrary.Helpers.Matrix.Gaussian1D3X3, 1, (int)decoder.PixelWidth, pixels);

                //      int[] newpixels1 = await this.SessionViewModel.ImgHelper.ApplyTwoPassFilter(mySpotterLibrary.Helpers.Matrix.Sobel1DX, mySpotterLibrary.Helpers.Matrix.Sobel1DY, 1, (int)decoder.PixelWidth, pixels);
                //        int[] newpixels2 = await this.SessionViewModel.ImgHelper.ApplyTwoPassFilter(mySpotterLibrary.Helpers.Matrix.Sobel1DY, mySpotterLibrary.Helpers.Matrix.Sobel1DX, 1, (int)decoder.PixelWidth, pixels);

                //    byte[] newpixels = await this.SessionViewModel.ImgHelper.ApplyTwoPassFilterInReverse(mySpotterLibrary.Helpers.Matrix.Sobel1DY, mySpotterLibrary.Helpers.Matrix.Sobel1DX, 1, (int)decoder.PixelWidth, pixels);

                byte[] newpixels = await this.SessionViewModel.ImgHelper.ApplyCanny(pixels, (int)decoder.PixelWidth);

                await this.SessionViewModel.ImgHelper.BytesToWriteableBitmap(newpixels, this.SessionViewModel.MyImageSource);
            }


                
            //   string b64 = await FileIO.ReadTextAsync(sampleFile);
            //    this.SessionViewModel.MyImageSource =  await  this.SessionViewModel.ImgHelper.ReadImageFromPi(b64);

            

        }

        private async void TestButton2_Click(object sender, RoutedEventArgs e)
        {
            StorageFolder storageFolder = KnownFolders.PicturesLibrary;
            StorageFile sampleFile = await storageFolder.GetFileAsync("sample64.txt");

            StorageFile sampleFileTwo = await storageFolder.GetFileAsync("sample264.txt");

            string b64 = await FileIO.ReadTextAsync(sampleFile);
            string b264 = await FileIO.ReadTextAsync(sampleFileTwo);

            

            WriteableBitmap bmp = await this.SessionViewModel.ImgHelper.ReadImageFromPi(b64);
            WriteableBitmap bmp2 = await this.SessionViewModel.ImgHelper.ReadImageFromPi(b264);

            byte[] subtractedImage = await this.SessionViewModel.ImgHelper.SubtractImages((bmp.PixelBuffer.ToArray()), (bmp2.PixelBuffer.ToArray()));

            if(this.SessionViewModel.MyImageSource == null)
            {
                this.SessionViewModel.MyImageSource = new WriteableBitmap(bmp.PixelWidth, bmp.PixelHeight);
            }

            //      await Apply3X3Filter(Matrix.GaussianBlur3x3, 1, bitmap.PixelWidth, refImageBytes);
              await this.SessionViewModel.ImgHelper.ApplyTwoPassFilter(mySpotterLibrary.Helpers.Matrix.Gaussian1D3X3, mySpotterLibrary.Helpers.Matrix.Gaussian1D3X3, 1, bmp.PixelWidth, subtractedImage);

            bmp = null;
            bmp2 = null;

            this.SessionViewModel.MyImageSource =  await this.SessionViewModel.ImgHelper.BytesToWriteableBitmap(subtractedImage, this.SessionViewModel.MyImageSource);
        }
    }
}
