using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTYS_PROJE.Core.Entities;

namespace VTYS_PROJE.Entities.Concrete
{
    public class UserType : IEntity
    {
        public int id { get; set; }

        public string type { get; set; }

        public int permission_id { get; set; }
    }
}
