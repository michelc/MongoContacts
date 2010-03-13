using System;
using System.Linq;
using NoRM;
using NoRM.Linq;

namespace MongoContact.Models
{
    public class MongoSession : IDisposable
    {
        MongoQueryProvider _provider;

        public MongoSession()
        {
            _provider = new MongoQueryProvider("ContactManager");
        }

        public MongoQueryProvider Provider
        {
            get
            {
                return _provider;
            }
        }

        public IQueryable<Contact> Contacts
        {
            get { return new MongoQuery<Contact>(_provider); }
        }

        public T MapReduce<T>(string map, string reduce)
        {
            T result = default(T);
            using (var mr = _provider.Server.CreateMapReduce())
            {
                var response = mr.Execute(new MapReduceOptions(typeof(T).Name) { Map = map, Reduce = reduce });
                var coll = response.GetCollection<MapReduceResult>();
                var r = coll.Find().FirstOrDefault();
                result = (T)r.Value;
            }
            return result;
        }

        public void Add<T>(T item) where T : class, new()
        {
            _provider.DB.GetCollection<T>().Insert(item);
        }

        public void Update<T>(T item) where T : class, new()
        {
            _provider.DB.GetCollection<T>().UpdateOne(item, item);
        }

        public bool Updatable<T>(T item) where T : class, new()
        {
            return _provider.DB.GetCollection<T>().Updateable;
        }

        public void Drop<T>()
        {
            _provider.DB.DropCollection(typeof(T).Name);
        }

        public void Dispose()
        {
            _provider.Server.Dispose();
        }

    }
}