using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using SQLite;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using SQLiteNetExtensions.Extensions;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensions;
using System.Threading.Tasks;
using Windows.Storage;
using System.Linq;
//using Microsoft.Live;
using Windows.UI.Xaml.Controls;
using Windows.UI.Popups;
using System.IO;
using mySpotterLibrary.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;



namespace mySpotterLibrary.Helpers
{
    //This class performs all database CRUD operations 
    public class DatabaseHelper
    {
        
        SQLiteConnection dbConn;
        public static string DB_PATH;// Path.Combine(ApplicationData.Current.LocalFolder.Path, "SpotterManager.sqlite");
        public string userFolderPath = "me/skydrive/files?filter=folders";
        
       // Boolean UserSignedIn { get { return App.UserSignedIn; } set { if (value != App.UserSignedIn) UserSignedIn = App.UserSignedIn; } }
     //   public ApplicationDataContainer roamingSettings = ApplicationData.Current.RoamingSettings;

      //  public string DatabasePath = roamingSettings.Values["OneDriveFolderPath"];
        
        public static string DB_NAME = "SpotterManager.sqlite";
        public static SQLitePlatformWinRT _SQLitePlatformWinRT;

        public DatabaseHelper(SQLite.Net.Platform.WinRT.SQLitePlatformWinRT platform, string path)
        {
            _SQLitePlatformWinRT = platform;
            DB_PATH = path;
        }


        /*   public async Task<bool> CreateOneDriveFolder()
           {
               try
               {



                   var folderData = new Dictionary<string, object>();
                   folderData.Add("name", "My Spotter");
                  // folderData.Add("description", "All of your Spotter Data and Stats");
                   LiveConnectClient liveClient = new LiveConnectClient(App.Session);
                   LiveOperationResult operationResult =
                     await liveClient.PostAsync("me/skydrive", folderData);
                   dynamic result = operationResult.Result;

                   roamingSettings.Values["OneDriveFolderPath"] = result.id;

                   return true;
               }
               catch (LiveConnectException exception)
               {
                   return false;
               }
           }

           public async Task<bool>  CheckIfCloudFileExists()
           {
               if (App.Session == null)
               {
                   await App.InitLogin();
               }

                   await GetFolderId();


               try
               {
               string path = (string)roamingSettings.Values["OneDriveFolderPath"];


                   LiveConnectClient client = new LiveConnectClient(App.Session);
                   LiveOperationResult liveOpResult =  await client.GetAsync(path + "/files");

                   dynamic dynResult = liveOpResult.Result;



                   var list = new List<dynamic>(dynResult.data);

                   if (!list.Any(x => x.name == DB_NAME))
                   {
                       await SyncDataBases();
                   }
                   foreach(dynamic d in dynResult.data){
                       if (d.name == DB_NAME)
                       {
                           roamingSettings.Values["OneDriveDataBasePath"] = d.id;
                       }
                   }

                   return true;
               }
               catch
               {
                   return false;
               }

           }

           public async Task<bool> DownloadCloudDatabaseFile(LiveConnectClient client, ProgressBar progressBar, System.Threading.CancellationTokenSource ctsDownload)
           {
               try
               {
               progressBar.Value = 0;
               progressBar.Visibility = Windows.UI.Xaml.Visibility.Visible;
               var progressHandler = new Progress<LiveOperationProgress>( 
               (progress) => 
               { 
                   progressBar.Value = progress.ProgressPercentage;



               });


                string s = (string)roamingSettings.Values["OneDriveDataBasePath"] + "/content";
               StorageFile File = await StorageFile.GetFileFromPathAsync(App.DB_PATH);
               ctsDownload = new System.Threading.CancellationTokenSource();
               await client.BackgroundDownloadAsync(  (string)roamingSettings.Values["OneDriveDataBasePath"] + "/content", File, ctsDownload.Token, progressHandler);

              // Task.WaitAll();

               return true;
               }
               catch
               {
                   return false;
               }
            //  HubPage.UpdateSyncedStatus();

           }



           public async Task GetFolderId()
           {
               try 
               {
                   LiveConnectClient client = new LiveConnectClient(App.Session);
                   LiveOperationResult liveOpResult =
                                                   await client.GetAsync("me/skydrive/files?filter=folders");
                   dynamic result = liveOpResult.Result;

                   foreach (dynamic d in result.data)
                   {
                       if (d.name == "My Spotter")
                       {
                           roamingSettings.Values["OneDriveFolderPath"] = d.id;
                       }
                   }
               }
               catch
               {

               }
           }


                public async Task SyncDataBases()
           {
               var file = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(DB_NAME);

               try
               {
                   LiveConnectClient liveClient = new LiveConnectClient(App.Session);
                   LiveUploadOperation op = await liveClient.CreateBackgroundUploadAsync(
                     (string)roamingSettings.Values["OneDriveFolderPath"], DB_NAME, file, OverwriteOption.Overwrite);
                   var result = await op.StartAsync();
                   // Handle result

               }
               catch (LiveConnectException exception)
               {
                   // Handle errors
               }
           }





            END OF COMMENT HERE!!!!!!!!!!!!!
           */
        //Create Tabble 
        public async Task onCreate()
        {



            //    {
            if (!IsDatabasePopulated())
            {
            
                    using (dbConn = new SQLiteConnection(_SQLitePlatformWinRT, DB_PATH))
                    {
                        
                        dbConn.CreateTable<UserGunSetup>();
                        dbConn.CreateTable<History>();
                        dbConn.CreateTable<Gun>();
                        dbConn.CreateTable<GunDataGroup>();
                        dbConn.CreateTable<Scope>();
                        dbConn.CreateTable<Ammo>();
                        dbConn.CreateTable<GunAmmoRelationship>();
                        dbConn.CreateTable<GunScopeRelationship>();
                        dbConn.CreateTable<RaspberryPiInfo>();
                        
                    }
                await AddDefaultDataBase();
            }
                    

            //   }


            
                
            
           // DependencyService.Get

        }



