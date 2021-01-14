namespace Azure.ResourceManager.Core
{
    /// <summary>
    /// A class representing a ResourceGroup along with the instance operations that can be performed on it.
    /// </summary>
    public class ResourceGroup : ResourceGroupOperations
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceGroup"/> class.
        /// </summary>
        /// <param name="options"> The client parameters to use in these operations. </param>
        /// <param name="resource"> The ResourceGroupData to use in these operations. </param>
        internal ResourceGroup(ResourceOperationsBase operations, ResourceGroupData resource)
            : base(operations, resource.Id)
        {
            Data = resource;
        }

        /// <summary>
        /// Gets the data representing this ResourceGroup.
        /// </summary>
        public ResourceGroupData Data { get; private set; }
    }
}
