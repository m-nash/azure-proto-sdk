using azure_proto_core;

namespace azure_proto_network
{
    public class XPublicIpAddress : PublicIpAddressOperations
    {
        public XPublicIpAddress(ArmClientContext context, PhPublicIPAddress resource) : base(context, resource.Id)
        {
            Model = resource;
        }

        public PhPublicIPAddress Model { get; private set; }
    }
}
