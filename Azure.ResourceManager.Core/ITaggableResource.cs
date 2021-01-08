// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Threading;
using System.Threading.Tasks;

namespace Azure.ResourceManager.Core
{
    /// <summary>
    /// Operations that allow manipulating resource tags
    /// </summary>
    /// <typeparam name="TOperations">The typed operations for a specific resource</typeparam>
    public interface ITaggableResource<TOperations>
        where TOperations : ResourceOperationsBase<TOperations>
    {
        /// <summary>
        /// Add a tag to the resource
        /// </summary>
        /// <param name="key">The tag key</param>
        /// <param name="value">The tag value</param>
        /// <returns>An <see cref="ArmOperation{TOperations}"/> that allows the user to control polling and waiting for Tag completion.</returns>
        ArmOperation<TOperations> AddTag(string key, string value);

        /// <summary>
        /// Add a tag to the resource
        /// </summary>
        /// <param name="key">The tag key</param>
        /// <param name="value">The tag value</param>
        /// <param name="cancellationToken"></param>
        /// <returns> a <see cref="Task"/> that performs the Tag operation.  The Task yields an an
        /// <see cref="ArmOperation{TOperations}"/> that allows the user to control polling and waiting for
        /// Tag completion.</returns>
        Task<ArmOperation<TOperations>> AddTagAsync(
            string key,
            string value,
            CancellationToken cancellationToken = default);
    }
}
