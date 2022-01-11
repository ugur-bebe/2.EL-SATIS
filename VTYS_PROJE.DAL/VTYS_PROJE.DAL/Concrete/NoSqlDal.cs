using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTYS_PROJE.Core.LogManager;
using VTYS_PROJE.Core.Repository;
using VTYS_PROJE.DAL.Abstarct;
using VTYS_PROJE.Entities.Concrete;

namespace VTYS_PROJE.DAL.Concrete
{
    public class NoSqlDal : NoSqlEntitiyRepository<ProductProperty>, INoSqlDal
    {
        public NoSqlDal(ILogManager logManager) : base(logManager)
        {

        }
    }
}
