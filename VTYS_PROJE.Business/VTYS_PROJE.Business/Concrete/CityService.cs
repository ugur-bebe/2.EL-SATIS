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
    public class CityService : ICityService
    {
        private readonly ICityDal _cityDal;

        public CityService(ICityDal cityDal)
        {
            _cityDal = cityDal;
        }
        public Response<NoContext> Add(City obje)
        {
            throw new NotImplementedException();
        }

        public Response<int> Count(Expression<Func<City, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Response<NoContext> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Response<List<City>> GetAll(Expression<Func<City, bool>> filter = null)
        {
            List<City> cities = _cityDal.GetAll(filter);

            if (cities != null)
            {
                return Response<List<City>>.Success(cities, 204);
            }
            return Response<List<City>>.Fail("Veri Bulunamadı!", 404);
        }

        public Response<City> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Response<NoContext> Update(City obje)
        {
            throw new NotImplementedException();
        }
    }
}
