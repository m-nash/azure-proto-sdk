// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Threading;
using System.Threading.Tasks;
using Azure;

namespace Azure.ResourceManager.Core
{
    public class ArmResourceOperations : ResourceOperationsBase<ArmResource>,
        ITaggableResource<ArmResource>, IDeletableResource
    {
        public ArmResourceOperations(ArmClientContext context, ResourceIdentifier id, ArmClientOptions clientOption)
            : base(context, id, clientOption)
        {
        }

        public ArmResourceOperations(ArmClientContext context, ArmResourceData resource, ArmClientOptions clientOption)
            : base(context, resource, clientOption)
        {
        }

        public ArmResponse<Response> Delete()
        {
            throw new NotImplementedException();
        }

        public Task<ArmResponse<Response>> DeleteAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public ArmOperation<Response> StartDelete()
        {
            throw new NotImplementedException();
        }

        public async Task<ArmOperation<Response>> StartDeleteAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        // TODO: Fill out the methods using ResourceManagementClient
        public ArmOperation<ArmResource> AddTag(string key, string value)
        {
            throw new NotImplementedException();
        }

        public Task<ArmOperation<ArmResource>> AddTagAsync(
            string key,
            string value,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override void Validate(ResourceIdentifier identifier)
        {
            //the id can be of any type so do nothing
        }

        public override ArmResponse<ArmResource> Get()
        {
            throw new NotImplementedException();
        }

        public override Task<ArmResponse<ArmResource>> GetAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
