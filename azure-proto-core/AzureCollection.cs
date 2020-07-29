using Azure;
using System.Collections;
using System.Collections.Generic;
using System.Net;

namespace azure_proto_core
{
    public abstract class AzureCollection<T> : IEnumerable<T> //TODO look at collection interfaces to implement to get rid of wierdness of getitems(...) IDictionary maybe
        where T: AzureEntity
    {
        protected TrackedResource Parent { get; private set; }

        public T this[string key]
        {
            get
            {
                T value;
                try
                {
                    value = Get(key);
                }
                catch(RequestFailedException e) when (e.Status == (int)HttpStatusCode.NotFound)
                {
                    throw new KeyNotFoundException("The resource you were accessing was not found by the service"); //TODO decide if we want to have a new exception here and best way to preserve the stack
                }
                return value;
            }
        }

        protected abstract IEnumerable<T> GetItems();

        protected abstract T Get(string name);

        protected AzureCollection(TrackedResource parent)
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
            catch (RequestFailedException e) when (e.Status == (int)HttpStatusCode.NotFound)
            {
                value = null;
                return false;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return GetItems().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetItems().GetEnumerator();
        }
    }
}
