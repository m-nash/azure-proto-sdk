// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using Azure.Core;

namespace Azure.ResourceManager.Core
{
    /// <summary>
    ///     TODO: split this into a base class for all Operations, and a base class for specific operations
    /// </summary>
    public abstract class OperationsBase
    {
        public OperationsBase(AzureResourceManagerClientOptions options, ResourceIdentifier id, Location location = null)
        {
            ClientOptions = options;
            Id = id;
            DefaultLocation = location ?? Location.Default;

            Validate(id);
        }

        public OperationsBase(AzureResourceManagerClientOptions options, Resource resource)
            : this(options, resource?.Id, (resource as TrackedResource)?.Location)
        {
            Resource = resource;
        }

        public virtual AzureResourceManagerClientOptions ClientOptions { get; }

        public virtual Location DefaultLocation { get; }

        public virtual ResourceIdentifier Id { get; }

        public virtual void Validate(ResourceIdentifier identifier)
        {
            if (identifier?.Type != GetValidResourceType())
                throw new InvalidOperationException($"Invalid resource type {identifier?.Type} expected {GetValidResourceType()}");
        }

        /// <summary>
        ///     Note that this is currently adapting to underlying management clients - once generator changes are in, this would
        ///     likely be unnecessary
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="creator"></param>
        /// <returns></returns>
        public T GetClient<T>(Func<Uri, TokenCredential, T> creator)
        {
            return ClientOptions.GetClient(creator);
        }

        protected virtual Resource Resource { get; set; }

        protected internal abstract ResourceType GetValidResourceType();
    }
}
