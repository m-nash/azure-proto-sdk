// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Threading;
using System.Threading.Tasks;

namespace Azure.ResourceManager.Core
{
    public interface ITaggableResource<TOperations>
        where TOperations : ResourceOperationsBase<TOperations>
    {
        ArmOperation<TOperations> AddTag(string key, string value);

        Task<ArmOperation<TOperations>> AddTagAsync(
            string key,
            string value,
            CancellationToken cancellationToken = default);
    }
}