namespace azure_proto_core
{
    using System;
    using System.Threading;
    using Azure;
    using Azure.ResourceManager.Resources;
    using azure_proto_core.Adapters;

    /// <summary>
    /// Subscription operations.
    /// </summary>
    public class SubscriptionOperations : OperationsBase
    {
        public static readonly string AzureResourceType = "Microsoft.Resources/subscriptions";

        public SubscriptionOperations(ArmClientContext context, string defaultSubscription)
            : base(context, $"/subscriptions/{defaultSubscription}") { }

        public SubscriptionOperations(ArmClientContext context, ResourceIdentifier id)
            : base(context, id) { }

        public SubscriptionOperations(ArmClientContext context, Resource subscription)
            : base(context, subscription) { }

        public override ResourceType ResourceType => AzureResourceType;

        public Pageable<ResourceGroupOperations> ListResourceGroups(CancellationToken cancellationToken = default(CancellationToken))
        {
            return new PhWrappingPageable<Azure.ResourceManager.Resources.Models.ResourceGroup, ResourceGroupOperations>(
                RgOperations.List(null, null, cancellationToken),
                s => new ResourceGroupOperations(ClientContext, new ResourceGroupData(s)));
        }

        public AsyncPageable<ResourceGroupOperations> ListResourceGroupsAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return new PhWrappingAsyncPageable<Azure.ResourceManager.Resources.Models.ResourceGroup, ResourceGroupOperations>(
                RgOperations.ListAsync(null, null, cancellationToken),
                s => new ResourceGroupOperations(ClientContext, new ResourceGroupData(s)));
        }

        public ResourceGroupOperations ResourceGroup(ResourceGroupData resourceGroup)
        {
            return new ResourceGroupOperations(ClientContext, resourceGroup);
        }

        public ResourceGroupOperations ResourceGroup(ResourceIdentifier resourceGroup)
        {
            return new ResourceGroupOperations(ClientContext, resourceGroup);
        }

        public ResourceGroupOperations ResourceGroup(string resourceGroup)
        {
            return new ResourceGroupOperations(this.ClientContext, $"{Id}/resourceGroups/{resourceGroup}");
        }

        public ResourceGroupContainerOperations ResourceGroups()
        {
            return new ResourceGroupContainerOperations(this.ClientContext, this);
        }

        internal SubscriptionsOperations SubscriptionsClient => GetClient<ResourcesManagementClient>((uri, cred) => new ResourcesManagementClient(uri, Guid.NewGuid().ToString(), cred)).Subscriptions;

        internal ResourceGroupsOperations RgOperations => GetClient<ResourcesManagementClient>((uri, cred) => new ResourcesManagementClient(uri, Id.Subscription, cred)).ResourceGroups;
    }
}
