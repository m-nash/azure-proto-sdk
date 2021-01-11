// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.ResourceManager.Core
{
    /// <summary>
    ///     TODO: GENERATOR Remove this class after generation updates
    /// </summary>
    /// <typeparam name="T">The type of the underlying model this class wraps</typeparam>
    public abstract class TrackedResource<T> : TrackedResource
        where T : class
    {
        protected TrackedResource(ResourceIdentifier id, Location location, T data)
        {
            Id = id;
            Location = location;
            Model = data;
        }

        protected TrackedResource(string id, Location location, T data)
        {
            if(object.ReferenceEquals(id, null)){
                Id = null;
            }
            else{
                Id = id;
            }
            Location = location;
            Model = data;
        }

        public virtual T Model { get; set; }

        public static implicit operator T(TrackedResource<T> other)
        {
            return other.Model;
        }
    }

    //Or call generic resource, other resource??
    public abstract class ProxyResource<T> : Resource
        where T : class
    {
        protected ProxyResource(ResourceIdentifier id, T data)
        {
            Id = id;
            Model = data;
        }

        public override ResourceIdentifier Id { get; protected set; }

        public virtual T Model { get; set; }

        public static implicit operator T(ProxyResource<T> other)
        {
            return other.Model;
        }
    }
}
