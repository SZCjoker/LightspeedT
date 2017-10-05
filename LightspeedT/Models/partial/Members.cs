using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LightspeedT.Models.partial
{
   public partial class Members
    {
        [Required(ErrorMessage = "請輸入帳號")]
        public string MemID { get; set; }
        [Required(ErrorMessage = "密碼不能為空")]
        public string MemPWD { get; set; }
        public bool DeletorNot { get; set; }
    }
}