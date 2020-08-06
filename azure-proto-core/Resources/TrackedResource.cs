using System;
using System.Collections.Generic;

namespace azure_proto_core
{
    /// <summary>
    /// Generic representation of a tracked resource.  All tracked resources should extend this class
    /// </summary>
    public abstract class TrackedResource: Resource
    {
        public virtual IDictionary<string, string> Tags => new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

        public virtual Location Location { get; protected set; }
    }
}
