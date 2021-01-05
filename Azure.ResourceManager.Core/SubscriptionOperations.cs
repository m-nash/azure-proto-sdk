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
        public static readonly ResourceType ResourceType = "Microsoft.Resources/subscriptions";

        internal SubscriptionOperations(AzureResourceManagerClientOptions options, string defaultSubscription)
            : base(options, $"/subscriptions/{defaultSubscription}")
        {
        }

        internal SubscriptionOperations(AzureResourceManagerClientOptions options, ResourceIdentifier id)
            : base(options, id)
        {
        }

        internal SubscriptionOperations(AzureResourceManagerClientOptions options, Resource subscription )
            : base(options, subscription)
        {
        }

        internal SubscriptionsOperations SubscriptionsClient => GetClient<ResourcesManagementClient>((uri, cred) =>
            new ResourcesManagementClient(uri, Guid.NewGuid().ToString(), cred, ClientOptions.Convert<ResourcesManagementClientOptions>())).Subscriptions;

        internal ResourceGroupsOperations RgOperations => GetClient<ResourcesManagementClient>((uri, cred) =>
            new ResourcesManagementClient(uri, Id.Subscription, cred, ClientOptions.Convert<ResourcesManagementClientOptions>())).ResourceGroups;

        public Pageable<ResourceGroupOperations> ListResourceGroups(CancellationToken cancellationToken = default)
        {
            return new PhWrappingPageable<Azure.ResourceManager.Resources.Models.ResourceGroup, ResourceGroupOperations>(
                RgOperations.List(null, null, cancellationToken),
                s => new ResourceGroupOperations(ClientOptions, new ResourceGroupData(s)));
        }

        public AsyncPageable<ResourceGroupOperations> ListResourceGroupsAsync(
            CancellationToken cancellationToken = default)
        {
            return new PhWrappingAsyncPageable<Azure.ResourceManager.Resources.Models.ResourceGroup, ResourceGroupOperations>(
                RgOperations.ListAsync(null, null, cancellationToken),
                s => new ResourceGroupOperations(ClientOptions, new ResourceGroupData(s)));
        }

        public ResourceGroupOperations ResourceGroup(ResourceGroupData resourceGroup)
        {
            return new ResourceGroupOperations(ClientOptions, resourceGroup);
        }

        public ResourceGroupOperations ResourceGroup(ResourceIdentifier resourceGroup)
        {
            return new ResourceGroupOperations(ClientOptions, resourceGroup);
        }

        public ResourceGroupOperations ResourceGroup(string resourceGroup)
        {
            return new ResourceGroupOperations(ClientOptions, $"{Id}/resourceGroups/{resourceGroup}");
        }

        public ResourceGroupContainer ResourceGroups()
        {
            return new ResourceGroupContainer(ClientOptions, this);
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
            return s => new Subscription(ClientOptions, new SubscriptionData(s));
        }
    }
}
