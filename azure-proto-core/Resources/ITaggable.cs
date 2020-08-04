using System.Collections.Generic;

namespace azure_proto_core
{
    public interface ITaggable
    {
        public IDictionary<string, string> Tags { get; }
    }
}
