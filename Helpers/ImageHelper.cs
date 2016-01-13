using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;


//Copyright 2015, Kellan Sells
/*

    My Image Processing Library

    @12/8/2015
    Currently it can convolute both 2D and 1D matrices
    2D is slower computational, but requires less memory
    1D is faster, but requires more memory as I had to add a second buffer
    tests show with a 640 x 400 image that 1D only takes about 55MB top for the whole app and conv is sub 10ms
    My original image was a 2560 x 2560 and with 2D it took about 800ms to complete everything
    havent tested 1D in same scenario but I will
    
    I may not need the variables that follow, but i havent been able to fully test the system. 
    TODO: Hough Transform to find circles.
          
*/
 

namespace mySpotterLibrary.Helpers
{
    public class ImageHelper
    {
        public BitmapImage RefPhoto { get; set; }

        public int RefPhotoHeight { get; set; }

        public WriteableBitmap bmp { get; set; }

        public int RefPhotoWidth { get; set; }

        public byte[] refImageBytes { get; set; }


        public ImageHelper()
        {
            RefPhoto = new BitmapImage();
           // bmp = new WriteableBitmap();

        }

        
        
        public async Task<WriteableBitmap> ReadImageFromPi(string b64string)
        {
            b64string.Replace("\r\n", String.Empty);
            b64string.Replace("\n", String.Empty);
            Regex.Replace(b64string, "[^a-zA-Z0-9]+", "", RegexOptions.None);


            byte[] buffer = Convert.FromBase64String(b64string);

            if (buffer == null || buffer.Length == 0) return null;
        //  var f =  Convert.GetTypeCode(buffer);


            using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
            {
                  await stream.WriteAsync(buffer.AsBuffer(0, buffer.Length));
                  stream.Seek(0);

                // BitmapImage image = new BitmapImage();

                //   await image.SetSourceAsync(stream);
                // this.CurrentImage.Source = image;

                  BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);

                   var pixels = await decoder.GetPixelDataAsync(
                  BitmapPixelFormat.Bgra8,
                  BitmapAlphaMode.Straight,
                   new BitmapTransform(),
                   ExifOrientationMode.IgnoreExifOrientation,
                    ColorManagementMode.DoNotColorManage);

             //   var sbmp = await decoder.GetSoftwareBitmapAsync();
             //   sbmp.LockBuffer(BitmapBufferAccessMode.ReadWrite);
            //    SoftwareBitmap.Convert(sbmp, BitmapPixelFormat.Gray16);
                

              var  bitmap = new WriteableBitmap((int)decoder.PixelWidth, (int)decoder.PixelHeight);

              //  sbmp.CopyToBuffer(bmp.PixelBuffer);

                
                
                refImageBytes = pixels.DetachPixelData();
                
                
                await ToGrayScale(refImageBytes);
                //  await stream.WriteAsync(refImageBytes.AsBuffer(0, refImageBytes.Length));
                //   stream.Seek(0);
                //   await bmp.SetSourceAsync(stream);

             //      await Apply3X3Filter(Matrix.GaussianBlur3x3, 1, bitmap.PixelWidth, refImageBytes);
          //      await ApplyTwoPassFilter(Matrix.Gaussian1D3X3, Matrix.Gaussian1D3X3, 1, bitmap.PixelWidth, refImageBytes);
                   using (Stream streambmp = bitmap.PixelBuffer.AsStream())
                   {
                      await streambmp.WriteAsync(refImageBytes, 0, refImageBytes.Length);
                      await streambmp.FlushAsync();
                   }

                
                
                    return bitmap;
            }
        }


        

