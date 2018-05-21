using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LightspeedT.Models.partial
{
   public partial class MemberDetial
    {
        public string MemID { get; set; }
        public string MemCN { get; set; }
        public int? Age { get; set; }
        public string Gender { get; set; }
        public string Add { get; set; }        
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4})$", ErrorMessage = "請輸入正確的電子郵件位址.")]
        public string Mailadd { get; set; }
        public string PhoneNum { get; set; }


    }
}