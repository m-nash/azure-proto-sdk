using azure_proto_core;

namespace azure_proto_network
{
    public class PublicIpAddress : PublicIpAddressOperations
    {
        public PublicIpAddress(ArmClientContext context, PublicIPAddressData resource) : base(context, resource.Id)
        {
            Model = resource;
        }

        public PublicIPAddressData Model { get; private set; }
    }
}
