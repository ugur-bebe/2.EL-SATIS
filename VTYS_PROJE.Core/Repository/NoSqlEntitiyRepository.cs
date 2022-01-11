using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using VTYS_PROJE.Core.LogManager;
using VTYS_PROJE.Entities.Concrete;

namespace VTYS_PROJE.Core.Repository
{
    public class NoSqlEntitiyRepository<TEntity> : INoSqlEntitiyRepository<TEntity>
    {
        private readonly ILogManager _logManager;
        private IMongoCollection<TEntity> _propertiesCollection;

        public NoSqlEntitiyRepository(ILogManager logManager)
        {
            _logManager = logManager;

            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("VTYS_PROJE");
            _propertiesCollection = database.GetCollection<TEntity>("PROPERTIES");
        }

        public int Count(Expression<Func<TEntity, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public TEntity GetById(string id)
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", ObjectId.Parse(id));
            var a = _propertiesCollection.Find(filter).ToList();
            return a.FirstOrDefault();
        }

        public bool Insert(ref TEntity entity)
        {
            _propertiesCollection.InsertOne(entity);

            return true;
        }

        public bool Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
