using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_core
{
    /// <summary>
    /// Placeholder for ARM Patch operations
    /// </summary>
    public interface IPatchModel
    {
        IDictionary<string, string> Tags { get; }
    }
}
