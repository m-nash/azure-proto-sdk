using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_core
{
    /// <summary>
    /// TODO: GENERATOR Remove this class after generation updates
    /// </summary>
    /// <typeparam name="T">The type of the underlying model this class wraps</typeparam>
    public abstract class TrackedResource<T> : TrackedResource where T : class
    {
        protected TrackedResource(ResourceIdentifier id, Location location, T data)
        {
            Id = id;
            Location = location;
            Model = data;
        }

        public virtual T Model { get; set; }

        public static implicit operator T(TrackedResource<T> other)
        {
            return other.Model;
        }
    }
}
