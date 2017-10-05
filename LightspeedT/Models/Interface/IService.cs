using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightspeedT.Models.Interface
{
    interface IService<T> where T:class
    {

        IResult Create(T instance);
        IResult Update(T instance);
        IEnumerable<T> GetAll(int? page = null, int? rows = null);
        T Get();
        bool IsExists();
        int PageCount(int rows);
        void SetValue(Dictionary<string, object> parameters);
        void SaveChange();
       }
    public interface IResult : IDisposable
    {
        Guid ID
        {
            get;
        }

        bool Success
        {
            get;
            set;
        }

        string Message
        {
            get;
            set;
        }

        Exception Exception
        {
            get;
            set;
        }

        List<IResult> InnerResults
        {
            get;
        }
    }
}