        public static async Task<byte[]> GetPixels(byte[] buffer)
        {
            using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
            {
                await stream.WriteAsync(buffer.AsBuffer(0,buffer.Length));
               // stream.Seek(0);

                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);

                var pixels = await decoder.GetPixelDataAsync(
        BitmapPixelFormat.Gray8,
        BitmapAlphaMode.Straight,
        new BitmapTransform(),
        ExifOrientationMode.IgnoreExifOrientation,
        ColorManagementMode.DoNotColorManage);

                
                
                return pixels.DetachPixelData();
            }
        }

        public async Task<WriteableBitmap> BytesToWriteableBitmap(byte[] bytes, WriteableBitmap bitmap)
        {
           // StorageFolder folder = KnownFolders.PicturesLibrary;
          //  StorageFile file = await folder.CreateFileAsync("TestPhoto.jpeg",CreationCollisionOption.ReplaceExisting);


            using (Stream streambmp = bitmap.PixelBuffer.AsStream())
            {
                await streambmp.WriteAsync(bytes, 0, bytes.Length);
                await streambmp.FlushAsync();
                return bitmap;
            }           
            

        }

        public async Task ToGrayScale(byte[] RGBAbytes)
        {


            for (int x = 0; x < RGBAbytes.Length; x += 4)
            {

                byte a = RGBAbytes[x + 3];

                if (a > 0)
                {
                    double ad = (double)a / 255.0; // 0..1 range alpha
                    double rd = (double)RGBAbytes[x + 2] / ad; // 0..255 range red, non-alpha-premultiplied
                    double gd = (double)RGBAbytes[x + 1] / ad; // 0..255 range green, non-alpha-premultiplied
                    double bd = (double)RGBAbytes[x + 0] / ad; // 0..255 range blue, non-alpha-premultiplied

                    // gain is the difference between current value and maximum (255), multiplied by the amount and alpha-premultiplied
                    double luminance = 0.2126 * rd + 0.7152 * gd + 0.0722 * bd;
                    double newR = luminance * ad;
                    double newG = newR;
                    double newB = newR;

                    RGBAbytes[x + 0] = (byte)newB;
                    RGBAbytes[x + 1] = (byte)newG;
                    RGBAbytes[x + 2] = (byte)newR;
                    RGBAbytes[x + 3] = 255;

                }
            }
        }

        public async Task WriteByteArrayToBitmap(byte[] buffer)
        {
            using (Stream streambmp = bmp.PixelBuffer.AsStream())
            {
                await streambmp.WriteAsync(buffer, 0, buffer.Length);
                await streambmp.FlushAsync();
            }

            

        }

        public async Task<byte[]> SubtractImages(byte[] refImage, byte[] newImage)
        {
            
            int f = Math.Min(refImage.Length, newImage.Length);

            while(f % 4 != 0)
            {
                f--;
            }

            for(int x = 0;x < f; x += 4)
            {
               // byte a = RGBAbytes[x + 3];

                
                     // 0..1 range alpha
                    double rd = (double)refImage[x + 2] - (double)newImage[x+2]; // 0..255 range red, non-alpha-premultiplied
                    double gd = (double)refImage[x + 1] - (double)newImage[x + 1]; // 0..255 range green, non-alpha-premultiplied
                    double bd = (double)refImage[x + 0] - (double)newImage[x + 0]; // 0..255 range blue, non-alpha-premultiplied

                    // gain is the difference between current value and maximum (255), multiplied by the amount and alpha-premultiplied
                    

                    refImage[x + 0] = (byte)Math.Abs(rd);
                    refImage[x + 1] = (byte)Math.Abs(gd);
                    refImage[x + 2] = (byte)Math.Abs(bd);

                
            }


            return refImage;
        }

        public async Task<int[]> Apply3X3Filter(double[,] filter, double factor, int imageWidth, byte[] pixels)
        {


            //  bitmap.CopyFromBuffer(bmp.PixelBuffer);



          //  byte[] pixels = refImageBytes;

            int[] buffer = new int[pixels.Length];

            int filterWidth = 3;
            int filterHeight = 3;

            int i = imageWidth * 4;

            System.Diagnostics.Stopwatch g = new System.Diagnostics.Stopwatch();
            g.Start();
            for (int x = 0; x < pixels.Length; x += 4)
            {
                double green = 0.0;

                //   int leftNeighbor = pixels[x - 4];
                //   int rightNeighbor = pixels[x + 4];

                //   int diagonalTopLeft = pixels[x - 4 - i];
                //   int diagonalBottomLeft = pixels[x - 4 + i];

                //   int topNeighbor = pixels[x - i];
                //   int bottomNeighbor = pixels[x + i];

                //   int diagonalTopRight = pixels[x + 4 - i];
                //   int diagonalBottomRight = pixels[x + 4 + i];

                

                for (int filterX = 0; filterX < 3; filterX++)                
                    for (int filterY = 0; filterY < 3; filterY++)
                    {
                        
                        int imageX = (x + ((filterY - 1)*4) + ((filterX -1) * i));
                        if(imageX < 0 || imageX >= pixels.Length)
                        {
                            imageX = x;
                        }
                      //  red += image[imageX][imageY].r * filter[filterX][filterY];
                        green += pixels[imageX] * filter[filterX,filterY];
                     //   blue += image[imageX][imageY].b * filter[filterX][filterY];
                    }

                //  buffer[x] = (byte)Math.Min(Math.Max((int)(factor * green), 0), 255);
                buffer[x] = (int)green;
                buffer[x + 1] = buffer[x];
                buffer[x + 2] = buffer[x];
                buffer[x+3] = pixels[x+3];
               // buffer[x++] = (byte)Math.Min(Math.Max((int)(factor * green), 0), 255);
            }
            g.Stop();


            return buffer;


        }

        public async Task<int[]> ApplyTwoPassFilter(double[] filterX,double[] filterY, double factor, int imageWidth, byte[] pixels)
        {

            int[] buffer = new int[pixels.Length];
            int[] buffer2 = new int[pixels.Length];

            int filterWidth = 3;
            int filterHeight = 3;

            int i = imageWidth * 4;
            int t = (filterX.Length / 2);

            for (int x = 0; x < pixels.Length; x += 4)
            {
               double green = 0.0;

                for (int j = -(filterX.Length/2); j <= filterX.Length/2; j++)
                {
                    int imageX = x + (j * 4);
                    if(imageX < 0 || imageX >= buffer.Length)
                    {
                        imageX = x;
                    }
                    green += pixels[imageX] * filterX[j + 1];

                }
                

                buffer[x] = (int)green;
                buffer[1 + x] = buffer[x];
                buffer[2 + x] = buffer[x];
                buffer[3 + x] = 255;// pixels[x + 3];
            }

            



                for (int y = 0; y < buffer.Length; y += 4)
                {
                    double green = 0.0;

                    for (int j = 0; j < filterY.Length; j++)
                    {
                        int imageX = y + ((j - filterY.Length / 2) * i);
                        if (imageX < 0 || imageX >= buffer.Length)
                        {
                            imageX = y;

                        }
                        green += buffer[imageX] * filterY[j];

                    }
                    // int t = i * (filterY.Length / 2);
                    buffer2[y] = (int)green; 
                    buffer2[y + 1] = buffer[y];
                    buffer2[y + 2] = buffer[y];
                    buffer2[y + 3] = 255; // pixels[y + 3];
                }

            

           


            return buffer2;
        }


        public async Task<byte[]> CombineGradients( int[] pixels, int[] pixels2)
        {
            byte[] buffer = new byte[pixels.Length];

            for(int x = 0; x < pixels.Length; x += 4)
            {
                double newByte = Math.Sqrt(pixels[x] * pixels[x] + pixels2[x] * pixels2[x]);
                buffer[x] = (byte)Math.Min(Math.Max((int)(newByte), 0), 255);
                buffer[x + 1] = buffer[x];
                buffer[x + 2] = buffer[x];
                buffer[x + 3] = 255;

            }

            return buffer;
        }

        public async Task<byte[]> CombineGradients(byte[] pixels, byte[] pixels2)
        {
            byte[] buffer = new byte[pixels.Length];

            for (int x = 0; x < pixels.Length; x += 4)
            {
                double newByte = Math.Sqrt(pixels[x] * pixels[x] + pixels2[x] * pixels2[x]);
                buffer[x] = (byte)Math.Min(Math.Max((int)(newByte), 0), 255);
                buffer[x + 1] = buffer[x];
                buffer[x + 2] = buffer[x];
                buffer[x + 3] = 255;

            }

            return buffer;
        }

        public void ApplyNonMaximumSupression(int minThreshold, byte[] pixels)
        {
            for (int x = 0; x < pixels.Length; x += 4)
            {
                if (pixels[x] > minThreshold)
                    pixels[x] = 255;
                else
                    pixels[x] = 0;

               // pixels[x] = (byte)Math.Min(Math.Max((int)(newByte), 0), 255);
                pixels[x + 1] = pixels[x];
                pixels[x + 2] = pixels[x];
                pixels[x + 3] = 255;

            }
        }




        public async Task<byte[]> ApplyTwoPassFilterInReverse(double[] filterX, double[] filterY, double factor, int imageWidth, byte[] pixels)
        {
            byte[] buffer = new byte[pixels.Length];

            int filterWidth = 3;
            int filterHeight = 3;

            int i = imageWidth * 4;
            int t = (filterX.Length / 2);


            for (int x = 0; x < pixels.Length; x += 4)
            {
                double green = 0.0;

                for (int j = -(filterX.Length / 2); j <= filterX.Length / 2; j++)
                {
                    int imageX = x + (j * 4);
                    if (imageX < 0 || imageX >= buffer.Length)
                    {
                        imageX = x;
                    }
                    green += pixels[imageX] * filterX[j + 1];

                }


                buffer[x] = (byte)Math.Min(Math.Max((int)(factor * green), 0), 255);
                buffer[1 + x] = buffer[x];
                buffer[2 + x] = buffer[x];
                buffer[3 + x] = 255;// pixels[x + 3];
            }


            for (int y = 0; y < pixels.Length - 4; y += 4)
            {
                double green = 0.0;

                for (int j = 0; j < filterY.Length; j++)
                {
                    int imageX = y + ((j - filterY.Length / 2) * i);
                    if (imageX < 0 || imageX >= pixels.Length)
                    {
                        imageX = y;

                    }
                    green += buffer[imageX] * filterY[j];

                }
                // int t = i * (filterY.Length / 2);
                buffer[y] = (byte)Math.Min(Math.Max((int)(factor * green), 0), 255);
                buffer[y + 1] = buffer[y];
                buffer[y + 2] = buffer[y];
                buffer[y + 3] = 255; // pixels[y + 3];
            }


            for (int x = 0; x < pixels.Length; x += 4)
            {
                double green = 0.0;

                for (int j = -(filterX.Length / 2); j < filterX.Length / 2; j++)
                {
                    int imageX = x + (j * 4);
                    if (imageX < 0 || imageX >= buffer.Length)
                    {
                        imageX = x;
                    }
                    green += pixels[imageX] * filterX[j + 1];

                }


                buffer[x] = (byte)Math.Min(Math.Max((int)(factor * green), 0), 255);
                buffer[1 + x] = buffer[x];
                buffer[2 + x] = buffer[x];
                buffer[3 + x] = 255;// pixels[x + 3];
            }

            return buffer;
        }

        public async Task<byte[]> ApplyCanny( byte[] pixels, int width)
        {

            int[] XGradient = await ApplyTwoPassFilter(Matrix.Sobel1DX, Matrix.Sobel1DY, 1, width, pixels);
            int[] YGradient = await ApplyTwoPassFilter(Matrix.Sobel1DY, Matrix.Sobel1DX, 1, width, pixels);

            //    byte[] newpixels = await this.SessionViewModel.ImgHelper.ApplyTwoPassFilterInReverse(mySpotterLibrary.Helpers.Matrix.Sobel1DY, mySpotterLibrary.Helpers.Matrix.Sobel1DX, 1, (int)decoder.PixelWidth, pixels);

            byte[] newpixels = await CombineGradients(XGradient, YGradient);

          //  ApplyThreshold(210, newpixels);




            return newpixels;
        }



        }//end class    
}// end namespace
