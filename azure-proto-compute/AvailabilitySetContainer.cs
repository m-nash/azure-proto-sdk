﻿using Azure;
using Azure.ResourceManager.Compute;
using Azure.ResourceManager.Compute.Models;
using azure_proto_core;
using azure_proto_core.Adapters;
using azure_proto_core.Resources;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_compute
{
    /// <summary>
    /// Operatiosn class for Availability Set Contaienrs (resource groups)
    /// </summary>
    public class AvailabilitySetContainer : ResourceContainerOperations<XAvailabilitySet, PhAvailabilitySet>
    {
        public AvailabilitySetContainer(ArmClientContext context, PhResourceGroup resourceGroup, ArmClientOptions clientOptions) : base(context, resourceGroup, clientOptions) { }

        internal AvailabilitySetContainer(ArmClientContext context, ResourceIdentifier id, ArmClientOptions clientOptions):base(context, id, clientOptions) { }

        public override ResourceType ResourceType => "Microsoft.Compute/availabilitySets";

        public override ArmResponse<XAvailabilitySet> Create(string name, PhAvailabilitySet resourceDetails, CancellationToken cancellationToken = default)
        {
            var response = Operations.CreateOrUpdate(Id.ResourceGroup, name, resourceDetails.Model, cancellationToken);
            return new PhArmResponse<XAvailabilitySet, AvailabilitySet>(
                response,
                a => new XAvailabilitySet(ClientContext, new PhAvailabilitySet(a), ClientOptions));
        }

        public async override Task<ArmResponse<XAvailabilitySet>> CreateAsync(string name, PhAvailabilitySet resourceDetails, CancellationToken cancellationToken = default)
        {
            var response = await Operations.CreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails.Model, cancellationToken).ConfigureAwait(false);
            return new PhArmResponse<XAvailabilitySet, AvailabilitySet>(
                response,
                a => new XAvailabilitySet(ClientContext, new PhAvailabilitySet(a), ClientOptions));
        }

        public override ArmOperation<XAvailabilitySet> StartCreate(string name, PhAvailabilitySet resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<XAvailabilitySet, AvailabilitySet>(
                Operations.CreateOrUpdate(Id.ResourceGroup, name, resourceDetails.Model, cancellationToken),
                a => new XAvailabilitySet(ClientContext, new PhAvailabilitySet(a), ClientOptions));
        }

        public async override Task<ArmOperation<XAvailabilitySet>> StartCreateAsync(string name, PhAvailabilitySet resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<XAvailabilitySet, AvailabilitySet>(
                await Operations.CreateOrUpdateAsync(Id.ResourceGroup, name, resourceDetails.Model, cancellationToken).ConfigureAwait(false),
                a => new XAvailabilitySet(ClientContext, new PhAvailabilitySet(a), ClientOptions));
        }

        public ArmBuilder<XAvailabilitySet, PhAvailabilitySet> Construct(string skuName, Location location = null)
        {
            var availabilitySet = new AvailabilitySet(location ?? DefaultLocation)
            {
                PlatformUpdateDomainCount = 5,
                PlatformFaultDomainCount = 2,
                Sku = new Azure.ResourceManager.Compute.Models.Sku() { Name = skuName }
            };

            return new ArmBuilder<XAvailabilitySet, PhAvailabilitySet>(this, new PhAvailabilitySet(availabilitySet));
        }

        public Pageable<ArmResourceOperations> ListByName(ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(PhAvailabilitySet.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContext<ArmResourceOperations, ArmResource>(ClientContext, ClientOptions, Id, filters, top, cancellationToken);
        }

        public AsyncPageable<ArmResourceOperations> ListByNameAsync(ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            ArmFilterCollection filters = new ArmFilterCollection(PhAvailabilitySet.ResourceType);
            filters.SubstringFilter = filter;
            return ResourceListOperations.ListAtContextAsync<ArmResourceOperations, ArmResource>(ClientContext, ClientOptions, Id, filters, top, cancellationToken);
        }

        public Pageable<XAvailabilitySet> ListByNameExpanded(ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            var results = ListByName(filter, top, cancellationToken);
            return new PhWrappingPageable<ArmResourceOperations, XAvailabilitySet>(results, s => new AvailabilitySetOperations(s).Get().Value);
        }

        public AsyncPageable<XAvailabilitySet> ListByNameExpandedAsync(ArmSubstringFilter filter, int? top = null, CancellationToken cancellationToken = default)
        {
            var results = ListByNameAsync(filter, top, cancellationToken);
            return new PhWrappingAsyncPageable<ArmResourceOperations, XAvailabilitySet>(results, s => new AvailabilitySetOperations(s).Get().Value);
        }

        internal AvailabilitySetsOperations Operations => GetClient((uri, cred) => new ComputeManagementClient(uri, Id.Subscription, cred, 
                    ArmClientOptions.Convert<ComputeManagementClientOptions>(ClientOptions))).AvailabilitySets;
    }
}
