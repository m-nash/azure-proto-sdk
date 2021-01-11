// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using Azure.Core;

namespace Azure.ResourceManager.Core
{
    /// <summary>
    /// Base class for resource operations.
    /// </summary>
    public abstract class OperationsBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperationsBase"/> class.
        /// </summary>
        /// <param name="options"> The client parameters to use in these operations. </param>
        /// <param name="id"> The identifier of the resource that is the target of operations. </param>
        /// <param name="location"> The location for the Azure resource. </param>
        protected OperationsBase(AzureResourceManagerClientOptions options, ResourceIdentifier id, Location location = null)
        {
            ClientOptions = options;
            Id = id;
            DefaultLocation = location ?? Location.Default;

            Validate(id);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationsBase"/> class.
        /// </summary>
        /// <param name="options"> The client parameters to use in these operations. </param>
        /// <param name="resource"> The Azure resource that is the target of operations. </param>
        protected OperationsBase(AzureResourceManagerClientOptions options, Resource resource)
            : this(options, resource?.Id, (resource as TrackedResource)?.Location)
        {
            Resource = resource;
        }

        /// <summary>
        /// Gets the Azure Resource Manager client options.
        /// </summary>
        public virtual AzureResourceManagerClientOptions ClientOptions { get; }

        /// <summary>
        /// Gets the default location.
        /// </summary>
        public virtual Location DefaultLocation { get; }

        /// <summary>
        /// Gets the resource identifier.
        /// </summary>
        public virtual ResourceIdentifier Id { get; }

        /// <summary>
        /// Gets or sets the Azure resource.
        /// </summary>
        protected virtual Resource Resource { get; set; }

        /// <summary>
        /// Validate the resource identifier against current operations.
        /// </summary>
        /// <param name="identifier"> The resource identifier. </param>
        public virtual void Validate(ResourceIdentifier identifier)
        {
            if (identifier?.Type != ValidResourceType)
                throw new InvalidOperationException($"Invalid resource type {identifier?.Type} expected {ValidResourceType}");
        }

        /// <summary>
        /// Gets the valid Azure resource type for the current operations.
        /// </summary>
        /// <returns> A valid Azure resource type. </returns>
        protected abstract ResourceType ValidResourceType { get; }

        /// <summary>
        ///    Gets the client for specific azure resource types.
        /// </summary>
        /// <typeparam name="T"> The type of the operations class for a specific resource. </typeparam>
        /// <param name="creator"> The client creation function. </param>
        /// <returns> An instance of client for a given resource type. </returns>
        public T GetClient<T>(Func<Uri, TokenCredential, T> creator)
        {
            // TODO: Anyway to make this protected internal? It is used in Extensions
            return ClientOptions.GetClient(creator);
        }
    }
}
