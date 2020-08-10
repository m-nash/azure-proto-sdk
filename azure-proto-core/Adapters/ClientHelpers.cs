using Azure;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_core
{
    /// <summary>
    /// Helpers for handling
    /// </summary>
    public class ClientHelpers
    {
        public static Response<T> ConvertResponse<T,U>(Response<U> source) where T: TrackedResource<U> where U: class
        {
            return new Response<T>()
        }
    }
}
