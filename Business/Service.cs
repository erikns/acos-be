using System.Collections.Generic;

namespace ACOS_be.Business
{
    public interface Service<T>
    {
        T Create(T entity);
        T Find(int id);
        IEnumerable<T> FindAll();
        T Update(int id, T updated);
        bool Delete(int id);
        bool Exists(int id);
    }
}