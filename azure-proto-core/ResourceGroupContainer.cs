// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.ResourceManager.Resources;
using azure_proto_core.Adapters;


namespace azure_proto_core
{
    /// <summary>
    /// Operations for the RespourceGroups container in the given subscription context.  Allows Creating and listign respource groups
    /// and provides an attachment point for Collections of Tracked Resources.
    /// </summary>
    public class ResourceGroupContainer : ResourceContainer<ResourceGroup, ResourceGroupData>
    {
        internal ResourceGroupContainer(ArmClientContext context, SubscriptionOperations subscription, ArmClientOptions clientOptions)
            : base(context, subscription.Id, clientOptions) { }

        internal ResourceGroupContainer(ArmClientContext context, ResourceIdentifier id, ArmClientOptions clientOptions)
            : base(context, id, clientOptions) { }
        public override ResourceType ResourceType => "Microsoft.Resources/resourceGroups";

        internal ResourceGroupsOperations Operations => GetClient<ResourcesManagementClient>((uri, cred) => new ResourcesManagementClient(uri, Id.Subscription, cred,
            ArmClientOptions.Convert<ResourcesManagementClientOptions>(ClientOptions))).ResourceGroups;

        public ArmOperation<ResourceGroup> Create(string name, Location location)
        {
            var model = new ResourceGroupData(new Azure.ResourceManager.Resources.Models.ResourceGroup(location));
            return new PhArmOperation<ResourceGroup, Azure.ResourceManager.Resources.Models.ResourceGroup>(
                Operations.CreateOrUpdate(name, model),
                g => new ResourceGroup(ClientContext, new ResourceGroupData(g), ClientOptions));
        }

        public override ArmResponse<ResourceGroup> Create(string name, ResourceGroupData resourceDetails, CancellationToken cancellationToken = default)
        {
            var response = Operations.CreateOrUpdate(name, resourceDetails, cancellationToken);
            return new PhArmResponse<ResourceGroup, Azure.ResourceManager.Resources.Models.ResourceGroup>(
                response,
                g => new ResourceGroup(ClientContext, new ResourceGroupData(g), ClientOptions));
        }

        public async override Task<ArmResponse<ResourceGroup>> CreateAsync(string name, ResourceGroupData resourceDetails, CancellationToken cancellationToken = default)
        {
            var response = await Operations.CreateOrUpdateAsync(name, resourceDetails, cancellationToken).ConfigureAwait(false);
            return new PhArmResponse<ResourceGroup, Azure.ResourceManager.Resources.Models.ResourceGroup>(
                response,
                g => new ResourceGroup(ClientContext, new ResourceGroupData(g), ClientOptions));
        }

        public override ArmOperation<ResourceGroup> StartCreate(string name, ResourceGroupData resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<ResourceGroup, Azure.ResourceManager.Resources.Models.ResourceGroup>(
                Operations.CreateOrUpdate(name, resourceDetails, cancellationToken),
                g => new ResourceGroup(ClientContext, new ResourceGroupData(g), ClientOptions));
        }

        public async override Task<ArmOperation<ResourceGroup>> StartCreateAsync(string name, ResourceGroupData resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<ResourceGroup, Azure.ResourceManager.Resources.Models.ResourceGroup>(
                await Operations.CreateOrUpdateAsync(name, resourceDetails, cancellationToken).ConfigureAwait(false),
                g => new ResourceGroup(ClientContext, new ResourceGroupData(g), ClientOptions));
        }

        public Pageable<ResourceGroup> List(CancellationToken cancellationToken = default)
        {
            return new PhWrappingPageable<Azure.ResourceManager.Resources.Models.ResourceGroup, ResourceGroup>(
                Operations.List(null, null, cancellationToken),
                s => new ResourceGroup(ClientContext, new ResourceGroupData(s), ClientOptions));
        }

        public AsyncPageable<ResourceGroup> ListAsync(CancellationToken cancellationToken = default)
        {
            return new PhWrappingAsyncPageable<Azure.ResourceManager.Resources.Models.ResourceGroup, ResourceGroup>(
                Operations.ListAsync(null, null, cancellationToken),
                s => new ResourceGroup(ClientContext, new ResourceGroupData(s), ClientOptions));
        }
    }
}