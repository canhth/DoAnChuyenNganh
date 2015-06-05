using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TrackingDefaceDTO
{
    public class ImageContent
    {
        public int id { get; set; }
        public string Content { get; set; }
        public int webID { get; set; }
    }
}
