// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Threading;
using Azure;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Resources.Models;
using azure_proto_core.Adapters;

namespace azure_proto_core
{
    /// <summary>
    ///     Subscription operations
    /// </summary>
    public class SubscriptionOperations : OperationsBase
    {
        public static readonly string AzureResourceType = "Microsoft.Resources/subscriptions";

        internal SubscriptionOperations(ArmClientContext context, string defaultSubscription, ArmClientOptions clientOptions)
            : base(context, $"/subscriptions/{defaultSubscription}", clientOptions)
        {
        }

        internal SubscriptionOperations(ArmClientContext context, ResourceIdentifier id, ArmClientOptions clientOptions)
            : base(context, id, clientOptions)
        {
        }

        internal SubscriptionOperations(ArmClientContext context, Resource subscription, ArmClientOptions clientOptions)
            : base(context, subscriptionm clientOptions)
        {
        }

        public override ResourceType ResourceType => AzureResourceType;

        internal SubscriptionsOperations SubscriptionsClient =>
            GetClient((uri, cred) => new ResourcesManagementClient(uri, Guid.NewGuid().ToString(), cred)).Subscriptions;

        internal ResourceGroupsOperations RgOperations =>
            GetClient((uri, cred) => new ResourcesManagementClient(uri, Id.Subscription, cred)).ResourceGroups;

        public Pageable<ResourceGroupOperations> ListResourceGroups(CancellationToken cancellationToken = default)
        {
            return new PhWrappingPageable<ResourceGroup, ResourceGroupOperations>(
                RgOperations.List(null, null, cancellationToken),
                s => new ResourceGroupOperations(ClientContext, new PhResourceGroup(s), ClientOptions));
        }

        public AsyncPageable<ResourceGroupOperations> ListResourceGroupsAsync(
            CancellationToken cancellationToken = default)
        {
            return new PhWrappingAsyncPageable<ResourceGroup, ResourceGroupOperations>(
                RgOperations.ListAsync(null, null, cancellationToken),
                s => new ResourceGroupOperations(ClientContext, new PhResourceGroup(s), ClientOptions));
        }

        public ResourceGroupOperations ResourceGroup(PhResourceGroup resourceGroup)
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

        public ResourceGroupContainerOperations ResourceGroups()
        {
            return new ResourceGroupContainerOperations(ClientContext, this, ClientOptions);
        }
        internal SubscriptionsOperations SubscriptionsClient => GetClient<ResourcesManagementClient>((uri, cred) => new ResourcesManagementClient(uri, Guid.NewGuid().ToString(), cred, 
                    ArmClientOptions.convert<ResourcesManagementClientOptions>(ClientOptions))).Subscriptions;
        internal ResourceGroupsOperations RgOperations => GetClient<ResourcesManagementClient>((uri, cred) => new ResourcesManagementClient(uri, Id.Subscription, cred, 
                    ArmClientOptions.convert<ResourcesManagementClientOptions>(ClientOptions))).ResourceGroups;
    }
}
