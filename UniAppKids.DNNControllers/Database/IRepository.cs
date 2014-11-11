using DotNetNuke.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniAppKids.DNNControllers.Database
{
    public interface IRepository<T> where T : class
    {
        void Delete(T item);

        void Delete(string sqlCondition, params object[] args);

        IEnumerable<T> Find(string sqlCondition, params object[] args);

        IPagedList<T> Find(int pageIndex, int pageSize, string sqlCondition, params object[] args);

        IEnumerable<T> Get();

        IEnumerable<T> Get<TScopeType>(TScopeType scopeValue);

        T GetById<TProperty>(TProperty id);

        T GetById<TProperty, TScopeType>(TProperty id, TScopeType scopeValue);

        IPagedList<T> GetPage(int pageIndex, int pageSize);

        IPagedList<T> GetPage<TScopeType>(TScopeType scopeValue, int pageIndex, int pageSize);

        void Insert(T item);

        void Update(T item);

        void Update(string sqlCondition, params object[] args);
    }
}