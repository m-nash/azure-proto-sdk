// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Resources.Models;

namespace Azure.ResourceManager.Core
{
    public class ArmResourceOperations : ResourceOperationsBase<ArmResource>, IDeletableResource
    {
        public ArmResourceOperations(AzureResourceManagerClientOptions options, ResourceIdentifier id, ApiVersionsBase apiVersion)
            : base(options, id)
        {
            ApiVersion = apiVersion;
        }

        public ArmResourceOperations(AzureResourceManagerClientOptions options, ArmResourceData resource, ApiVersionsBase apiVersion)
            : base(options, resource)
        {
            ApiVersion = apiVersion;
        }
        private ApiVersionsBase ApiVersion;

        public ArmResponse<Response> Delete()
        {
            return new ArmVoidResponse(Operations.StartDelete(Id.ResourceGroup, Id.Type.Namespace, Id.Parent, Id.Type.Type, Id.Name, ApiVersion).WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult());
        }

        public async Task<ArmResponse<Response>> DeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmVoidResponse(await Operations.StartDelete(Id.ResourceGroup, Id.Type.Namespace, Id.Parent, Id.Type.Type, Id.Name, ApiVersion).WaitForCompletionAsync());
        }

        public ArmOperation<Response> StartDelete()
        {
            return new ArmVoidOperation(Operations.StartDelete(Id.ResourceGroup, Id.Type.Namespace, Id.Parent, Id.Type.Type, Id.Name, ApiVersion));
        }

        public async Task<ArmOperation<Response>> StartDeleteAsync(CancellationToken cancellationToken = default)
        {
            return new ArmVoidOperation(await Operations.StartDeleteAsync(Id.ResourceGroup, Id.Type.Namespace, Id.Parent, Id.Type.Type, Id.Name, ApiVersion));
        }

        public override void Validate(ResourceIdentifier identifier)
        {
            //the id can be of any type so do nothing
        }

        public override ArmResponse<ArmResource> Get()
        {
            return new PhArmResponse<ArmResource, GenericResource>(
                Operations.Get(Id.ResourceGroup, Id.Type.Namespace, Id.Parent, Id.Type.Type, Id.Name, ApiVersion),
                a => new ArmResource(ClientOptions, new ArmResourceData(a), ApiVersion)); ;
        }

        public async override Task<ArmResponse<ArmResource>> GetAsync(CancellationToken cancellationToken = default)
        {
            return new PhArmResponse<ArmResource, GenericResource>(
                await Operations.GetAsync(Id.ResourceGroup, Id.Type.Namespace, Id.Parent, Id.Type.Type, Id.Name, ApiVersion, cancellationToken),
                a => new ArmResource(ClientOptions, new ArmResourceData(a), ApiVersion)); ;
        }

        internal ResourcesOperations Operations => GetClient<ResourcesManagementClient>((uri, creds) => new ResourcesManagementClient(
            uri,
            Id.Subscription,
            creds,
            ClientOptions.Convert<ResourcesManagementClientOptions>()))?.Resources;

    }
}
