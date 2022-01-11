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
    public class AddressService : IAddressService
    {
        private readonly IAddressDal _addressDal;

        public AddressService(IAddressDal addressDal)
        {
            this._addressDal = addressDal;
        }

        public Response<NoContext> Add(Address obje)
        {
            _addressDal.Insert(obje);

            return Response<NoContext>.Success(204);
        }

        public Response<int> Count(Expression<Func<Address, bool>> filter = null)
        {
            int count =_addressDal.Count();
            return Response<int>.Success(count, 204);
        }

        public Response<NoContext> Delete(int id)
        {
            _addressDal.Delete(id);
            return Response<NoContext>.Success(204);
        }

        public Response<List<Address>> GetAll(Expression<Func<Address, bool>> filter = null)
        {
            List<Address> addresses = _addressDal.GetAll();
            return Response<List<Address>>.Success(addresses, 204);
        }

        public Response<Address> GetById(int id)
        {
            Address address = _addressDal.GetById(id);
            return Response<Address>.Success(address, 204);
        }

        public Response<NoContext> Update(Address obje)
        {
            if (_addressDal.GetById(obje.id) != null)
            {
                _addressDal.Update(obje);
                return Response<NoContext>.Success(204);
            }
            return Response<NoContext>.Fail("Kullanıcı bulunamadı!", 404);
        }
    }
}
