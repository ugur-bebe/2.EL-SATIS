using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTYS_PROJE.Business.Abstract;
using VTYS_PROJE.Core.Results;
using VTYS_PROJE.DAL.Abstarct;
using VTYS_PROJE.Entities.Concrete;

namespace VTYS_PROJE.Business.Concrete
{
    public class NoSqlService : INoSqlService
    {
        private readonly INoSqlDal _noSqlDal;

        public NoSqlService(INoSqlDal noSqlDal)
        {
            _noSqlDal = noSqlDal;
        }
        public Response<NoContext> Add(ref ProductProperty obje)
        {
            _noSqlDal.Insert(ref obje);

            return Response<NoContext>.Success(201);
        }

        public Response<int> Count(Expression<Func<ProductProperty, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Response<NoContext> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Response<List<ProductProperty>> GetAll(Expression<Func<ProductProperty, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Response<ProductProperty> GetById(string id)
        {
            var a = _noSqlDal.GetById(id);
            return Response<ProductProperty>.Success(a, 201);
        }

        public Response<NoContext> Update(ProductProperty obje)
        {
            throw new NotImplementedException();
        }
    }
}
