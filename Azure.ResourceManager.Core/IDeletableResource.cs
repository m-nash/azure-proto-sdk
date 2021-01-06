// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Threading;
using System.Threading.Tasks;

namespace Azure.ResourceManager.Core
{
    /// <summary>
    /// The operations to delete a resource
    /// </summary>
    public interface IDeletableResource
    {
        /// <summary>
        /// Delete the resource.  This call blocks until the delete operation is completed on the service.
        /// </summary>
        /// <returns>The final http response to the delete request</returns>
        ArmResponse<Response> Delete();

        /// <summary>
        /// Delete the resource.  This call returns a <see cref="Task"/> that completes the delete operation.  The task may perform multiple 
        /// blocking calls
        /// </summary>
        /// <param name="cancellationToken">A token allowing immediate cancellation of any blocking call performed during the deletion.</param>
        /// <returns>A <see cref="Task"/> that completes the delete operation</returns>
        Task<ArmResponse<Response>> DeleteAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete the resource.  This call blocks until the delete operation is accepted on the service.
        /// </summary>
        /// <param name="cancellationToken">A token allowing immediate cancellation of any blocking call performed during the deletion.</param>
        /// <returns>An <see cref="ArmResponse{Response}"/> which allows the caller to control polling and waiting for resource deletion.
        /// The operation yields the final http response to the delete request when complete.</returns>
        ArmOperation<Response> StartDelete(CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete the resource.  This call returns a Task that blocks until the delete operation is accepted on the service.
        /// </summary>
        /// <returns>A <see cref="Task"/> that performs deletion.  The task yields a <see cref="ArmResponse{Response}"/> which 
        /// allows the caller to control polling and waiting for resource deletion.
        /// The operation yields the final http response to the delete request when complete.</returns>
        Task<ArmOperation<Response>> StartDeleteAsync(CancellationToken cancellationToken = default);
    }
}
