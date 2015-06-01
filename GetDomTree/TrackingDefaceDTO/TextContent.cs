using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingDefaceDTO
{
    public class TextContent
    {
        public int ContentID { get; set; }
        public string Content { get; set; }
        public DateTime TimeCheck { get; set; }
        public string TextResult { get; set; }
        public int WebID { get; set; }

        public TextContent() { }

        public TextContent(int id, string content, DateTime timeCheck, string result, int webid)
        {  
            this.ContentID = id;
            this.Content = content;
            this.TimeCheck = timeCheck;
            this.TextResult = result;     
            this.WebID = webid;
        }
    }
}
