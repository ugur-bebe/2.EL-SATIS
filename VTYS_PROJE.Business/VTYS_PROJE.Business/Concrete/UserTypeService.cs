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
    public class UserTypeService : IUserTypeService
    {
        private readonly IUserTypeDal _userTypeDal;

        public UserTypeService(IUserTypeDal userTypeDal)
        {
            this._userTypeDal = userTypeDal;
        }

        public Response<NoContext> Add(UserType user)
        {
            _userTypeDal.Insert(user);

            return Response<NoContext>.Success(204);
        }

        public Response<int> Count(Expression<Func<UserType, bool>> filter = null)
        {
            int count = _userTypeDal.Count();
            return Response<int>.Success(count, 204);
        }

        public Response<NoContext> Delete(int id)
        {
            _userTypeDal.Delete(id);
            return Response<NoContext>.Success(204);
        }

        public Response<List<UserType>> GetAll(Expression<Func<UserType, bool>> filter = null)
        {
            List<UserType> userTypes = _userTypeDal.GetAll();
            return Response<List<UserType>>.Success(userTypes, 204);
        }

        public Response<UserType> GetById(int userId)
        {
            UserType userType = _userTypeDal.GetById(userId);

            return Response<UserType>.Success(userType, 204);
        }

        public Response<NoContext> Update(UserType user)
        {
            _userTypeDal.Update(user);
            return Response<NoContext>.Success(204);
        }
    }
}
