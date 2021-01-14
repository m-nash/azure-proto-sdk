using Azure.ResourceManager.Core;

namespace azure_proto_compute
{
    /// <summary>
    /// A class representing an availability set along with the instance operations that can be performed on it.
    /// </summary>
    public class AvailabilitySet : AvailabilitySetOperations
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AvailabilitySet"/> class.
        /// </summary>
        /// <param name="options"> The client parameters to use in these operations. </param>
        /// <param name="resource"> The resource that is the target of operations. </param>
        public AvailabilitySet(ResourceOperationsBase options, AvailabilitySetData resource)
            : base(operations, resource.Id)
        {
            Data = resource;
        }

        /// <summary>
        /// Gets or sets the availability set data.
        /// </summary>
        public AvailabilitySetData Data { get; private set; }
    }
}
