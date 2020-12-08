// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Resources.Models;
using azure_proto_core.Adapters;

namespace azure_proto_core
{
    /// <summary>
    ///     Operations for the RespourceGroups container in the given subscription context.  Allows Creating and listign
    ///     respource groups
    ///     and provides an attachment point for Collections of Tracked Resources.
    /// </summary>
    public class ResourceGroupContainerOperations : ResourceContainerOperations<XResourceGroup, PhResourceGroup>
    {
        internal ResourceGroupContainerOperations(ArmClientContext context, SubscriptionOperations subscription)
            : base(context, subscription.Id)
        {
        }

        internal ResourceGroupContainerOperations(ArmClientContext context, ResourceIdentifier id, ArmClientOptions clientOptions)
            : base(context, id, clientOptions)
        {
        }

        public override ResourceType ResourceType => "Microsoft.Resources/resourceGroups";

        internal ResourceGroupsOperations Operations =>
            GetClient((uri, cred) => new ResourcesManagementClient(uri, Id.Subscription, cred)).ResourceGroups;

        public ArmOperation<XResourceGroup> Create(string name, Location location)
        {
            var model = new PhResourceGroup(new ResourceGroup(location));

            return new PhArmOperation<XResourceGroup, ResourceGroup>(
                Operations.CreateOrUpdate(name, model),
                g => new XResourceGroup(ClientContext, new PhResourceGroup(g), ClientOptions));
        }

        public override ArmResponse<XResourceGroup> Create(
            string name,
            PhResourceGroup resourceDetails,
            CancellationToken cancellationToken = default)
        {
            var response = Operations.CreateOrUpdate(name, resourceDetails, cancellationToken);

            return new PhArmResponse<XResourceGroup, ResourceGroup>(
                response,
                g => new XResourceGroup(ClientContext, new PhResourceGroup(g), ClientOptions));
        }

        public override async Task<ArmResponse<XResourceGroup>> CreateAsync(
            string name,
            PhResourceGroup resourceDetails,
            CancellationToken cancellationToken = default)
        {
            var response = await Operations.CreateOrUpdateAsync(name, resourceDetails, cancellationToken)
                .ConfigureAwait(false);

            return new PhArmResponse<XResourceGroup, ResourceGroup>(
                response,
                g => new XResourceGroup(ClientContext, new PhResourceGroup(g), ClientOptions));
        }

        public override ArmOperation<XResourceGroup> StartCreate(
            string name,
            PhResourceGroup resourceDetails,
            CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<XResourceGroup, ResourceGroup>(
                Operations.CreateOrUpdate(name, resourceDetails, cancellationToken),
                g => new XResourceGroup(ClientContext, new PhResourceGroup(g), ClientOptions));
        }

        public override async Task<ArmOperation<XResourceGroup>> StartCreateAsync(
            string name,
            PhResourceGroup resourceDetails,
            CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<XResourceGroup, ResourceGroup>(
                await Operations.CreateOrUpdateAsync(name, resourceDetails, cancellationToken).ConfigureAwait(false),
                g => new XResourceGroup(ClientContext, new PhResourceGroup(g), ClientOptions));
        }

        public Pageable<XResourceGroup> List(CancellationToken cancellationToken = default)
        {
            return new PhWrappingPageable<ResourceGroup, XResourceGroup>(
                Operations.List(null, null, cancellationToken),
                s => new XResourceGroup(ClientContext, new PhResourceGroup(s), ClientOptions));
        }

        public AsyncPageable<XResourceGroup> ListAsync(CancellationToken cancellationToken = default)
        {
            return new PhWrappingAsyncPageable<ResourceGroup, XResourceGroup>(
                Operations.ListAsync(null, null, cancellationToken),
                s => new XResourceGroup(ClientContext, new PhResourceGroup(s), ClientOptions));
        }
        internal ResourceGroupsOperations Operations => GetClient<ResourcesManagementClient>((uri, cred) => new ResourcesManagementClient(uri, Id.Subscription, cred, ArmClientOptions.convert<ResourcesManagementClientOptions>(ClientOptions))).ResourceGroups;
    }
}
