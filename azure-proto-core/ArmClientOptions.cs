using System;
using System.Collections.Generic;
using System.ComponentModel;
using Azure.Core;
using Azure.Core.Pipeline;

namespace azure_proto_core
{
    public class ArmClientOptions : ClientOptions
    {
        private Dictionary<Type, object> _overrides = new Dictionary<Type, object>();

        private static readonly object _overridesLock = new object();

        public static T Convert<T>(ArmClientOptions options)
            where T : ClientOptions, new()
        {
            var newOptions = new T();
            newOptions.Transport = options.Transport;
            foreach (var pol in options.PerCallPolicies)
            {
                newOptions.AddPolicy(pol, HttpPipelinePosition.PerCall);
            }

            foreach (var pol in options.PerRetryPolicies)
            {
                newOptions.AddPolicy(pol, HttpPipelinePosition.PerRetry);
            }

            return newOptions;
        }

        // TODO policy lists are internal hence we don't have acces to them by inheriting ClientOptions in this Asembly, this is a wrapper for now to convert to the concrete
        // policy options.
        public new void AddPolicy(HttpPipelinePolicy policy, HttpPipelinePosition position)
        {
            switch (position)
            {
                case HttpPipelinePosition.PerCall:
                    PerCallPolicies.Add(policy);
                    break;
                case HttpPipelinePosition.PerRetry:
                    PerRetryPolicies.Add(policy);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(position), position, null);
            }

            base.AddPolicy(policy, position);
        }

        internal IList<HttpPipelinePolicy> PerCallPolicies { get; } = new List<HttpPipelinePolicy>();

        internal IList<HttpPipelinePolicy> PerRetryPolicies { get; } = new List<HttpPipelinePolicy>();

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