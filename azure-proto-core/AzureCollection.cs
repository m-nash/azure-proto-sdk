using System;
using System.Collections.Generic;

namespace azure_proto_core
{
    public abstract class AzureCollection<T>
        where T: AzureResource
    {
        protected IResource Parent { get; private set; }

        public T this[string key]
        {
            get
            {
                //lazy load on first access
                T value = Get(key);
                if (value == null)
                    throw new KeyNotFoundException();
                return value;
            }
        }

        public abstract IEnumerable<T> GetItems();

        protected abstract T Get(string name);

        protected AzureCollection(IResource parent)
        {
            Parent = parent;
        }

        public bool TryGetValue(string name, out T value)
        {
            try
            {
                value = Get(name);
                return true;
            }
            catch(Exception) //should only be catching 404 here
            {
                value = null;
                return false;
            }
        }
    }
}
