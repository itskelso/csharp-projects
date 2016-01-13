using mySpotterLibrary.Helpers;
using mySpotterLibrary.Models;
using mySpotterLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Networking.Sockets;
using Windows.Networking.Connectivity;
using System.Net.Sockets;
using System.Net;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Core;
using Windows.UI.Xaml;
using System.Runtime.InteropServices.WindowsRuntime;
using GalaSoft.MvvmLight;
using System.Text.RegularExpressions;
using Windows.ApplicationModel.Core;

namespace mySpotterLibrary.ViewModels
{
    public class SessionViewModel : ViewModelBase
    {
        StreamSocket socket;

        

        public string receivedMessage { get; set; }
        

        public SessionViewModel()
        {
            Log = new ObservableCollection<LogMessage>();
            PiInfo = new RaspberryPiInfo() { IpAddress = "192.168.1.2", Port = "5000" };
           // ImageSource = new BitmapImage();
            ImgHelper = new ImageHelper();
            DbHelper = new DatabaseHelper(DatabaseHelper._SQLitePlatformWinRT, DatabaseHelper.DB_PATH);
            try
            {
                ObservableCollection<RaspberryPiInfo> pis = DbHelper.ReadAllGeneric<RaspberryPiInfo>();
                PiInfo = pis.First(x => x.isDefault);
            }
            catch
            {
                Log.Add(new LogMessage("No Default Pi is in the database please add one"));
            }
            
          // Image     ImageHelper.ReadImageFromPi
        }



        private WriteableBitmap imageSource;

        public WriteableBitmap MyImageSource
        {
            get { return imageSource; }
            set { Set(() => MyImageSource, ref imageSource, value); }
        }
        private ObservableCollection<LogMessage> log;

        public ObservableCollection<LogMessage> Log
        {
            get { return log; }
            set { Set(() => Log, ref log, value); }
        }

        private RaspberryPiInfo piInfo;

        public RaspberryPiInfo PiInfo
        {
            get { return piInfo; }
            set { Set(() => PiInfo, ref piInfo, value); }
        }

        private ImageHelper imgHelper;

        public ImageHelper ImgHelper
        {
            get { return imgHelper; }
            set { Set(() => ImgHelper, ref imgHelper, value); }
        }

        private DatabaseHelper dbHelper;

        public DatabaseHelper DbHelper
        {
            get { return dbHelper; }
            set { Set(() => DbHelper,ref dbHelper, value); }
        }

        private UserGunSetupViewModel userSetup;
        public UserGunSetupViewModel UserSetup
        {
            get { return userSetup; }
            set { Set(() => UserSetup, ref userSetup, value); }
        }

        private byte[] targetImageBytes;

        public byte[] TargetImageBytes
        {
            get { return targetImageBytes; }
            set { Set(()=> TargetImageBytes,ref targetImageBytes, value); }
        }

        private RelayCommand connectToPiCommand;

        public RelayCommand ConnectToPiCommand
        {
            get
            {
                if (connectToPiCommand == null)
                {
                    connectToPiCommand = new RelayCommand(async
                        () =>
                        {
                            await Connect();
                        },
                        () =>
                        {
                            return (piInfo != null);
                        });

                }
                return connectToPiCommand;
            }
        }

        private RelayCommand sendRefPhotoCommand;

        public RelayCommand SendRefPhotoCommand
        {
            get
            {
                if(sendRefPhotoCommand == null)
                {
                    sendRefPhotoCommand = new RelayCommand( async
                        () => 
                        {
                           await Send("Send Photo");
                           //receivedMessage = await Receive();
                           if(MyImageSource != null)
                            {
                              //  ImgHelper.RefPhoto = ImageSource;
                            }
                            
                        },
                        () =>
                        {
                            return (socket != null);
                        }
                        );
                }
                return sendRefPhotoCommand;
            }
        }

        public async Task CalculateShotPoint()
        {
         //  await ImgHelper.ToGrayScale(ImgHelper.refImageBytes);

          //  ImageSource = await ImgHelper.RGBArrayToBitmap(ImgHelper.refImageBytes);
            
        }

        private async Task DecodeImage()
        {
            receivedMessage.Replace("\r\n", String.Empty);
            receivedMessage.Replace("\n", String.Empty);
            Regex.Replace(receivedMessage, "[^a-zA-Z0-9]+", "", RegexOptions.None);


            byte[] buffer = Convert.FromBase64String(receivedMessage);
        //    ImageSource = await ImgSrcFromBytes(buffer);
            

        }

        public async Task<BitmapImage> ImgSrcFromBytes(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0) return null;
            


