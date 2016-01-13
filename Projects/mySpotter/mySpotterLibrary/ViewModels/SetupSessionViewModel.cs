using mySpotterLibrary.Helpers;
using mySpotterLibrary.Models;
using mySpotterLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mySpotterLibrary.ViewModels
{
    public class SetupSessionViewModel  : BindableBase
    {

        private DatabaseHelper dbHelper;

        private string userName;

        public string UserName
        {
            get { return userName; }
            set { SetProperty(ref userName, value); }
        }

     //   public UserGunSetupViewModel UserSetup { get; set; }

        /// <summary>
        /// ViewModel right before one enters a session. Set setupId to 0 for new setup.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="setupId"></param>
        public SetupSessionViewModel(DatabaseHelper helper, int setupId)
        {
            dbHelper = helper;
            
            //    UserSetup = new UserGunSetupViewModel(setupId, helper);
            
            
        }

        public SetupSessionViewModel()
        {

        }

        private RaspberryPiInfo piInfo;
        public RaspberryPiInfo PiInfo
        {
            get { return piInfo; }
            set { SetProperty(ref piInfo, value); }
        }
        
    }
}