        public async Task AddDefaultDataBase()
        {
            

            string ammoData = ReadFileFromPCL("mySpotterLibrary.DefaultData.AmmoData.json");

            AmmoList Ammo =  JsonConvert.DeserializeObject<AmmoList>(ammoData);

            string scopeData = ReadFileFromPCL("mySpotterLibrary.DefaultData.ScopeData.json");

            ScopeList Scopes = JsonConvert.DeserializeObject<ScopeList>(scopeData);

            string gunData = ReadFileFromPCL("mySpotterLibrary.DefaultData.GunData.json");

            JObject guns = JObject.Parse(gunData);
            

            GunCatalog Guns = JsonConvert.DeserializeObject<GunCatalog>(gunData);

            int f = 0;

            //Having problems here will need to probably update this. BackGroundTask possibly.
            using (var dbConn = new SQLiteConnection(_SQLitePlatformWinRT, DB_PATH))
            {


                /*
                //first try, needed to insert guns with children and everything worked
                //TODO check other inserts like history adn gunsetup

                                foreach (Ammo a in Ammo.Ammo)
                                {
                                    dbConn.InsertWithChildren(a);

                                }

                                foreach (Scope s in Scopes.Scopes)
                                {
                                    dbConn.InsertWithChildren(s);
                                }

                                ConvertJsonAmmoAndScopesInGuns(Guns,Ammo,Scopes);
                                foreach (Gun g in Guns.GunGroups.SelectMany(x => x.Guns).ToList<Gun>())
                                {
                                    dbConn.InsertWithChildren(g,recursive: true);
                                }

                            */
                foreach (Ammo a in Ammo.Ammo)
                {
                    dbConn.InsertOrReplaceWithChildren(a,recursive:true);

                }

                foreach (Scope s in Scopes.Scopes)
                {
                    dbConn.InsertOrReplaceWithChildren(s,recursive:true);
                }

                ConvertJsonAmmoAndScopesInGuns(Guns, Ammo, Scopes);
                foreach(GunDataGroup group in Guns.GunGroups)
                {
                    dbConn.InsertOrReplaceWithChildren(group, recursive: true);
                }

            }
            int t = 0;
            
        }