            using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
            {
                await stream.WriteAsync(bytes.AsBuffer(0, bytes.Length));
                stream.Seek(0);

                BitmapImage image = new BitmapImage();

                await image.SetSourceAsync(stream);
               // this.CurrentImage.Source = image;
                return image;
            }
        }

        public async Task Connect()
        {
            socket = new StreamSocket();

           // listenerSocket = new StreamSocketListener();
            
            

            try
            {
                await socket.ConnectAsync(new Windows.Networking.HostName(PiInfo.IpAddress), PiInfo.Port);
                Log.Add(new LogMessage("Successfully Connected to the Rasperry Pi at " + PiInfo.IpAddress));
                SendRefPhotoCommand.RaiseCanExecuteChanged();
            }
            catch
            {
                Log.Add(new LogMessage("There was an error connecting to the Pi at " + PiInfo.IpAddress));


                Log.Add(new LogMessage("Loading Photo to be manipulated"));

                

            }
           

            
            
        }

        private async Task Send(string stringToSend)
        {
            using (DataWriter chatWriter = new DataWriter(socket.OutputStream))
            {
               // string ToSend = "Send Photo";
                

                // chatWriter.WriteUInt32((uint)ToSend.Length);

                chatWriter.WriteString(stringToSend);

                try
                {
                    await chatWriter.StoreAsync();
                }

                catch (Exception ex)
                {
                    //   MainPage.Current.NotifyUser("Error: " + ex.HResult.ToString() + " - " + ex.Message,
                    //        NotifyType.StatusMessage);

                }
                await chatWriter.FlushAsync();
                chatWriter.DetachStream();

            }
            receivedMessage = null;
            await Task.Delay(2000);
            receivedMessage = await Receive();

            if (receivedMessage != null)
            {

                //   await  Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                //     async () => 
                //       {
                //RequestAggregateBatteryReport();
                MyImageSource = await ImgHelper.ReadImageFromPi(receivedMessage);
                RaisePropertyChanged("MyImageSource");
       //           });
                
               await CalculateShotPoint();
            }
        }

        private async Task<string> Receive()
        {
            if (socket == null) return null;
            

            using (DataReader reader = new DataReader(socket.InputStream))
            {
                

              //  reader.InputStreamOptions = InputStreamOptions.Partial;
                reader.UnicodeEncoding = Windows.Storage.Streams.UnicodeEncoding.Utf8;
                reader.ByteOrder = Windows.Storage.Streams.ByteOrder.LittleEndian;

                
                   

                    try
                    {
                    while (true)
                    {
                        StringBuilder ImageStrBuilder = new StringBuilder();
                        // Set the DataReader to only wait for available data (so that we don't have to know the data size)
                        // reader.InputStreamOptions = Windows.Storage.Streams.InputStreamOptions.Partial;
                        // The encoding and byte order need to match the settings of the writer we previously used.


                        // Send the contents of the writer to the backing stream. 
                        // Get the size of the buffer that has not been read.

                        // await  Task.Delay(50);
                        //  if (socket == null) return null;


                        uint sizeFieldCount = await reader.LoadAsync(sizeof(uint));
                        if (sizeFieldCount != sizeof(uint))
                        {
                            // The underlying socket was closed before we were able to read the whole data.
                            return null;
                        }



                        uint stringLength = reader.ReadUInt32();
                        uint actualStringLength = await reader.LoadAsync(stringLength);
                        if (stringLength != actualStringLength)
                        {
                            // The underlying socket was closed before we were able to read the whole data.
                            return null;
                        }

                        // Keep reading until we consume the complete stream.
                        // while (reader.UnconsumedBufferLength > 0)
                        //     {
                        //        ImageStrBuilder.Append(reader.ReadString(reader.UnconsumedBufferLength));
                        //        await reader.LoadAsync(2048);

                        //    }



                        //    ImageString = ImageStrBuilder.ToString();


                        //    else if (ImageStrBuilder.Length < 100 && ImageStrBuilder.Length > 0)
                        //    {
                        //         Calculate(ImageString);
                        //     }





                        // ClientAddLine("Received(" + DateTime.Now.ToString("h:mm:ss") + "): " + ImageStrBuilder.ToString());
                        //await Task.Factory.StartNew(ReloadPicture);
                        // Send(2);
                        ImageStrBuilder.Append(reader.ReadString(actualStringLength));

                        // reader.DetachBuffer();
                         reader.DetachStream();
                        // reader.Dispose();

                        return ImageStrBuilder.ToString();
                        //  return "error";


                    }


                    }
                    catch (Exception e)
                    {
                    //   ClientWaitForMessage();
                    //received = false;
                    return "error";
                    }




                
            }


        }

    }
}
