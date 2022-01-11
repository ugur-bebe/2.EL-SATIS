using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace VTYS_PROJE.Core.Repository
{
    public interface INoSqlEntitiyRepository<T>
    {
        bool Insert(ref T entity);
        bool Update(T entity);
        bool Delete(int id);

        T GetById(string id);

        List<T> GetAll(Expression<Func<T, bool>> filter = null);

        int Count(Expression<Func<T, bool>> filter = null);
    }
}