        /// <summary>
        ///  This is the code to read from the PCL it gets the assembly of which the class input is located and then creates a stream.
        /// </summary>
        /// <param name="filename">
        ///  EXAMPLE string filename = "mySpotterLibrary.DefaultData.AmmoData.json"
        /// </param>
        public string ReadFileFromPCL(string filepath)
        {
            var assembly = typeof(DatabaseHelper).GetTypeInfo().Assembly;

          //  foreach (var res in assembly.GetManifestResourceNames())
          //  {
          //      System.Diagnostics.Debug.WriteLine(res);
          //  }
            Stream stream = assembly.GetManifestResourceStream(filepath);
            string text = "";
            using (var reader = new System.IO.StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }
            //stream.Flush();
           // stream.Dispose();
            return text;
        }
        public async Task<bool> CheckFileExists(string filename)
        {
            try
            {
             //   var files = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFilesAsync();

                var store = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(filename);
                if (store != null)
                return true;

            }
            catch
            {
                return false;
            }


            return false;
                
          
        }

        // Retrieve the specific contact from the database. 
        public UserGunSetup ReadUserSetup(int contactid)
        {
            using (var dbConn = new SQLiteConnection(_SQLitePlatformWinRT,DB_PATH))
            {
               // var existingconact = dbConn.Query<UserGunSetup>("select * from UserGunSetup where ID =" + contactid).FirstOrDefault();

                var gunSetup = dbConn.GetWithChildren<UserGunSetup>(contactid);
                return gunSetup;
            }
        }

        public RaspberryPiInfo GetRaspberryPiInfo(int raspID)
        {
            using (var dbConn = new SQLiteConnection(_SQLitePlatformWinRT, DB_PATH))
            {
                // var existingconact = dbConn.Query<UserGunSetup>("select * from UserGunSetup where ID =" + contactid).FirstOrDefault();

                var raspberryPi = dbConn.GetWithChildren<RaspberryPiInfo>(raspID);
                return raspberryPi;
            }
        }

        

        public Gun GetGun(string name)
        {
            var a = GetGuns();
            var t = a.ToList<Gun>();

            Gun gun = t.FirstOrDefault(x => x.Name == name);
            return gun;
        }
        // Retrieve the all contact list from the database. 
        public ObservableCollection<UserGunSetup> ReadUserSetups()
        {
            using (var dbConn = new SQLiteConnection(_SQLitePlatformWinRT,DB_PATH))
            {
                // List<UserGunSetup> myCollection = dbConn.Table<UserGunSetup>().ToList<UserGunSetup>();

                //  ObservableCollection<UserGunSetup> ContactsList = new ObservableCollection<UserGunSetup>(myCollection);
                dbConn.Table<History>().Sum(x => x.Score);
               List<UserGunSetup> ass = dbConn.GetAllWithChildren<UserGunSetup>();

               ObservableCollection<UserGunSetup> assss = new ObservableCollection<UserGunSetup>(ass);
                return assss;
            }
        }

        public ObservableCollection<History> GetSpecificGunHistory(int gunid)
        {
            ObservableCollection<History> r = ReadUserHistory();
            

            try
            {
                var historyToSend = r.Where(x => x.Setup.UserGunSetupId == gunid).ToList();
            return new ObservableCollection<History>(historyToSend);
            }
            catch
            {
                return null;
            }
            
        }

        public ObservableCollection<History> ReadUserHistory()
        {
            using (var dbConn = new SQLiteConnection(_SQLitePlatformWinRT, DB_PATH))
            {              

                List<History> ass = dbConn.GetAllWithChildren<History>();

                ObservableCollection<History> assss = new ObservableCollection<History>(ass);
                return assss;
            }
        }

        public ObservableCollection<T> ReadAllGeneric<T>()  where T : class
        {
            using (var dbConn = new SQLiteConnection(_SQLitePlatformWinRT, DB_PATH))
            {   
            List<T> list = dbConn.GetAllWithChildren<T>(recursive:true);
            ObservableCollection<T> collection = new ObservableCollection<T>(list);
            return collection;
            }

        }

        public T ReadSingleGeneric<T>( int primaryId) where T : class
        {

            using (var dbConn = new SQLiteConnection(_SQLitePlatformWinRT, DB_PATH))
            {

                T itemToget = dbConn.GetWithChildren<T>(primaryId);

                return itemToget;
                    
            }
        }

        

