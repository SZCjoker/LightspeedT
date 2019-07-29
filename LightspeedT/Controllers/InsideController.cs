using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LightspeedT.Models;
using LightspeedT.Models.service;
using LightspeedT.Models.partial;
using System.Web.Http;
using Newtonsoft;

namespace LightspeedT.Controllers
{
    [System.Web.Http.RoutePrefix("apitest")]
    public class InsideController : baseController
    {   /// <summary>
    /// 新增會員
    /// </summary>
    /// <param name="acc"></param>
    ///  <param ID, pwd ></param>
    /// <returns></returns>
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("~/apitest/add")]
        public IHttpActionResult Newmember(Members acc)
        {
            var inside = Role.GetQ<Member>().DataByKey(p => p.MemID.ToUpper() == acc.MemID.ToUpper());
            if (inside != null) return GetResult(data: "此帳號已有人使用");
            var mem = new Member();
            mem.MemID = acc.MemID;
            mem.MemPWD = acc.MemPWD;
            mem.DeleteorNot = false;
            var memt = new Memdetail
            { MemID=acc.MemID,
              MemCN ="",
              Age=0,
              Gender="",
              Add="",
              Mailadd="",
              PhoneNum=""
            };
            
            Role.GetOp<Member>().Update(p => p.MemID == acc.MemID & p.MemPWD == acc.MemPWD,mem);
            Role.GetOp<Memdetail>().Update(p => p.MemID == acc.MemID,memt);
            return GetResult(true);
        }
        /// <summary>
        /// 取得所有正常會員資料
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        [AllowCORS]
        [System.Web.Http.Route("~/apitest/getalldata")]
        public IEnumerable<Memdetail> GetMemberDetails()
        {
            var data = Role.GetQ<Member>().Queries(p=>p.DeleteorNot==false);
            var idlist=data.Select(p => p.MemID);
            var result = new List<Memdetail>();
            foreach (var id in idlist)
            {
                var datas = Role.GetQ<Memdetail>().DataByKey(p => p.MemID == id);
                result.Add(datas);
            }

            return  result;
        }
        /// <summary>
        /// 取得會員資料BYID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("~/apitest/getdatabyKey")]
        public IHttpActionResult GetMDetailbyKey(Members mem)
        {
            List<Members> list = new List<Members>();
            var data = Role.GetQ<Member>().Queries(p => p.MemID.ToUpper() == mem.MemID.ToUpper());
            if (data == null) return GetResult(data: "查無此帳號");
            var id = data.FirstOrDefault().MemID.ToUpper();
            var result = Role.GetQ<Memdetail>().Queries(p => p.MemID.ToUpper() ==id );
            return GetResult(result);
        }

        /// <summary>
        /// 編輯會員資料
        /// </summary>
        /// <param name="MD"></param>        
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("~/apitest/edit")]
        public IHttpActionResult MDataDetail(MemberDetial MD)
        {               
           var taget = Role.GetOp<Memdetail>();
           var query = Role.GetQ<Memdetail>().DataByKey(p=>p.MemID==MD.MemID);
            query.MemCN = MD.MemCN;
            query.Gender = MD.Gender;
            query.Age = MD.Age;
            query.Add = MD.Add;
            query.Mailadd = MD.Mailadd;
            query.PhoneNum = MD.PhoneNum;
            if( taget.IsDataExist(p => p.MemID.ToUpper() == MD.MemID.ToUpper()))
              taget.Update(p=>p.MemID==MD.MemID,query);
            return  GetResult(true);
        }

        /// <summary>
        /// 刪除資料
        /// </summary>
        /// <param name="acc"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("~/apitest/delete")]
        public IHttpActionResult DeleteMember(Members acc)
        {
            var target = Role.GetOp<Member>(); //member list
            var query = Role.GetQ<Member>().DataByKey(p => p.MemID.ToUpper() == acc.MemID.ToUpper());//value want to change
            
            //detail want to change
            query.DeleteorNot = true;
            target.Update(p=>p.MemID==acc.MemID,query);

            //var detailtarget = Role.GetOp<Memdetail>(); 刪除測試用
            //var querydetail=Role.GetQ<Memdetail>().DataByKey(p => p.MemID.ToUpper() == acc.MemID.ToUpper());
            //querydetail.MemID = "";
            //querydetail.MemCN = "";
            //querydetail.Gender = "";
            //querydetail.Age = 0;
            //querydetail.Add = "";
            //querydetail.Mailadd = "";
            //querydetail.PhoneNum = "";
            //detailtarget.Update(p => p.MemID == acc.MemID,querydetail);
            return GetResult( true);
        }


        
    }
}