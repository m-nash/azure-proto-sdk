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
            : this(options, new ArmResourceData(id, location ?? Location.Default))
        {
        }

        public OperationsBase(AzureResourceManagerClientOptions options, Resource resource)
        {
            Validate(resource?.Id);

            ClientOptions = options;
            Id = resource.Id;
            var trackedResource = resource as TrackedResource;
            DefaultLocation = trackedResource?.Location ?? Location.Default;
            Resource = resource;
        }

        public virtual AzureResourceManagerClientOptions ClientOptions { get; }

        protected virtual Resource Resource { get; set; }

        public virtual ResourceType ResourceType { get; }

        public virtual Location DefaultLocation { get; }

        public virtual ResourceIdentifier Id { get; }

        public virtual void Validate(ResourceIdentifier identifier)
        {
            if ((SubscriptionContainer.AzureResourceType != ResourceType && identifier == null) || (identifier != null && identifier?.Type != ResourceType))
            {
                throw new InvalidOperationException($"{identifier} is not a valid resource of type {ResourceType}");
            }
        }

        /// <summary>
        ///     Note that this is currently adapting to underlying management clients - once generator changes are in, this would
        ///     likely be unnecessary
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="creator"></param>
        /// <returns></returns>
        protected T GetClient<T>(Func<Uri, TokenCredential, T> creator)
        {
            return ClientOptions.GetClient(creator);
        }
    }
}
