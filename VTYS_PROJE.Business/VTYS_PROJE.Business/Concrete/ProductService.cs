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
    public class ProductService : IProductService
    {
        private readonly IProductDal _productDal;

        public ProductService(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public Response<NoContext> Add(Product user)
        {
            if (_productDal.Insert(user))
                return Response<NoContext>.Success(204);
            else return Response<NoContext>.Fail("Hata oluştu!", 200);
        }

        public Response<int> Count(Expression<Func<Product, bool>> filter = null)
        {
            int c = _productDal.Count(filter);
            if (c != -1)
            {
                return Response<int>.Success(c, 204);
            }

            return Response<int>.Fail("Hata oluştu!", 200);

        }

        public Response<NoContext> Delete(int id)
        {
            if (_productDal.GetById(id) != null)
            {
                if (_productDal.Delete(id))
                    return Response<NoContext>.Success(204);
                else return Response<NoContext>.Fail("Hata oluştu!", 200);
            }
            return Response<NoContext>.Success(404);
        }

        public Response<List<Product>> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            List<Product> users = _productDal.GetAll(filter);

            if (users != null)
            {
                return Response<List<Product>>.Success(users, 204);
            }
            return Response<List<Product>>.Fail("Veri Bulunamadı!", 404);
        }

        public Response<Product> GetById(int userId)
        {
            Product user = _productDal.GetById(userId);
            if (user != null)
            {
                return Response<Product>.Success(user, 204);
            }
            return Response<Product>.Fail("Veri Bulunamadı!", 404);
        }


        public Response<NoContext> Update(Product user)
        {
            if (_productDal.GetById(user.id) != null)
            {
                if (_productDal.Update(user))
                    return Response<NoContext>.Success(204);
                else return Response<NoContext>.Fail("Hata oluştu!", 200);
            }
            return Response<NoContext>.Fail("Kullanıcı bulunamadı!", 404);
        }
    }
}
