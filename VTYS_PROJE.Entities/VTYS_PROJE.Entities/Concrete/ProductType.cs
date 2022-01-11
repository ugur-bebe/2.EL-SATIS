﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTYS_PROJE.Core.Entities;

namespace VTYS_PROJE.Entities.Concrete
{
    public class ProductType : IEntity
    {
        public int id { get; set; }

        public string type_name { get; set; }

        public int category_id { get; set; }
    }
}
