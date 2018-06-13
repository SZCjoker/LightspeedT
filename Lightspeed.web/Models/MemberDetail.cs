using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lightspeed.web.Models
{
    public partial class MemberDetail
    {
        public int SN { get; set; }
        public string MemID { get; set; }
        public string MemCN { get; set; }
        public int? Age { get; set; }
        public string Gender { get; set; }
        public string Add { get; set; }
        public string Mailadd { get; set; }
        public string PhoneNum { get; set; }
    }
}