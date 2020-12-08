using azure_proto_core;

namespace azure_proto_network
{
    public class XPublicIpAddress : PublicIpAddressOperations
    {
        internal XPublicIpAddress(ArmClientContext context, PhPublicIPAddress resource, ArmClientOptions clientOptions) : base(context, resource.Id, clientOptions)
        {
            Model = resource;
        }

        public PhPublicIPAddress Model { get; private set; }
    }
}
