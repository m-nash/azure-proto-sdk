using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace azure_proto_core
{
    // TODO: Think about other base classes for different resource 'containers'
    public abstract class AzureEntityHolder<T> : AzureEntity<T>
        where T: TrackedResource
    {
        private static readonly object resourceLock = new object();

        //TODO: do we want to expose this as public to list/search all resource types in a resourcegroup
        private ReadOnlyDictionary<Type, object> Resources { get; set; }

        public AzureEntityHolder(TrackedResource parent, T model):base(parent, model)
        {
            Resources = new ReadOnlyDictionary<Type, object>(new Dictionary<Type, object>());
        }

        public C GetCollection<C, E>(Func<C> constructor)
            where C : AzureCollection<E>
            where E : AzureEntity
        {
            C result = null;
            object value;
            var type = typeof(C);
            if (!Resources.TryGetValue(type, out value))
            {
                lock (resourceLock)
                {
                    if (!Resources.TryGetValue(type, out value))
                    {
                        result = constructor();
                        AddResouceCollection(type, result);
                    }
                }
            }
            result = result ?? value as C;
            return result;
        }

        private void AddResouceCollection(Type type, object collection)
        {
            var newCollection = new Dictionary<Type, object>(Resources);
            newCollection.Add(type, collection);
            Resources = new ReadOnlyDictionary<Type, object>(newCollection);
        }

        public IEnumerable<AzureEntity> Entities
        {
            get
            {
                foreach (AzureCollection<AzureEntity> collection in Resources.Values)
                {
                    foreach(AzureEntity entity in collection)
                    {
                        yield return entity;
                    }
                }
            }
        }
    }
}
