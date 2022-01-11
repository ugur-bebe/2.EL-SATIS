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
    public class ProductTypeService : IProductTypeService
    {
        private readonly IProductTypeDal _productTypeDal;
        public ProductTypeService(IProductTypeDal productTypeDal)
        {
            _productTypeDal = productTypeDal;
        }

        public Response<NoContext> Add(ProductType obje)
        {
            throw new NotImplementedException();
        }

        public Response<int> Count(Expression<Func<ProductType, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Response<NoContext> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Response<List<ProductType>> GetAll(Expression<Func<ProductType, bool>> filter = null)
        {
            List<ProductType> productTypes = _productTypeDal.GetAll(filter);

            if (productTypes != null)
            {
                return Response<List<ProductType>>.Success(productTypes, 204);
            }
            return Response<List<ProductType>>.Fail("Veri Bulunamadı!", 404);
        }

        public Response<ProductType> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Response<NoContext> Update(ProductType obje)
        {
            throw new NotImplementedException();
        }
    }
}
