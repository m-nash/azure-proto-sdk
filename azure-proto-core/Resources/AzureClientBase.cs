using System;
using System.Collections.Generic;

namespace azure_proto_core
{
    public abstract class AzureClientBase : TrackedResource
    {
        protected abstract IEnumerable<AzureSubscriptionBase> SubscriptionsGeneric { get; set; }

        public IEnumerable<E> GetSubscriptions<C, E>(Func<TrackedResource, C> constructor)
            where C : AzureCollection<E>
            where E : AzureEntity
        {
            foreach (var sub in SubscriptionsGeneric)
            {
                foreach(var entity in sub.GetResources<C, E>(constructor))
                {
                    yield return entity;
                }
            }
        }
    }
}
