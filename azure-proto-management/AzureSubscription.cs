using azure_proto_core;
using Azure.ResourceManager.Resources.Models;

namespace azure_proto_management
{
    public class AzureSubscription : AzureEntity<PhSubscriptionModel>
    {
        string _name;
        public LocationCollection Locations { get; private set; }
        public ResourceGroupCollection ResourceGroups { get; private set; }

        public AzureSubscription(AzureClient client, PhSubscriptionModel subModel) : base(client, subModel)
        {
            Locations = new LocationCollection(this);
            ResourceGroups = new ResourceGroupCollection(this);
            _name = subModel.DisplayName;
        }

        public override string Name => _name;
    }
}
