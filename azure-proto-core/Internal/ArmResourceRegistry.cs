using azure_proto_core.Internal;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace azure_proto_core
{
    public class ArmResourceRegistry
    {
        internal IDictionary<Type, object> _registry = new ConcurrentDictionary<Type, object>();

        public void Register<U, T>(ArmResourceRegistration<U, T> registration)
            where T : TrackedResource
            where U : ResourceOperationsBase<T>
        {
            _registry[registration.ModelType] = registration;
        }

        /// <summary>
        /// Return the typed resource operations for a specific top-level provider resource
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <param name="context"></param>
        /// <param name="operations"></param>
        /// <returns></returns>
        public bool TryGetOperations<U, T>(ArmClientContext parent, TrackedResource context, out ResourceOperationsBase<T> operations)
            where T : TrackedResource
            where U : ResourceOperationsBase<T>
        {
            operations = null;
            ArmResourceRegistration<U, T> registration;
            if (!TryGetRegistration<U, T>(out registration) || !registration.HasOperations)
            {
                return false;
            }

            operations = registration.GetOperations(parent, context);
            return true;
        }

        public bool TryGetContainer<U, T>(ArmClientContext parent, TrackedResource parentContext, out ResourceContainerOperations<U, T> container)
            where T: TrackedResource
            where U : ResourceOperationsBase<T>
        {
            container = null;
            ArmResourceRegistration<U, T> registration;
            if (!TryGetRegistration<U, T>(out registration) || !registration.HasContainer)
            {
                return false;
            }

            container = registration.GetContainer(parent, parentContext);
            return true;
        }

        public bool TryGetResourceType<U, T>(out ResourceType type)
            where T : TrackedResource
            where U : ResourceOperationsBase<T>
        {
            type = ResourceType.None;
            ArmResourceRegistration<U, T> registration;
            if (!TryGetRegistration<U, T>(out registration))
            {
                return false;
            }

            type = registration.ResourceType;
            return true;
        }

        internal bool TryGetRegistration<U, T>(out ArmResourceRegistration<U, T> registration)
            where T : TrackedResource
            where U : ResourceOperationsBase<T>
        {
            registration = null;
            object regObject;
            if (!_registry.TryGetValue(typeof(T), out regObject) || ((registration = regObject as ArmResourceRegistration<U, T>) == null))
            {
                return false;
            }

            return true;
        }
    }
}