        public ObservableCollection<Scope> GetScopes()
        {
            using (var dbConn = new SQLiteConnection(_SQLitePlatformWinRT, DB_PATH))
            {
                List<Scope> myCollection = dbConn.GetAllWithChildren<Scope>();
                ObservableCollection<Scope> ContactsList = new ObservableCollection<Scope>(myCollection);
                return ContactsList;
            }
        }

        public ObservableCollection<Gun> GetGuns()
        {
            using (var dbConn = new SQLiteConnection(_SQLitePlatformWinRT, DB_PATH))
            {
                List<Gun> myCollection = dbConn.Table<Gun>().ToList<Gun>();
                ObservableCollection<Gun> ContactsList = new ObservableCollection<Gun>(myCollection);
                List<GunAmmoRelationship> s = dbConn.Table<GunAmmoRelationship>().ToList<GunAmmoRelationship>();
                var b = dbConn.Query<Gun>("select * from Gun");
               List<Ammo> a = dbConn.Table<Ammo>().ToList<Ammo>();

               var bb = dbConn.Table<GunScopeRelationship>().ToList<GunScopeRelationship>();

               int r = bb.Count;
               GunScopeRelationship d = dbConn.FindWithChildren<GunScopeRelationship>(0);
                return ContactsList;
            }
        }

        public ObservableCollection<Ammo> GetAmmo()
        {
            using (var dbConn = new SQLiteConnection(_SQLitePlatformWinRT, DB_PATH))
            {
                List<Ammo> myCollection = dbConn.Table<Ammo>().ToList<Ammo>();
                ObservableCollection<Ammo> ContactsList = new ObservableCollection<Ammo>(myCollection);
              //  List<GunAmmoRelationship> s = dbConn.Table<GunAmmoRelationship>().ToList<GunAmmoRelationship>();
             //   var b = dbConn.Query<Gun>("select * from Gun");
            //    List<Ammo> a = dbConn.Table<Ammo>().ToList<Ammo>();

             //   var bb = dbConn.Table<GunScopeRelationship>().ToList<GunScopeRelationship>();

             //   int r = bb.Count;

                return ContactsList;
            }
        }

        //Update existing conatct 
        public void UpdateUserSetup(UserGunSetup newSetup)
        {
            using (var dbConn = new SQLiteConnection(_SQLitePlatformWinRT,DB_PATH))
            {
               // var existingUserSetup = dbConn.Query<UserGunSetup>("select * from UserGunSetup where ID =" + newSetup.ID).FirstOrDefault();

                var existingUserSetup = dbConn.GetWithChildren<UserGunSetup>(newSetup.UserGunSetupId);
                if (existingUserSetup != null)
                {
                    existingUserSetup.Gun = newSetup.Gun;
                    existingUserSetup.SetupName = newSetup.SetupName;
                    existingUserSetup.TimesUsed = newSetup.TimesUsed;
                    existingUserSetup.Ammo = newSetup.Ammo;
                    existingUserSetup.Scope = newSetup.Scope;
                    existingUserSetup.Accuracy = newSetup.Accuracy;
                    existingUserSetup.LastUsed = DateTime.Now;
                    existingUserSetup.DateCreated = newSetup.DateCreated;
                    existingUserSetup.BulletCount = newSetup.BulletCount;
                    existingUserSetup.AvgSpread = newSetup.AvgSpread;
                    
                    dbConn.RunInTransaction(() =>
                    {
                        dbConn.Update(existingUserSetup);
                    });
                }
            }
        }
        // Insert the new contact in the Contacts table. 
        public void InsertUserSetup(UserGunSetup newSetup)
        {
            using (var dbConn = new SQLiteConnection(_SQLitePlatformWinRT,DB_PATH))
            {
                dbConn.RunInTransaction(() =>
                    {
                        dbConn.InsertOrReplaceWithChildren(newSetup,recursive:true);
                        
                    });
            }
        }

        public void InsertRaspberryPiInfo(RaspberryPiInfo pi)
        {
            using (var dbConn = new SQLiteConnection(_SQLitePlatformWinRT, DB_PATH))
            {
                dbConn.RunInTransaction(() =>
                {
                    dbConn.InsertWithChildren(pi);
                });
            }
        }

        public void UpdateRaspberryPiInfo(RaspberryPiInfo pi)
        {
            using (var dbConn = new SQLiteConnection(_SQLitePlatformWinRT, DB_PATH))
            {
                dbConn.RunInTransaction(() =>
                {
                    dbConn.UpdateWithChildren(pi);
                });
            }
        }

