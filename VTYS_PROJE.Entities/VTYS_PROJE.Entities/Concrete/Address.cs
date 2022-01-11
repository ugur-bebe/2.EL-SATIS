using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTYS_PROJE.Core.Entities;

namespace VTYS_PROJE.Entities.Concrete
{
    public class Address : IEntity
    {
        public int id { get; set; }

        public int city_id { get; set; }

        public int district_id { get; set; }

        public string address_description { get; set; }
    }
}
