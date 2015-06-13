using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingDefaceDTO
{
    public class Email
    {
        public int id { get; set; }
        public string port { get; set; }
        public string host { get; set; }
        public int timeSend { get; set; }
        public string email { get; set; }
        public string passWords { get; set; }
        public bool enableSSL { get; set; }
        public bool isHost { get; set; }
    }
}
