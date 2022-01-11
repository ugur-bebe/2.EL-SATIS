using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTYS_PROJE.Core.Entities;

namespace VTYS_PROJE.Entities.Concrete
{
    public class ProductProperty : IEntity
    {
        
        public ObjectId _id { get; set; }

        public string image_id { get; set; }

        public List<string> images { get; set; }
    }
}
