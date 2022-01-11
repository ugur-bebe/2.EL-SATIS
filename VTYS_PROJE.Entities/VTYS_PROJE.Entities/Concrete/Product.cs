using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTYS_PROJE.Core.Entities;

namespace VTYS_PROJE.Entities.Concrete
{
    public class Product : IEntity
    {
        public int id { get; set; }

        public int product_type_id { get; set; }

        public int owner_id { get; set; }

        public string title { get; set; }

        public string explanation { get; set; }

        public int price { get; set; }

        public string no_sql_property_id { get; set; }

        public DateTime release_date { get; set; }

        public string category_name { get; set; }

        public string ownername { get; set; }

        public string phone_number { get; set; }

        public string type_name { get; set; }

        public string base64 { get; set; }
    }
}
