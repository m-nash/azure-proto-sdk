using System;
using System.Collections.Generic;

namespace azure_proto_core
{
    //TODO: If we decide to put base types plus core classes all in one this this class is no longer necessary
    public abstract class AzureSubscriptionBase : AzureEntityHolder<TrackedResource>
    {
        public AzureSubscriptionBase(TrackedResource parent, TrackedResource model) : base(parent, model) { }

        protected abstract IEnumerable<AzureEntityHolder<TrackedResource>> ResourceGroupsGeneric { get; set; }

        //TODO: change to IPageable
        public virtual IEnumerable<E> GetResources<C, E>(Func<TrackedResource, C> constructor)
            where C : AzureCollection<E>
            where E : AzureEntity
        {
            foreach (var rg in ResourceGroupsGeneric)
            {
                foreach (var entity in rg.GetCollection<C, E>(() => { return constructor(rg); }))
                {
                    yield return entity;
                }
            }
        }
    }
}
