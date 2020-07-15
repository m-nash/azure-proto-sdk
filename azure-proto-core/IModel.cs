using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_core
{
    public interface IModel
    {
        string Name { get; }
        string Id { get; }
        object Data { get; }
    }
}
