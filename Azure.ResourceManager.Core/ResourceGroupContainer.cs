// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Threading;
using System.Threading.Tasks;
using Azure.ResourceManager.Core.Adapters;
using Azure.ResourceManager.Resources;

namespace Azure.ResourceManager.Core
{
    /// <summary>
    /// Operations for the RespourceGroups container in the given subscription context.  Allows Creating and listign respource groups
    /// and provides an attachment point for Collections of Tracked Resources.
    /// </summary>
    public class ResourceGroupContainer : ResourceContainerBase<ResourceGroup, ResourceGroupData>
    {
        internal ResourceGroupContainer(AzureResourceManagerClientOptions options, SubscriptionOperations subscription)
            : base(options, subscription.Id) { }

        internal ResourceGroupContainer(AzureResourceManagerClientOptions options, ResourceIdentifier id)
            : base(options, id) { }

        internal ResourceGroupsOperations Operations => GetClient<ResourcesManagementClient>((uri, cred) => new ResourcesManagementClient(
            uri,
            Id.Subscription,
            cred,
            ClientOptions.Convert<ResourcesManagementClientOptions>())).ResourceGroups;

        protected override ResourceType ValidResourceType => SubscriptionOperations.ResourceType;

        public ArmOperation<ResourceGroup> Create(string name, Location location)
        {
            var model = new ResourceGroupData(new ResourceManager.Resources.Models.ResourceGroup(location));
            return new PhArmOperation<ResourceGroup, Azure.ResourceManager.Resources.Models.ResourceGroup>(
                Operations.CreateOrUpdate(name, model),
                g => new ResourceGroup(ClientOptions, new ResourceGroupData(g)));
        }

        public override ArmResponse<ResourceGroup> Create(string name, ResourceGroupData resourceDetails)
        {
            var response = Operations.CreateOrUpdate(name, resourceDetails);
            return new PhArmResponse<ResourceGroup, ResourceManager.Resources.Models.ResourceGroup>(
                response,
                g => new ResourceGroup(ClientOptions, new ResourceGroupData(g)));
        }

        public async override Task<ArmResponse<ResourceGroup>> CreateAsync(string name, ResourceGroupData resourceDetails, CancellationToken cancellationToken = default)
        {
            var response = await Operations.CreateOrUpdateAsync(name, resourceDetails, cancellationToken).ConfigureAwait(false);
            return new PhArmResponse<ResourceGroup, ResourceManager.Resources.Models.ResourceGroup>(
                response,
                g => new ResourceGroup(ClientOptions, new ResourceGroupData(g)));
        }

        public override ArmOperation<ResourceGroup> StartCreate(string name, ResourceGroupData resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<ResourceGroup, ResourceManager.Resources.Models.ResourceGroup>(
                Operations.CreateOrUpdate(name, resourceDetails, cancellationToken),
                g => new ResourceGroup(ClientOptions, new ResourceGroupData(g)));
        }

        public async override Task<ArmOperation<ResourceGroup>> StartCreateAsync(string name, ResourceGroupData resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<ResourceGroup, ResourceManager.Resources.Models.ResourceGroup>(
                await Operations.CreateOrUpdateAsync(name, resourceDetails, cancellationToken).ConfigureAwait(false),
                g => new ResourceGroup(ClientOptions, new ResourceGroupData(g)));
        }

        public Pageable<ResourceGroup> List(CancellationToken cancellationToken = default)
        {
            return new PhWrappingPageable<ResourceManager.Resources.Models.ResourceGroup, ResourceGroup>(
                Operations.List(null, null, cancellationToken),
                s => new ResourceGroup(ClientOptions, new ResourceGroupData(s)));
        }

        public AsyncPageable<ResourceGroup> ListAsync(CancellationToken cancellationToken = default)
        {
            return new PhWrappingAsyncPageable<ResourceManager.Resources.Models.ResourceGroup, ResourceGroup>(
                Operations.ListAsync(null, null, cancellationToken),
                s => new ResourceGroup(ClientOptions, new ResourceGroupData(s)));
        }
    }
}
