using System.Collections.Generic;

namespace azure_proto_core
{
    public abstract class AzureCollection<T> : Dictionary<string, T>
        where T: class
    {
        protected IResource Parent { get; private set; }

        new public T this[string key]
        {
            get
            {
                //lazy load on first access
                T value;
                if (!this.TryGetValue(key, out value))
                {
                    value = GetSingleValue(key);
                    if (value == null)
                        throw new KeyNotFoundException();
                    Add(key, value);
                }
                return value;
            }
            set { /*disable setting values*/ }
        }

        protected abstract void LoadValues();

        protected abstract T GetSingleValue(string key);

        protected AzureCollection(IResource parent)
        {
            Parent = parent;
        }
    }
}
