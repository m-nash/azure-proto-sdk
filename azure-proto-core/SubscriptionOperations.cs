using Azure;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Resources.Models;
using azure_proto_core.Adapters;
using System;
using System.Threading;

namespace azure_proto_core
{
    /// <summary>
    /// Subscription operations
    /// TODO: Look at selecting subscriptions by name
    /// </summary>
    public class SubscriptionOperations : OperationsBase
    {
        public SubscriptionOperations(ArmClientContext parent, string defaultSubscription) : base(parent, $"/subscriptions/{defaultSubscription}") { }

        public SubscriptionOperations(ArmClientContext parent, ResourceIdentifier defaultSubscription) : base(parent, defaultSubscription) { }

        public SubscriptionOperations(ArmClientContext parent, Resource defaultSubscription) : base(parent, defaultSubscription) { }

        public override ResourceType ResourceType => "Microsoft.Resources/subscriptions";

        public Pageable<ResourceGroupOperations> ListResourceGroups(CancellationToken cancellationToken = default(CancellationToken))
        {
            return new PhWrappingPageable<ResourceGroup, ResourceGroupOperations>(RgOperations.List(null, null, cancellationToken), s => new ResourceGroupOperations(this, new PhResourceGroup(s)));
        }

        public AsyncPageable<ResourceGroupOperations> ListResourceGroupsAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return new PhWrappingAsyncPageable<ResourceGroup, ResourceGroupOperations>(RgOperations.ListAsync(null, null, cancellationToken), s => new ResourceGroupOperations(this, new PhResourceGroup(s)));
        }

        public ResourceGroupOperations ResourceGroup(PhResourceGroup resourceGroup)
        {
            return new ResourceGroupOperations(this, resourceGroup);
        }

        public ResourceGroupOperations ResourceGroup(ResourceIdentifier resourceGroup)
        {
            return new ResourceGroupOperations(this, resourceGroup);
        }

        public ResourceGroupOperations ResourceGroup(string resourceGroup)
        {
            return new ResourceGroupOperations(this, $"{Id}/resourceGroups/{resourceGroup}");
        }

        public ResourceGroupContainerOperations ResourceGroups()
        {
            return new ResourceGroupContainerOperations(this.ClientContext, this.Id);
        }

        internal SubscriptionsOperations SubscriptionsClient => GetClient<ResourcesManagementClient>((uri, cred) => new ResourcesManagementClient(uri, Guid.NewGuid().ToString(), cred)).Subscriptions;

        internal ResourceGroupsOperations RgOperations => GetClient<ResourcesManagementClient>((uri, cred) => new ResourcesManagementClient(uri, Id.Subscription, cred)).ResourceGroups;

        //TODO: Add back replacement for generic ListResource / ListLocations
    }
}
