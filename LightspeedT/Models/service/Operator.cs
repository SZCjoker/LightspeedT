using LightspeedT.Models.Interface;
using LightspeedT.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace LightspeedT.Models.service
{
    public class Operator<T> where T:class
    {

        public Operator()
        {

        }

        //get all datas
        public T[] AllDatas()
        {
            IRepository<T> Repository = new GenericRepository<T>();
            var Result = Repository.GetAll().ToArray();
            return Result;
        }
        //query data by key
        public T DataByKey(Expression<Func<T, bool>> predicate)
        {
            IRepository<T> Repository = new GenericRepository<T>();
            var Result = Repository.Get(predicate);
            return Result;
        }//query datas by key
        public T[] DatasByKeys(Expression<Func<T, bool>> predicate)
        {
            IRepository<T> Repository = new GenericRepository<T>();
            var Result = Repository.Query(predicate);
            return Result.ToArray();
        }
        // count query data
        public int DataCount(Expression<Func<T, bool>> predicate)
        {
            IRepository<T> Repository = new GenericRepository<T>();
            var Result = Repository.Query(predicate).Count();
            return Result;
        }
        //query
        public T[] Queries(Expression<Func<T, bool>> predicate)
        {
            IRepository<T> Repository = new GenericRepository<T>();
            var Result = Repository.Query(predicate).ToArray();
            return Result;
        }


        public string Delete(Expression<Func<T, bool>> predicate)
        {
            IRepository<T> Repository = new GenericRepository<T>();
            Repository.Delete(predicate);
            return "";
        }

        public bool IsDataExist(Expression<Func<T, bool>> predicate)
        {
            IRepository<T> Repository = new GenericRepository<T>();

            var Count = Repository.Query(predicate).Count();
            return Count > 0;

        }
        public string Update(Expression<Func<T, bool>> predicate, T data)
        {
            IRepository<T> Repository = new GenericRepository<T>();
            // Repository.Create(data);
            Repository.CreateOrUpdate(predicate, data);
            return "";
        }

    }
}