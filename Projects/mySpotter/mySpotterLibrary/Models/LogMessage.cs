using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mySpotterLibrary.Models
{
    public class LogMessage
    {
        public string Message { get; set; }

        public string TimeStamp { get; set; }


        public LogMessage(string message)
        {
            this.Message = message;
            TimeStamp = DateTime.Now.ToString();
        }
    }
}
