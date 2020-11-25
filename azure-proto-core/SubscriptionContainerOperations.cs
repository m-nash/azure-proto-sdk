using Azure;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Resources.Models;
using azure_proto_core.Adapters;
using System;
using System.Threading;

namespace azure_proto_core
{
    /// <summary>
    /// Subscription Container Operations
    /// </summary>
    public class SubscriptionContainerOperations : OperationsBase
    {
        public static readonly string AzureResourceType = "Microsoft.Resources/subscriptions";

        public SubscriptionContainerOperations(ArmClientContext context, string defaultSubscription) : base(context, $"/subscriptions/{defaultSubscription}") { }

        public SubscriptionContainerOperations(ArmClientContext context, ResourceIdentifier id) : base(context, id) { }

        public SubscriptionContainerOperations(ArmClientContext context, Resource subscription) : base(context, subscription) { }

        public override ResourceType ResourceType => AzureResourceType;

        public Pageable<SubscriptionOperations> List(CancellationToken cancellationToken = default)
        {
            return new PhWrappingPageable<Subscription, SubscriptionOperations>(
                Operations.List(cancellationToken),
                this.convertor());
        }

        public AsyncPageable<SubscriptionOperations> ListAsync(CancellationToken cancellationToken = default)
        {
            return new PhWrappingAsyncPageable<Subscription, SubscriptionOperations>(
                Operations.ListAsync(cancellationToken),
                this.convertor());
        }

        private Func<Subscription, SubscriptionOperations> convertor()
        {
            return s => new SubscriptionOperations(ClientContext, new PhSubscriptionModel(s));
        }

        internal SubscriptionsOperations Operations => GetClient<ResourcesManagementClient>((uri, cred) => new ResourcesManagementClient(uri, Guid.NewGuid().ToString(), cred)).Subscriptions;
    }
}
