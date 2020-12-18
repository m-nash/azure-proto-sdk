// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Threading;
using Azure.ResourceManager.Core.Adapters;
using Azure.ResourceManager.Resources;
using System.Threading.Tasks;

namespace Azure.ResourceManager.Core
{
    /// <summary>
    ///     Subscription operations
    /// </summary>
    public class SubscriptionOperations : ResourceOperationsBase<Subscription>
    {
        public static readonly ResourceType AzureResourceType = "Microsoft.Resources/subscriptions";

        internal SubscriptionOperations(AzureResourceManagerClientContext context, string defaultSubscription, AzureResourceManagerClientOptions clientOptions)
            : base(context, $"/subscriptions/{defaultSubscription}", clientOptions)
        {
        }

        internal SubscriptionOperations(AzureResourceManagerClientContext context, ResourceIdentifier id, AzureResourceManagerClientOptions clientOptions)
            : base(context, id, clientOptions)
        {
        }

        internal SubscriptionOperations(AzureResourceManagerClientContext context, Resource subscription, AzureResourceManagerClientOptions clientOptions)
            : base(context, subscription, clientOptions)
        {
        }

        public override ResourceType ResourceType => AzureResourceType;

        internal SubscriptionsOperations SubscriptionsClient => GetClient<ResourcesManagementClient>((uri, cred) =>
            new ResourcesManagementClient(uri, Guid.NewGuid().ToString(), cred, AzureResourceManagerClientOptions.Convert<ResourcesManagementClientOptions>(ClientOptions))).Subscriptions;

        internal ResourceGroupsOperations RgOperations => GetClient<ResourcesManagementClient>((uri, cred) =>
            new ResourcesManagementClient(uri, Id.Subscription, cred, AzureResourceManagerClientOptions.Convert<ResourcesManagementClientOptions>(ClientOptions))).ResourceGroups;

        public Pageable<ResourceGroupOperations> ListResourceGroups(CancellationToken cancellationToken = default)
        {
            return new PhWrappingPageable<Azure.ResourceManager.Resources.Models.ResourceGroup, ResourceGroupOperations>(
                RgOperations.List(null, null, cancellationToken),
                s => new ResourceGroupOperations(ClientContext, new ResourceGroupData(s), ClientOptions));
        }

        public AsyncPageable<ResourceGroupOperations> ListResourceGroupsAsync(
            CancellationToken cancellationToken = default)
        {
            return new PhWrappingAsyncPageable<Azure.ResourceManager.Resources.Models.ResourceGroup, ResourceGroupOperations>(
                RgOperations.ListAsync(null, null, cancellationToken),
                s => new ResourceGroupOperations(ClientContext, new ResourceGroupData(s), ClientOptions));
        }

        public ResourceGroupOperations ResourceGroup(ResourceGroupData resourceGroup)
        {
            return new ResourceGroupOperations(ClientContext, resourceGroup, ClientOptions);
        }

        public ResourceGroupOperations ResourceGroup(ResourceIdentifier resourceGroup)
        {
            return new ResourceGroupOperations(ClientContext, resourceGroup, ClientOptions);
        }

        public ResourceGroupOperations ResourceGroup(string resourceGroup)
        {
            return new ResourceGroupOperations(ClientContext, $"{Id}/resourceGroups/{resourceGroup}", ClientOptions);
        }

        public ResourceGroupContainer ResourceGroups()
        {
            return new ResourceGroupContainer(ClientContext, this, ClientOptions);
        }

        public override ArmResponse<Subscription> Get()
        {
            return new PhArmResponse<Subscription, Azure.ResourceManager.Resources.Models.Subscription>(
                SubscriptionsClient.Get(Id.Name),
                Converter());
        }

        public async override Task<ArmResponse<Subscription>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhArmResponse<Subscription, Azure.ResourceManager.Resources.Models.Subscription>(
                await SubscriptionsClient.GetAsync(Id.Name, cancellationToken),
                Converter());
        }

        private Func<Azure.ResourceManager.Resources.Models.Subscription, Subscription> Converter()
        {
            return s => new Subscription(ClientContext, new SubscriptionData(s), ClientOptions);
        }
    }
}
