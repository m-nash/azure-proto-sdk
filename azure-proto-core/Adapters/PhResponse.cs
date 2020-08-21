using Azure;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_core
{
    public class PhArmResponse<T, U> : Response<T> where T: class where U : class
    {
        Response<U> _wrapped;
        Func<U, T> _converter;

        public PhArmResponse(Response<U> wrapped, Func<U, T> converter)
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
