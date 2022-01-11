using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTYS_PROJE.Core.Entities;
using VTYS_PROJE.Core.LogManager;

namespace VTYS_PROJE.Core.Repository
{
    public interface IEntityRepository<T>
    {
        bool Insert(T entity);
        bool Update(T entity);
        bool Delete(int id);

        T GetById(int id);

        List<T> GetAll(Expression<Func<T, bool>> filter = null);

        int Count(Expression<Func<T, bool>> filter = null);
    }
}
