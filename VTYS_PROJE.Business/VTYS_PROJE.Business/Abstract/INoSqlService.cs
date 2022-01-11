using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTYS_PROJE.Core.CRUD;
using VTYS_PROJE.Core.Results;
using VTYS_PROJE.Entities.Concrete;

namespace VTYS_PROJE.Business.Abstract
{
    public interface INoSqlService
    {
        Response<List<ProductProperty>> GetAll(Expression<Func<ProductProperty, bool>> filter = null);

        Response<ProductProperty> GetById(string id);

        Response<int> Count(Expression<Func<ProductProperty, bool>> filter = null);

        Response<NoContext> Add(ref ProductProperty obje);

        Response<NoContext> Delete(int id);

        Response<NoContext> Update(ProductProperty obje);
    }
}
