// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Resources.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.ResourceManager.Core
{
    /// <summary>
    /// A class representing the operations that can be performed over a specific ResourceGroup.
    /// </summary>
    public class ResourceGroupOperations : ResourceOperationsBase<ResourceGroup>,
        ITaggableResource<ResourceGroup>, IDeletableResource
    {
        /// <summary>
        /// Gets the resource type definition for a ResourceType.
        /// </summary>
        public static readonly ResourceType ResourceType = "Microsoft.Resources/resourceGroups";

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceGroupOperations"/> class.
        /// </summary>
        /// <param name="options"> The client parameters to use in these operations. </param>
        /// <param name="id"> The identifier of the resource that is the target of operations. </param>
        internal ResourceGroupOperations(AzureResourceManagerClientOptions options, ResourceIdentifier id)
            : base(options, id)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceGroupOperations"/> class.
        /// </summary>
        /// <param name="options"> The client parameters to use in these operations. </param>
        /// <param name="resource"> The resource that is the target of operations. </param>
        internal ResourceGroupOperations(AzureResourceManagerClientOptions options, Resource resource)
            : base(options, resource)
        {
        }

        /// <inheritdoc/>
        protected override ResourceType ValidResourceType => ResourceType;

        private ResourceGroupsOperations Operations => GetClient<ResourcesManagementClient>((uri, creds) => new ResourcesManagementClient(
            uri,
            Id.Subscription,
            creds,
            ClientOptions.Convert<ResourcesManagementClientOptions>()))?.ResourceGroups;

        /// <summary>
        /// When you delete a resource group, all of its resources are also deleted. Deleting a resource group deletes all of its template deployments and currently stored operations.
        /// </summary>
        /// <returns> A response with the <see cref="ArmResponse{Response}"/> operation for this resource. </returns>
        public ArmResponse<Response> Delete()
        {
            return new ArmResponse(Operations.StartDelete(Id.Name).WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult());
        }

        /// <summary>
        /// When you delete a resource group, all of its resources are also deleted. Deleting a resource group deletes all of its template deployments and currently stored operations.
        /// </summary>
        /// <param name="cancellationToken"> A token to allow the caller to cancel the call to the service. The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param>
        /// <returns> A <see cref="Task"/> that on completion returns a response with the <see cref="ArmResponse{Response}"/> operation for this resource. </returns>
        public async Task<ArmResponse<Response>> DeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmResponse(await Operations.StartDelete(Id.Name).WaitForCompletionAsync());
        }

        /// <summary>
        /// When you delete a resource group, all of its resources are also deleted. Deleting a resource group deletes all of its template deployments and currently stored operations.
        /// </summary>
        /// <param name="cancellationToken"> A token to allow the caller to cancel the call to the service. The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param>
        /// <returns> A response with the <see cref="ArmOperation{Response}"/> operation for this resource. </returns>
        /// <remarks>
        /// <see href="https://azure.github.io/azure-sdk/dotnet_introduction.html#dotnet-longrunning">Details on long running operation object.</see>
        /// </remarks>
        public ArmOperation<Response> StartDelete(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(Operations.StartDelete(Id.Name, cancellationToken));
        }

        /// <summary>
        /// When you delete a resource group, all of its resources are also deleted. Deleting a resource group deletes all of its template deployments and currently stored operations.
        /// </summary>
        /// <param name="cancellationToken"> A token to allow the caller to cancel the call to the service. The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param>
        /// <returns> A <see cref="Task"/> that on completion returns a response with the <see cref="ArmResponse{Response}"/> operation for this resource. </returns>
        /// <remarks>
        /// <see href="https://azure.github.io/azure-sdk/dotnet_introduction.html#dotnet-longrunning">Details on long running operation object.</see>
        /// </remarks>
        public async Task<ArmOperation<Response>> StartDeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(await Operations.StartDeleteAsync(Id.Name, cancellationToken));
        }

        /// <inheritdoc/>
        public override ArmResponse<ResourceGroup> Get()
        {
            return new PhArmResponse<ResourceGroup, Azure.ResourceManager.Resources.Models.ResourceGroup>(Operations.Get(Id.Name), g =>
            {
                Resource = new ResourceGroupData(g);
                return new ResourceGroup(ClientOptions, Resource as ResourceGroupData);
            });
        }

        /// <inheritdoc/>
        public async override Task<ArmResponse<ResourceGroup>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhArmResponse<ResourceGroup, Azure.ResourceManager.Resources.Models.ResourceGroup>(await Operations.GetAsync(Id.Name, cancellationToken), g =>
            {
                Resource = new ResourceGroupData(g);
                return new ResourceGroup(ClientOptions, Resource as ResourceGroupData);
            });
        }

        /// <summary>
        /// Add a tag to a ResourceGroup.
        /// If the tag already exists it will be modified.
        /// </summary>
        /// <param name="name"> The key for the tag. </param>
        /// <param name="value"> The value for the tag. </param>
        /// <returns> A response with the <see cref="ArmOperation{ResourceGroup}"/> operation for this resource. </returns>
        /// <remarks>
        /// <see href="https://azure.github.io/azure-sdk/dotnet_introduction.html#dotnet-longrunning">Details on long running operation object.</see>
        /// </remarks>
        public ArmOperation<ResourceGroup> StartAddTag(string name, string value)
        {
            var patch = new ResourceGroupPatchable();
            patch.Tags[name] = value;
            return new PhArmOperation<ResourceGroup, Azure.ResourceManager.Resources.Models.ResourceGroup>(Operations.Update(Id.Name, patch), g =>
            {
                Resource = new ResourceGroupData(g);
                return new ResourceGroup(ClientOptions, Resource as ResourceGroupData);
            });
        }

        /// <summary>
        /// Add a tag to a ResourceGroup.
        /// If the tag already exists it will be modified.
        /// </summary>
        /// <param name="name"> The key for the tag. </param>
        /// <param name="value"> The value for the tag. </param>
        /// <returns> A <see cref="Task"/> that on completion returns a response with the <see cref="ArmOperation{ResourceGroup}"/> operation for this resource. </returns>
        /// <remarks>
        /// <see href="https://azure.github.io/azure-sdk/dotnet_introduction.html#dotnet-longrunning">Details on long running operation object.</see>
        /// </remarks>
        public async Task<ArmOperation<ResourceGroup>> StartAddTagAsync(string name, string value, CancellationToken cancellationToken = default)
        {
            var patch = new ResourceGroupPatchable();
            patch.Tags[name] = value;
            return new PhArmOperation<ResourceGroup, Azure.ResourceManager.Resources.Models.ResourceGroup>(await Operations.UpdateAsync(Id.Name, patch, cancellationToken), g =>
            {
                Resource = new ResourceGroupData(g);
                return new ResourceGroup(ClientOptions, Resource as ResourceGroupData);
            });
        }

        /// <summary>
        /// Create a resource with a ResourceGroupOperations.
        /// </summary>
        /// <param name="name"> A string representing the name of the resource />. </param>
        /// <typeparam name="TResource"> The type of the class containing properties for the underlying resource. </typeparam>
        /// <param name="location"> A Location of where to to host the resource. />. </param>
        /// <returns> Returns a response with the <see cref="ArmResponse{TOperations}"/> operation for this resource. </returns>
        public ArmResponse<TOperations> CreateResource<TContainer, TOperations, TResource>(string name, TResource model, LocationData location = default)
            where TResource : TrackedResource
            where TOperations : ResourceOperationsBase<TOperations>
            where TContainer : ResourceContainerBase<TOperations, TResource>
        {
            var myResource = Resource as TrackedResource;

            if (myResource == null)
            {
                myResource = new ArmResourceData(Id);
            }

            if (location != null)
            {
                myResource = new ArmResourceData(Id, location);
            }

            TContainer container = Activator.CreateInstance(typeof(TContainer), ClientOptions, myResource) as TContainer;

            return container.Create(name, model);
        }

        /// <summary>
        /// Create a resource with a ResourceGroupOperations.
        /// </summary>
        /// <param name="name"> A string representing the name of the resource />. </param>
        /// <typeparam name="TResource"> The type of the class containing properties for the underlying resource. </typeparam>
        /// <param name="location"> A Location of where to to host the resource. />. </param>
        /// <returns> A <see cref="Task"/> that on completion returns a response with the <see cref="ArmResponse{TOperations}"/> operation for this resource. </returns>
        public Task<ArmResponse<TOperations>> CreateResourceAsync<TContainer, TOperations, TResource>(string name, TResource model, LocationData location = default, CancellationToken token = default)
            where TResource : TrackedResource
            where TOperations : ResourceOperationsBase<TOperations>
            where TContainer : ResourceContainerBase<TOperations, TResource>
        {
            var myResource = Resource as TrackedResource;

            if (myResource == null)
            {
                myResource = new ArmResourceData(Id);
            }

            if (location != null)
            {
                myResource = new ArmResourceData(Id, location);
            }

            TContainer container = Activator.CreateInstance(typeof(TContainer), ClientOptions, myResource) as TContainer;

            return container.CreateAsync(name, model, token);
        }

        /// <summary>
        /// Lists all available geo-locations.
        /// </summary>
        /// <param name="cancellationToken"> A token to allow the caller to cancel the call to the service. The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param>
        /// <returns> A collection of location that may take multiple service requests to iterate over. </returns>
        public IEnumerable<LocationData> ListAvailableLocations(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetResourcesClient(Id.Subscription).Providers.List(expand: "metadata", cancellationToken: cancellationToken)
                .FirstOrDefault(p => string.Equals(p.Namespace, ResourceType?.Namespace, StringComparison.InvariantCultureIgnoreCase))
                .ResourceTypes.FirstOrDefault(r => ResourceType.Equals(r.ResourceType))
                .Locations.Cast<LocationData>();
        }

        /// <summary>
        /// Lists all available geo-locations.
        /// </summary>
        /// <param name="cancellationToken"> A token to allow the caller to cancel the call to the service. The default value is <see cref="P:System.Threading.CancellationToken.None" />. </param>
        /// <returns> An async collection of location that may take multiple service requests to iterate over. </returns>
        /// <exception cref="InvalidOperationException"> The default subscription id is null. </exception>
        public async IAsyncEnumerable<LocationData> ListAvailableLocationsAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            if (Id.Subscription == null)
            {
                throw new InvalidOperationException("Please select a default subscription");
            }

            await foreach (var provider in GetResourcesClient(Id.Subscription).Providers.ListAsync(expand: "metadata", cancellationToken: cancellationToken).WithCancellation(cancellationToken))
            {
                if (string.Equals(provider.Namespace, ResourceType?.Namespace, StringComparison.InvariantCultureIgnoreCase))
                {
                    var foundResource = provider.ResourceTypes.FirstOrDefault(p => ResourceType.Equals(p.ResourceType));
                    foreach (var location in foundResource.Locations)
                    {
                        yield return location;
                    }
                }
            }
        }
    }
}
