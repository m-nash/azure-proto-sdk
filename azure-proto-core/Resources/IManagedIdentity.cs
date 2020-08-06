using System.Collections.Generic;

namespace azure_proto_core
{
    /// <summary>
    /// Resource that uses MSI
    /// </summary>
    public interface IManagedIdentity
    {
        IList<Identity> Identity { get; set; }
    }
}
