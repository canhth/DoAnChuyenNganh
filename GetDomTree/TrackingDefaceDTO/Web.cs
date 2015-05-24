using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingDefaceDTO
{
    public class Web
    {
        public int webID { get; set; }
        public string nameSite { get; set; }
        public string uRL { get; set; }
        public string ipPulbic { get; set; }
        public string ipPrivate { get; set; }
        public int webPriority { get; set; }
        public string phones { get; set; }
        public string emails { get; set; }
        public string searchText { get; set; }
        public string webStatus { get; set; }
        public string banText { get; set; }
        public int isEnable { get; set; }

        public Web() { }

        public Web (int webid, string namsite, string url, string ippub, string ippri, int priority, string phone, string email, string searchtext, string status, string bantext, int isenable)
        {
            this.webID = webid;
            this.nameSite = namsite;
            this.uRL = url;
            this.ipPulbic = ippub;
            this.ipPrivate = ippri;
            this.webPriority = priority;
            this.phones = phone;
            this.emails = email;
            this.searchText = searchtext;
            this.webStatus = status;
            this.banText = bantext;
            this.isEnable = isenable;
        }
    }
}
