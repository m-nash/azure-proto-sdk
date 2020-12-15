// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure;

namespace Azure.ResourceManager.Core
{
    public abstract class ArmResponse<TOperations> : Response<TOperations>
    {
    }

    public class ArmVoidResponse : ArmResponse<Response>
    {
        private readonly Response _response;

        public ArmVoidResponse(Response response)
        {
            _response = response;
        }

        public override Response Value => _response;

        public override Response GetRawResponse()
        {
            return _response;
        }
    }
}
