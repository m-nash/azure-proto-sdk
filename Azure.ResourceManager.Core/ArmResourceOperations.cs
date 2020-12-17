// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Threading;
using System.Threading.Tasks;
using Azure;

namespace Azure.ResourceManager.Core
{
    public class ArmResourceOperations : ResourceOperationsBase<ArmResourceOperations, ArmResource>,
        ITaggable<ArmResourceOperations, ArmResource>, IDeletableResource<ArmResourceOperations, ArmResource>
    {
        public ArmResourceOperations(AzureResourceManagerClientContext context, ResourceIdentifier id)
            : base(context, id)
        {
        }

        public ArmResourceOperations(AzureResourceManagerClientContext context, ArmResource resource )
            : base(context, resource)
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
        public ArmOperation<ArmResourceOperations> AddTag(string key, string value)
        {
            throw new NotImplementedException();
        }

        public Task<ArmOperation<ArmResourceOperations>> AddTagAsync(
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

        public override ArmResponse<ArmResourceOperations> Get()
        {
            throw new NotImplementedException();
        }

        public override Task<ArmResponse<ArmResourceOperations>> GetAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
