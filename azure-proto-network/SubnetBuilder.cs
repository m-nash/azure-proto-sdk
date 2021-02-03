using Azure.ResourceManager.Core;

namespace azure_proto_network
{
    /// <summary>
    /// A class representing a builder object to help create a subnet.
    /// </summary>
    public class SubnetBuilder : ArmBuilder<Subnet, SubnetData>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubnetBuilder"/> class.
        /// </summary>
        /// <param name="container"> The container to create the subnet in. </param>
        /// <param name="subnet"> The data model representing the subnet to create. </param>
        public SubnetBuilder(SubnetContainer container, SubnetData subnet)
            : base(container, subnet)
        {

        }

        /// <inheritdoc/>
        protected override void OnBeforeBuild()
        {
            Resource.Model.Name = ResourceName;
        }
    }
}
