using System.Collections.Generic;

namespace ACOS_be.Business
{
    public interface Service<T, TKey>
    {
        T Create(T entity);
        T Find(TKey id);
        IEnumerable<T> FindAll();
        T Update(TKey id, T updated);
        bool Delete(TKey id);
        bool Exists(TKey id);
    }
}