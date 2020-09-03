using Azure;
using System;

namespace azure_proto_core
{
    /// <summary>
    /// Placeholder class, used to convert the gewneric type argument for a response from the underlyign rest API to the desired type argument in the response
    /// </summary>
    /// <typeparam name="T">The desired type argument for the returned response</typeparam>
    /// <typeparam name="U">The type argument of the response to be converted</typeparam>
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