        public void InsertHistory(History newSession)
        {
            using (var dbConn = new SQLiteConnection(_SQLitePlatformWinRT, DB_PATH))
            {
                dbConn.RunInTransaction(() =>
                {
                    dbConn.InsertWithChildren(newSession);
                });
            }
        }

        public void InsertGun(Gun gun)
        {
            using (var dbConn = new SQLiteConnection(_SQLitePlatformWinRT, DB_PATH))
            {
                
                dbConn.RunInTransaction(() =>
                {
                    dbConn.InsertWithChildren(gun);
                    
                });
            }
        }

        public void InsertGunGroup(GunDataGroup gun)
        {
            using (var dbConn = new SQLiteConnection(_SQLitePlatformWinRT, DB_PATH))
            {

                dbConn.RunInTransaction(() =>
                {
                    dbConn.InsertWithChildren(gun);

                });
            }
        }

        public void InsertScope(Scope scope)
        {
            using (var dbConn = new SQLiteConnection(_SQLitePlatformWinRT, DB_PATH))
            {
                dbConn.RunInTransaction(() =>
                {
                    dbConn.InsertWithChildren(scope);
                });
            }
        }

        public void InsertAmmo(Ammo ammo)
        {
            using (var dbConn = new SQLiteConnection(_SQLitePlatformWinRT, DB_PATH))
            {
                dbConn.RunInTransaction(() =>
                {
                    dbConn.InsertWithChildren(ammo);
                });
            }
        }

        //Delete specific contact 
        public void DeleteContact(int Id)
        {
            using (var dbConn = new SQLiteConnection(_SQLitePlatformWinRT, DB_PATH))
            {
                var existingSetup = dbConn.Query<UserGunSetup>("select * from UserGunSetup where ID =" + Id).FirstOrDefault();
                if (existingSetup != null)
                {
                    dbConn.RunInTransaction(() =>
                    {
                        dbConn.Delete(existingSetup);
                    });
                }
            }
        }
        //Delete all contactlist or delete Contacts table 
        public void DeleteAllUserGunSetups()
        {
            using (var dbConn = new SQLiteConnection(_SQLitePlatformWinRT, DB_PATH))
            {
                //dbConn.RunInTransaction(() => 
                //   { 
                dbConn.DropTable<UserGunSetup>();
                dbConn.CreateTable<UserGunSetup>();
                dbConn.Dispose();
                dbConn.Close();
                //}); 
            }
        }

   /*     public async Task InsertDefaultScopes()
        {

        //    ScopeList scopes = new ScopeList();
        //   await scopes.GetScopeDataAsync();
        //   AmmoList ammos = new AmmoList();
        //   await ammos.GetAmmoDataAsync();
        //   GunDataSource data = new GunDataSource();
        //  await data.GetGunDataAsync();
        
            
                   foreach (Scope s in scopes.Scopes)
                       InsertScope(s);


                   foreach (Ammo a in ammos.Ammo)                       
                       InsertAmmo(a);
                

                   foreach (Gun g in data.GunGroups.SelectMany(x => x.Guns))
                  {
                      InsertGun(g);
                   }

                   foreach (GunDataGroup gunGroup in data.GunGroups)
                   {
                       InsertGunGroup(gunGroup);
                   }

         
            
        }  */




        public void ConvertJsonAmmoAndScopesInGuns(GunCatalog cata, AmmoList ammo, ScopeList scopes)
        {

            foreach(Gun gun in cata.GunGroups.SelectMany(g => g.Guns))
            {
                for(int x = 0; x < gun.PossibleAmmo.Count; x++)
                {
                    gun.PossibleAmmo[x] = ammo.Ammo.First((item) => item.Name.Equals(gun.PossibleAmmo[x].Name));
                }

                for(int y = 0; y < gun.PossibleScopes.Count; y++)
                {
                    gun.PossibleScopes[y] = scopes.Scopes.First((item) => item.Name.Equals(gun.PossibleScopes[y].Name));
                }
            }


        }
        public bool IsDatabasePopulated()
        {

            try
            {
                ObservableCollection<Gun> t = GetGuns();
                var r = t.ToList();

                if (r.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}