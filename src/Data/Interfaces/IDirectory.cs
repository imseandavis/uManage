using System.Collections.Generic;

namespace S203.uManage.Data.Interfaces
{
    public interface IDirectory<T>
    {
        IEnumerable<T> All { get; }
        T Find(string identity);
        T Upsert(T entity);
        void Delete(T entity);
    }
}
