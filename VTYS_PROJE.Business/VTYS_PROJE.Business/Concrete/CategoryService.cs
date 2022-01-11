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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;

        public CategoryService(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public Response<NoContext> Add(Category category)
        {
            string c = category.category_name;

            var a = _categoryDal.GetAll(x => x.category_name == c);

            var _category = _categoryDal.GetAll(x => x.category_name == c);

            if (_category == null || _category.Count == 0)
            {
                _categoryDal.Insert(category);

                return Response<NoContext>.Success(204);
            }

            return Response<NoContext>.Fail("Kullanıcı Adı Daha Önce Kullanılmış!", 200);
        }

        public Response<int> Count(Expression<Func<Category, bool>> filter = null)
        {
            int c = _categoryDal.Count();
            if (c != -1)
            {
                return Response<int>.Success(c, 204);
            }

            return Response<int>.Fail("Hata oluştu!", 200);

        }

        public Response<NoContext> Delete(int id)
        {
            if (_categoryDal.GetById(id) != null)
            {
                _categoryDal.Delete(id);
                return Response<NoContext>.Success(204);
            }
            return Response<NoContext>.Success(404);
        }

        public Response<List<Category>> GetAll(Expression<Func<Category, bool>> filter = null)
        {
            List<Category> categories = _categoryDal.GetAll(filter);

            if (categories != null)
            {
                return Response<List<Category>>.Success(categories, 204);
            }
            return Response<List<Category>>.Fail("Veri Bulunamadı!", 404);
        }

        public Response<Category> GetById(int userId)
        {
            Category user = _categoryDal.GetById(userId);
            if (user != null)
            {
                return Response<Category>.Success(user, 204);
            }
            return Response<Category>.Fail("Veri Bulunamadı!", 404);
        }


        public Response<NoContext> Update(Category user)
        {
            if (_categoryDal.GetById(user.id) != null)
            {
                _categoryDal.Update(user);
                return Response<NoContext>.Success(204);
            }
            return Response<NoContext>.Fail("Kullanıcı bulunamadı!", 404);
        }
    }
}
