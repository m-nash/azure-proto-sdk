// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.ResourceManager.Core
{
    /// <summary>
    /// Abstract class for long-running or synchronous applications. If we want to add ARM-specific OM, this is where we
    /// would add it.
    /// We may need to add ARM-specific OM, as customers have asked for additional configurability over polling stratgies
    /// on a per-operation basis
    /// </summary>
    /// <typeparam name="TOperations"> The <see cref="OperationsBase"/> to return representing the result of the ArmOperation. </typeparam>
    public abstract class ArmOperation<TOperations> : Operation<TOperations>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArmOperation{TOperations}"/> class.
        /// </summary>
        /// <param name="syncValue"> The <see cref="OperationsBase"/> representing the result of the ArmOperation. </param>
        public ArmOperation(TOperations syncValue)
        {
            CompletedSynchronously = syncValue != null;
            SyncValue = syncValue;
        }

        /// <summary>
        /// Gets a value indicating whether or not the operation completed synchronously.
        /// </summary>
        protected bool CompletedSynchronously { get; }

        /// <summary>
        /// Gets the <see cref="OperationsBase"/> representing the result of the ArmOperation.
        /// </summary>
        protected TOperations SyncValue { get; }
    }
}
