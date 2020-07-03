using System;
using System.Collections.Generic;
using System.Text;

namespace AskApp.Common.Interfaces
{
    public interface IRepository<TType, TIdType> : IDisposable
        where TType : class
    {
        TType Create(TType entity);
        TType Modify(TType entity);
        bool Delete(TType entity);
        List<TType> GetAll();
        TType GetById(TIdType id);
    }
}
