using Azure.Core;
using System;

namespace azure_proto_core
{
    /// <summary>
    /// TODO: split this into a base class for all Operations, and a base class for specific operations
    /// </summary>
    public abstract class OperationsBase
    {
        public OperationsBase(OperationsBase parent, ResourceIdentifier context) : this(parent.ClientContext, context)
        {
            Validate(context);
            Context = context;
            DefaultLocation = parent.DefaultLocation;
        }

        public OperationsBase(OperationsBase parent, Resource context) : this(parent, context.Id)
        {
            Validate(context?.Id);
            Context = context?.Id;
            var tracked = context as TrackedResource;
            if (tracked != null)
            {
                DefaultLocation = tracked.Location;
            }

            Resource = context;
        }

        public OperationsBase(ArmClientContext parent, ResourceIdentifier context)
        {
            ClientContext = parent;
            Validate(context);
            Context = context;
            DefaultLocation = Location.Default;
        }

        public OperationsBase(ArmClientContext parent, Resource context) : this(parent, context.Id)
        {
            Validate(context?.Id);
            Context = context?.Id;
            var tracked = context as TrackedResource;
            if (tracked != null)
            {
                DefaultLocation = tracked.Location;
            }
            Resource = context;
        }

        protected virtual ArmClientContext ClientContext { get; }

        protected virtual Resource Resource { get; set; }

        public virtual ResourceType ResourceType { get; }

        public virtual Location DefaultLocation { get; }

        public virtual ResourceIdentifier Context { get; }

        public virtual void Validate(ResourceIdentifier identifier)
        {
            if (identifier?.Type != ResourceType)
            {
                throw new InvalidOperationException($"{identifier} is not a valid resource of type {ResourceType}");
            }
        }

        /// <summary>
        /// Note that this is currently adapting to underlying management clients - once generator changes are in, this would likely be unnecessary
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="creator"></param>
        /// <returns></returns>
        protected T GetClient<T>(Func<Uri, TokenCredential, T> creator)
        {
            return ClientContext.GetClient<T>(creator);
        }

    }

}
