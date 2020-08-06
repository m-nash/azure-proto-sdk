using azure_proto_core;
using System.Collections.Generic;

namespace azure_proto_management
{
    public class AzureSubscription : AzureSubscriptionBase
    {
        string _name;
        public LocationCollection Locations { get; private set; }
        protected override IEnumerable<AzureEntityHolder<TrackedResource>> ResourceGroupsGeneric { get; set; }
        public ResourceGroupCollection ResourceGroups => ResourceGroupsGeneric as ResourceGroupCollection;

        public AzureSubscription(AzureClient client, PhSubscriptionModel subModel) : base(client, subModel)
        {
            Locations = new LocationCollection(this);
            ResourceGroupsGeneric = new ResourceGroupCollection(this);
            _name = subModel.DisplayName;
        }

        public override string Name => _name;
    }
}
