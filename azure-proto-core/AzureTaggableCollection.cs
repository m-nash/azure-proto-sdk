using System.Collections.Generic;

namespace azure_proto_core
{
    public abstract class AzureTaggableCollection<T, Ph> : AzureCollection<T>
        where T: AzureEntity<Ph>
        where Ph: TrackedResource, ITaggable
    {
        protected AzureTaggableCollection(TrackedResource parent) : base(parent) { }

        public IEnumerable<T> GetItemsByTag(string key, string value)
        {
            foreach (var entity in this)
            {
                string curValue = null;
                var exists = entity.Tags?.TryGetValue(key, out curValue);
                if (exists.HasValue && exists.Value && curValue == value)
                    yield return entity;
            }
        }
    }
}
