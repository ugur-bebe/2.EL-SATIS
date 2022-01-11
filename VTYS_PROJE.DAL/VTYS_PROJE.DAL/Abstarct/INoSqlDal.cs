using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTYS_PROJE.Core.LogManager;
using VTYS_PROJE.Core.Repository;
using VTYS_PROJE.Entities.Concrete;

namespace VTYS_PROJE.DAL.Abstarct
{
    public interface INoSqlDal : INoSqlEntitiyRepository<ProductProperty>
    {

    }
}
