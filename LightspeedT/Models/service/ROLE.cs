using LightspeedT.Models.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LightspeedT.Models.service
{
    public static class Role  // can be any role   just  do authority control 
    {
        public static Operator<T> GetOp<T>() where T : class
        {
            Operator<T> op = new Operator<T>();
            return op;
        }
        public static Query<T> GetQ<T>() where T : class
        {
            Query<T> query = new Query<T>();
            return query;
        }

      



    }
}