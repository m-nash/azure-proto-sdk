using System.Collections.Generic;

namespace azure_proto_sdk
{
    public abstract class AzureCollection<T> : Dictionary<string, T> where T: class
    {
        protected bool initialized;

        new public T this[string key]
        {
            get
            {
                //lazy load on first access
                if (!this.ContainsKey(key) && !initialized)
                {
                    LoadValues();
                    initialized = true;
                }
                T value;
                return this.TryGetValue(key, out value) ? value : null;
            }
            set { /*disable setting values*/ }
        }

        protected abstract void LoadValues();
    }
}
