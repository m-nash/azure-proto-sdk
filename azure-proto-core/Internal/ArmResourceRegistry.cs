using azure_proto_core.Internal;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace azure_proto_core
{
    public class ArmResourceRegistry
    {
        internal IDictionary<Type, object> _registry = new ConcurrentDictionary<Type, object>();

        public void Register<T>(ArmResourceRegistration<T> registration) where T : TrackedResource
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
        public bool TryGetOperations<T>(ArmClientBase parent, TrackedResource context, out ResourceClientBase<T> operations) where T : TrackedResource
        {
            operations = null;
            ArmResourceRegistration<T> registration;
            if (!TryGetRegistration<T>(out registration) || !registration.HasOperations)
            {
                return false;
            }

            operations = registration.GetOperations(parent, context);
            return true;
        }


        public bool TryGetContainer<T>(ArmClientBase parent, TrackedResource parentContext, out ResourceContainerOperations<T> container) where T: TrackedResource
        {
            container = null;
            ArmResourceRegistration<T> registration;
            if (!TryGetRegistration<T>(out registration) || !registration.HasContainer)
            {
                return false;
            }

            container = registration.GetContainer(parent, parentContext);
            return true;
        }

        public bool TryGetColletcion<T>(ArmClientBase parent, ResourceIdentifier parentContext, out ResourceCollectionOperations<T> collection) where T : TrackedResource
        {
            collection = null;
            ArmResourceRegistration<T> registration;
            if (!TryGetRegistration<T>(out registration) || !registration.HasCollection)
            {
                return false;
            }

            collection = registration.GetCollection(parent, parentContext);
            return true;
        }

        public bool TryGetResourceType<T>(out ResourceType type) where T : TrackedResource
        {
            type = ResourceType.None;
            ArmResourceRegistration<T> registration;
            if (!TryGetRegistration<T>(out registration))
            {
                return false;
            }

            type = registration.ResourceType;
            return true;
        }

        internal bool TryGetRegistration<T>(out ArmResourceRegistration<T> registration) where T : TrackedResource
        {
            registration = null;
            object regObject;
            if (!_registry.TryGetValue(typeof(T), out regObject) || ((registration = regObject as ArmResourceRegistration<T>) == null))
            {
                return false;
            }

            return true;
        }

    }
}
