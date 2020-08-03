
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace azure_proto_core
{
    // TODO: Think about other base classes for different resource 'containers'
    public class AzureResourceGroupBase : AzureEntity
    {
        private static readonly object resourceLock = new object();

        private ReadOnlyDictionary<string, object> Resources { get; set; } //whats the worst case if its public

        public AzureResourceGroupBase(ResourceIdentifier id) : this(id, null) { }

        public AzureResourceGroupBase(ResourceIdentifier id, Location location)
        {
            Id = id;
            Location = location;
            Resources = new ReadOnlyDictionary<string, object>(new Dictionary<string, object>());
        }

        public T GetCollection<T, E>(string type, Func<T> constructor)
            where T : AzureCollection<E>
            where E : AzureEntity
        {
            T result = null;
            object value;
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
            result = result ?? value as T;
            return result;
        }

        private void AddResouceCollection(string type, object collection)
        {
            var newCollection = new Dictionary<string, object>(Resources);
            newCollection.Add(type, collection);
            Resources = new ReadOnlyDictionary<string, object>(newCollection);
        }
    }
}
