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
    public class DistrictService : IDistrictService
    {
        private readonly IDistrictDal _districtDal;
        public DistrictService(IDistrictDal districtDal)
        {
            _districtDal = districtDal;
        }

        public Response<NoContext> Add(District obje)
        {
            throw new NotImplementedException();
        }

        public Response<int> Count(Expression<Func<District, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Response<NoContext> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Response<List<District>> GetAll(Expression<Func<District, bool>> filter = null)
        {
            List<District> districts = _districtDal.GetAll(filter);

            if (districts != null)
            {
                return Response<List<District>>.Success(districts, 204);
            }
            return Response<List<District>>.Fail("Veri Bulunamadı!", 404);
        }

        public Response<District> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Response<NoContext> Update(District obje)
        {
            throw new NotImplementedException();
        }
    }
}
