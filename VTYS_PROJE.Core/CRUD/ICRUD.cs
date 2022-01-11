using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTYS_PROJE.Core.Results;
using VTYS_PROJE.Entities.Concrete;

namespace VTYS_PROJE.Core.CRUD
{
    public interface ICRUD<T>
    {
        Response<List<T>> GetAll(Expression<Func<T, bool>> filter = null);

        Response<T> GetById(int id);

        Response<int> Count(Expression<Func<T, bool>> filter = null);

        Response<NoContext> Add(T obje);

        Response<NoContext> Delete(int id);

        Response<NoContext> Update(T obje);
    }
}
