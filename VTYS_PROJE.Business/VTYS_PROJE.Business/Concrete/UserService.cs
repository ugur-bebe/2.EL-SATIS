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
    public class UserService : IUserService
    {
        //Burada veri kontrolleri yapılıyor
        //Ve ardından Data Access Layera geçiyoruz
        private readonly IUserDal _userDal;

        public UserService(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public Response<NoContext> Add(User user)
        {
            string user_name = user.user_name;
            var a = _userDal.GetAll(x => x.user_name == user_name);
            if (a.Count == 0)
            {
                _userDal.Insert(user);

                return Response<NoContext>.Success(204);
            }

            return Response<NoContext>.Fail("Kullanıcı Adı Daha Önce Kullanılmış!", 200);
        }

        public Response<int> Count(Expression<Func<User, bool>> filter = null)
        {
            int c = _userDal.Count();
            if (c != -1)
            {
                return Response<int>.Success(c, 204);
            }

            return Response<int>.Fail("Hata oluştu!", 200);

        }

        public Response<NoContext> Delete(int id)
        {
            if (_userDal.GetById(id) != null)
            {
                _userDal.Delete(id);
                return Response<NoContext>.Success(204);
            }
            return Response<NoContext>.Success(404);
        }

        public Response<List<User>> GetAll(Expression<Func<User, bool>> filter = null)
        {
            List<User> users = _userDal.GetAll(filter);

            if (users != null)
            {
                return Response<List<User>>.Success(users, 204);
            }
            return Response<List<User>>.Fail("Veri Bulunamadı!", 404);
        }

        public Response<User> GetById(int userId)
        {
            User user = _userDal.GetById(userId);
            if (user != null)
            {
                return Response<User>.Success(user, 204);
            }
            return Response<User>.Fail("Veri Bulunamadı!", 404);
        }


        public Response<NoContext> Update(User user)
        {
            if (_userDal.GetById(user.id) != null)
            {
                if (_userDal.Update(user))
                {
                    return Response<NoContext>.Success(204);
                }
            }

            return Response<NoContext>.Fail("Kullanıcı bulunamadı!", 404);
        }
    }
}
