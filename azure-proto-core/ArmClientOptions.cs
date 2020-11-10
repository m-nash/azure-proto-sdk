using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace azure_proto_core
{
    public class ArmClientOptions
    {
        private Dictionary<Type, object> _overrides = new Dictionary<Type, object>();
        private static readonly object _overridesLock = new object();

        [EditorBrowsable(EditorBrowsableState.Never)]
        public object GetOverrideObject<T>(Func<object> ctor)
        {
            object overrideObject;
            Type type = typeof(T);
            if (!_overrides.TryGetValue(type, out overrideObject))
            {
                lock (_overridesLock)
                {
                    if (!_overrides.TryGetValue(type, out overrideObject))
                    {
                        overrideObject = ctor();
                        _overrides[type] = overrideObject;
                    }
                }
            }
            return overrideObject;
        }
    }
}
