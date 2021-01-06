// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.ResourceManager.Core
{
    public class ArmResponse : ArmResponse<Response>
    {
        private readonly Response _response;

        public ArmResponse(Response response)
        {
            _response = response;
        }

        public override Response Value => _response;

        public override Response GetRawResponse()
        {
            return _response;
        }
    }

    public abstract class ArmResponse<TOperations> : Response<TOperations>
    {
    }
}
