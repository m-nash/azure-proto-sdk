using Azure;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Resources.Models;
using azure_proto_core.Adapters;
using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;

namespace azure_proto_core
{
    /// <summary>
    /// Subscription Container Operationss
    /// </summary>
    public class SubscriptionContainerOperations : OperationsBase
    {
        public static readonly string AzureResourceType = "Microsoft.Resources/subscriptions";

        public SubscriptionContainerOperations(ArmClientContext context, ArmClientOptions options) : base(context, AzureResourceType, null, options) { }

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

        internal async Task<string> GetDefaultSubscription(CancellationToken token = default(CancellationToken))
        {
            var subs = ListAsync(token).GetAsyncEnumerator();
            string sub = null;
            if (await subs.MoveNextAsync())
            {
                if (subs.Current != null)
                {
                    sub = subs.Current.Id.Subscription;
                }
            }
            return sub;
        }

        private ResourcesManagementClientOptions covertToResourcesManagementClientOptions()
        {
            var options = new ResourcesManagementClientOptions();
            options.Transport = this.ClientOptions.Transport;
            foreach (var pol in this.ClientOptions.PerCallPolicies)
            {
                options.AddPolicy(pol, HttpPipelinePosition.PerCall);
            }
            foreach (var pol in this.ClientOptions.PerRetryPolicies)
            {
                options.AddPolicy(pol, HttpPipelinePosition.PerRetry);
            }
            return options;
        }

        internal SubscriptionsOperations Operations => GetClient<ResourcesManagementClient>((uri, cred) =>
                    new ResourcesManagementClient(uri, Guid.NewGuid().ToString(), cred, this.covertToResourcesManagementClientOptions())).Subscriptions;
    }
}
