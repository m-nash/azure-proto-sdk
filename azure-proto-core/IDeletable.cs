// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Threading;
using System.Threading.Tasks;
using Azure;

namespace azure_proto_core
{
    public interface IDeletable
    {
        ArmOperation<Response> Delete();

        Task<ArmOperation<Response>> DeleteAsync(CancellationToken cancellationToken = default);
    }
}
