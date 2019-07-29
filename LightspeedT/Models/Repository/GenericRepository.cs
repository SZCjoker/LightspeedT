using LightspeedT.Models.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace LightspeedT.Models.Repository
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        #region constructor
        public GenericRepository() : this(new TestDBEntities())
        {

        }
        public GenericRepository(DbContext context)
        {
            _context = context;
        }
        public GenericRepository(ObjectContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            this._context = new DbContext(context, true);
        }
        #endregion

        #region destructor
        ~GenericRepository()
        {
            Dispose(true);
        }
        #endregion

        #region dispose  resource 
        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._context.Dispose();
                this._context = null;
                GC.SuppressFinalize(this);
            }
        }
        #endregion

        #region property
        private DbContext _context { get; set; }
        #endregion

        #region method 
        public void Create(T instance)
        {
            if (instance == null) throw new ArgumentNullException("instance is null");
            _context.Set<T>().Add(instance);
            SaveChanges();
        }
        public T Get(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }
        public IQueryable<T> Query(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsQueryable();
        }

        public void Update(T instance)
        {
            if (instance == null) throw new ArgumentNullException("context");
            _context.Entry(instance).State = EntityState.Modified;
            SaveChanges();
        }

        public void CreateOrUpdate(Expression<Func<T, bool>> predicate, T instance)
        {

            //try
            //{
            //    if (instance == null) throw new ArgumentNullException("context");
            //    var IsExist = _context.Set<T>().Any(predicate);

            //    if (!IsExist)
            //    {
            //        _context.Set<T>().Add(instance);
            //        SaveChanges();
            //    }
            //    else
            //    {
            //        _context.Entry(instance).State = EntityState.Modified;
            //        SaveChanges();
            //    }
            //}
            //catch (DbEntityValidationException ex)
            //{
            //    var entityError = ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage);
            //    var getFullMessage = string.Join("; ", entityError);
            //    var exceptionMessage = string.Concat(ex.Message, "errors are: ", getFullMessage);
            //    //NLog
            //    //LogException(new Exception(string.Format("File : {0} {1}.", logFile.FullName, exceptionMessage), ex));
            //}


            if (instance == null) throw new DbEntityValidationException("context");
            var IsExist = _context.Set<T>().Any(predicate);

            if (!IsExist)
            {
                _context.Set<T>().Add(instance);
                SaveChanges();
            }
            else
            {
                _context.Entry(instance).State = EntityState.Modified;
                SaveChanges();
            }



        }


        public void Delete(Expression<Func<T, bool>> predicate)
        {
            var Target = _context.Set<T>().FirstOrDefault(predicate);
            if (Target == null) throw new ArgumentNullException("The Target doesn't exist");
            _context.Set<T>().Remove(Target);
            SaveChanges();

        }
        public void SaveChanges()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        #endregion




    }


}