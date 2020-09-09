using Azure;
using Azure.ResourceManager.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_core
{
    /// <summary>
    /// TODO: Refactor using the new ArmOperations class, which allows synchronous or asynchronous operations
    /// Create known Container and Leaf scopes for ARM Containers
    /// Think about how to extend known scope types in an extensible fashion (is it just adding them to the default, or is it having scopes for all provider or consumer services?
    /// For example, INetworkConsumer, IDatabaseConsumer, IEncryptionConsumer, IControlConsumer, ITriggerConsumer which also allows you to attach at that scope? [AttachDatabase]
    /// </summary>
    /// <typeparam name="T">The type of the resource model</typeparam>
    /// <typeparam name="U">The return type of the Creation methods, this can be Response<typeparamref name="T"/> or a long-running response</typeparam>
    public abstract class ResourceContainerOperations<T> : OperationsBase where T : Resource
    {
        protected ResourceContainerOperations(ArmClientContext parent, ResourceIdentifier contexts) : base(parent, contexts)
        {
        }

        protected ResourceContainerOperations(ArmClientContext parent, Resource contexts) : base(parent, contexts)
        {
        }

        protected ResourceContainerOperations(OperationsBase parent, ResourceIdentifier contexts) : base(parent, contexts)
        {
        }

        protected ResourceContainerOperations(OperationsBase parent, Resource contexts) : base(parent, contexts)
        {
        }

        public override void Validate(ResourceIdentifier identifier)
        {
            if (identifier.Type != "Microsoft.Resources/resourceGroups" && identifier.Type != ResourceType.Parent)
            {
                throw new InvalidOperationException($"{identifier.Type} is not a valid container for {ResourceType}");
            }
        }

        public virtual ArmOperation<ResourceOperationsBase<T>> Create(T resourceDetails)
        {
            return Create(resourceDetails.Name, resourceDetails);
        }

        public abstract ArmOperation<ResourceOperationsBase<T>> Create(string name, T resourceDetails = null);
        public virtual Task<ArmOperation<ResourceOperationsBase<T>>> CreateAsync(T resourceDetails, CancellationToken cancellationToken = default)
        {
            return CreateAsync(resourceDetails?.Id?.Name, resourceDetails, cancellationToken);
        }

        public abstract Task<ArmOperation<ResourceOperationsBase<T>>> CreateAsync(string name, T resourceDetails, CancellationToken cancellationToken = default);
    }
}
