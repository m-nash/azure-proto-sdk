using Azure.ResourceManager.Core;

namespace azure_proto_network
{
    public class SubnetBuilder : ArmBuilder<Subnet, SubnetData>
    {
        public SubnetBuilder(SubnetContainer container, SubnetData subnet)
            : base(container, subnet)
        {

        }

        protected override void OnBeforeBuild()
        {
            Resource.Model.Name = ResourceName;
        }
    }
}
