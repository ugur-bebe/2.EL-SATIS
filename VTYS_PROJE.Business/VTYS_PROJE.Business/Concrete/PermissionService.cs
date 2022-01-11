using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTYS_PROJE.Core.Results;
using VTYS_PROJE.Entities.Concrete;

namespace VTYS_PROJE.Business.Concrete
{
    public class PermissionService : IPermissionService
    {
        public Response<NoContext> Add(Permission obje)
        {
            throw new NotImplementedException();
        }

        public Response<int> Count(Expression<Func<Permission, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Response<NoContext> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Response<List<Permission>> GetAll(Expression<Func<Permission, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Response<Permission> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Response<NoContext> Update(Permission obje)
        {
            throw new NotImplementedException();
        }
    }
}
