using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTYS_PROJE.Core.Entities;

namespace VTYS_PROJE.Entities.Concrete
{
    public class User : IEntity
    {
        public int id { get; set; }

        public string user_name { get; set; }

        public string email { get; set; }

        public string name { get; set; }

        public string surname { get; set; }

        public string password { get; set; }

        public string phone_number { get; set; }

        public int address_id { get; set; }

        public int user_type { get; set; }

        public string address_description { get; set; }

        public string city_name { get; set; }

        public string district_name { get; set; }

    }
}
