using Azure;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_core
{
    public class PhResponse<T, U> : Response<T> where T: TrackedResource<U> where U : class
    {
        Response<U> _wrapped;
        Func<U, T> _converter;

        public PhResponse(Response<U> wrapped, Func<U, T> converter)
        {
            _wrapped = wrapped;
            _converter = converter;
        }

        public override T Value => _converter(_wrapped.Value);

        public override Response GetRawResponse()
        {
            return _wrapped.GetRawResponse();
        }
    }
}
