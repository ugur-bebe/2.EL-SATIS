using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTYS_PROJE.Core.Entities;

namespace VTYS_PROJE.Entities.Concrete
{
    public class Permission : IEntity
    {
        public int id { get; set; }

        public bool permission_1 { get; set; }

        public bool permission_2 { get; set; }

        public bool permission_3 { get; set; }

        public bool permission_4 { get; set; }
    }
}
