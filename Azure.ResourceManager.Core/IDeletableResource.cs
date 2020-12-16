// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Threading;
using System.Threading.Tasks;
using Azure;

namespace Azure.ResourceManager.Core
{
    public interface IDeletableResource
    {
        ArmResponse<Response> Delete();

        Task<ArmResponse<Response>> DeleteAsync(CancellationToken cancellationToken = default);

        ArmOperation<Response> StartDelete();

        Task<ArmOperation<Response>> StartDeleteAsync(CancellationToken cancellationToken = default);
    }
}