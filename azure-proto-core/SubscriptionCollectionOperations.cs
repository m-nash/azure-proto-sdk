using Azure;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Resources.Models;
using azure_proto_core.Adapters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_core
{
    public class SubscriptionCollectionOperations : ArmOperations
    {
        public ResourceIdentifier DefaultSubscription { get; }
        public SubscriptionCollectionOperations(ArmOperations parent, string defaultSubscription) :base(parent)
        {
            DefaultSubscription = $"/subscriptions/{defaultSubscription}";
        }

        internal AsyncPageable<PhSubscriptionModel> ListSubscriptionsAsync(CancellationToken token = default(CancellationToken))
        {
            return new WrappingAsyncPageable<Subscription, PhSubscriptionModel>(SubscriptionsClient.ListAsync(token), s => new PhSubscriptionModel(s));
        }

        public Pageable<PhSubscriptionModel> ListSubscriptions(CancellationToken token = default(CancellationToken))
        {
            return new WrappingPageable<Subscription, PhSubscriptionModel>(SubscriptionsClient.List(token), s => new PhSubscriptionModel(s));
        }

        protected override ResourceType ResourceType => ResourceType.None;

        public SubscriptionsOperations SubscriptionsClient => GetClient<ResourcesManagementClient>((uri, cred) => new ResourcesManagementClient(uri, Guid.NewGuid().ToString(), cred)).Subscriptions;

        public ResourceGroupContainerOperations ResourceGroups() => ResourceGroups(DefaultSubscription);

        public ResourceGroupContainerOperations ResourceGroups(ResourceIdentifier sub) => new ResourceGroupContainerOperations(this, sub);
        public ResourceGroupContainerOperations ResourceGroups(Resource sub) => new ResourceGroupContainerOperations(this, sub);
    }


}
