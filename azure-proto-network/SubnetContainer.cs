using Azure;
using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using azure_proto_core.Adapters;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace azure_proto_network
{
    public class SubnetContainer : ResourceContainerOperations<SubnetOperations, PhSubnet>
    {
        public SubnetContainer(ArmClientContext context, PhVirtualNetwork virtualNetwork, ArmClientOptions clientOptions) : base(context, virtualNetwork, clientOptions) { }

        public override ResourceType ResourceType => "Microsoft.Network/virtualNetworks/subnets";


        internal SubnetsOperations Operations => GetClient<NetworkManagementClient>((uri, cred) => new NetworkManagementClient(Id.Subscription, uri, cred,
                    ArmClientOptions.convert<NetworkManagementClientOptions>(this.ClientOptions))).Subnets;

        public override ArmResponse<SubnetOperations> Create(string name, PhSubnet resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = Operations.StartCreateOrUpdate(Id.ResourceGroup, Id.Name, name, resourceDetails.Model, cancellationToken);
            return new PhArmResponse<SubnetOperations, Subnet>(
                operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false).GetAwaiter().GetResult(),
                s => new SubnetOperations(ClientContext, new PhSubnet(s, Location.Default), this.ClientOptions));
        }

        public async override Task<ArmResponse<SubnetOperations>> CreateAsync(string name, PhSubnet resourceDetails, CancellationToken cancellationToken = default)
        {
            var operation = await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, name, name, resourceDetails.Model, cancellationToken).ConfigureAwait(false);
            return new PhArmResponse<SubnetOperations, Subnet>(
                await operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false),
                s => new SubnetOperations(ClientContext, new PhSubnet(s, Location.Default), this.ClientOptions));
        }

        public override ArmOperation<SubnetOperations> StartCreate(string name, PhSubnet resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<SubnetOperations, Subnet>(
                Operations.StartCreateOrUpdate(Id.ResourceGroup, Id.Name, name, resourceDetails.Model, cancellationToken),
                s => new SubnetOperations(ClientContext, new PhSubnet(s, Location.Default), this.ClientOptions));
        }

        public async override Task<ArmOperation<SubnetOperations>> StartCreateAsync(string name, PhSubnet resourceDetails, CancellationToken cancellationToken = default)
        {
            return new PhArmOperation<SubnetOperations, Subnet>(
                await Operations.StartCreateOrUpdateAsync(Id.ResourceGroup, Id.Name, name, resourceDetails.Model, cancellationToken).ConfigureAwait(false),
                s => new SubnetOperations(ClientContext, new PhSubnet(s, Location.Default), this.ClientOptions));
        }

        public ArmBuilder<SubnetOperations, PhSubnet> Construct(string name, string cidr, Location location = null, PhNetworkSecurityGroup group = null)
        {
            var subnet = new Subnet()
            {
                Name = name,
                AddressPrefix = cidr,
            };

            if (null != group)
            {
                subnet.NetworkSecurityGroup = group.Model;
            }

            return new ArmBuilder<SubnetOperations, PhSubnet>(this, new PhSubnet(subnet, location ?? DefaultLocation));
        }

        public Pageable<SubnetOperations> List(CancellationToken cancellationToken = default)
        {
            return new PhWrappingPageable<Subnet, SubnetOperations>(
                Operations.List(Id.ResourceGroup, Id.Name, cancellationToken),
                this.convertor());
        }

        public AsyncPageable<SubnetOperations> ListAsync(CancellationToken cancellationToken = default)
        {
            return new PhWrappingAsyncPageable<Subnet, SubnetOperations>(
                Operations.ListAsync(Id.ResourceGroup, Id.Name, cancellationToken),
                this.convertor());
        }
        private Func<Subnet, SubnetOperations> convertor()
        {
            //TODO: Subnet will be a proxy resource and not a tracked resource ADO #4481
            return s => new SubnetOperations(ClientContext, new PhSubnet(s, Location.Default), this.ClientOptions);
        }

    }
}
