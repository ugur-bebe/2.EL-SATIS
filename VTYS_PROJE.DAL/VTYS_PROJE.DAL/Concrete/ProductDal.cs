using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTYS_PROJE.Core.LogManager;
using VTYS_PROJE.Core.Repository;
using VTYS_PROJE.Entities.Concrete;

namespace VTYS_PROJE.Business.Concrete
{
    public class ProductDal : EntityRepository<Product>, IProductDal
    {
        public ProductDal(ILogManager logManager) : base(logManager)
        {

        }
    }
}
