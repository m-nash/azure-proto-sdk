using System;
using System.Collections.Generic;
using System.Text;
using azure_proto_core;

namespace azure_proto_compute.Convenience
{
    public interface IAzureModelBuilderBase<out T> where T : AzureEntity
    {
        IAzureModelBuilderBase<T> Location(Location location);

        T ToModel();
    }
}
