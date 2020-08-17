using System;
using System.Collections.Generic;
using System.Text;
using azure_proto_core;

namespace azure_proto_network
{
    public static class AzureSubscriptionExtensions
    {
        /// <summary>
        /// Extensions for Vnets
        /// </summary>
        /// <param name="subscriptionOperations"></param>
        /// <returns></returns>
        public static VnetCollection Vnets(this SubscriptionCollectionOperations subscriptionOperations)
        {
            return new VnetCollection(subscriptionOperations, subscriptionOperations.DefaultSubscription);
        }

        public static VnetCollection Vnets(this SubscriptionCollectionOperations subscriptionOperations, ResourceIdentifier subscription)
        {
            return new VnetCollection(subscriptionOperations, subscription);
        }

        public static VnetCollection Vnets(this SubscriptionCollectionOperations subscriptionOperations, azure_proto_core.Resource subscription)
        {
            return new VnetCollection(subscriptionOperations, subscription);
        }

        public static VnetCollection Vnets(this SubscriptionCollectionOperations subscriptionOperations, string subscriptionId)
        {
            return new VnetCollection(subscriptionOperations, $"/subscriptions/{subscriptionId}");
        }

        /// <summary>
        /// Extensions for PublicIps
        /// </summary>
        /// <param name="subscriptionOperations"></param>
        /// <returns></returns>
        public static PublicIpCollection PublicIps(this SubscriptionCollectionOperations subscriptionOperations)
        {
            return new PublicIpCollection(subscriptionOperations, subscriptionOperations.DefaultSubscription);
        }

        public static PublicIpCollection PublicIps(this SubscriptionCollectionOperations subscriptionOperations, ResourceIdentifier subscription)
        {
            return new PublicIpCollection(subscriptionOperations, subscription);
        }

        public static PublicIpCollection PublicIps(this SubscriptionCollectionOperations subscriptionOperations, azure_proto_core.Resource subscription)
        {
            return new PublicIpCollection(subscriptionOperations, subscription);
        }

        public static PublicIpCollection PublicIps(this SubscriptionCollectionOperations subscriptionOperations, string subscriptionId)
        {
            return new PublicIpCollection(subscriptionOperations, $"/subscriptions/{subscriptionId}");
        }


        /// <summary>
        /// Extensions for Nics
        /// </summary>
        /// <param name="subscriptionOperations"></param>
        /// <returns></returns>
        public static NicCollection Nics(this SubscriptionCollectionOperations subscriptionOperations)
        {
            return new NicCollection(subscriptionOperations, subscriptionOperations.DefaultSubscription);
        }

        public static NicCollection Nics(this SubscriptionCollectionOperations subscriptionOperations, ResourceIdentifier subscription)
        {
            return new NicCollection(subscriptionOperations, subscription);
        }

        public static NicCollection Nics(this SubscriptionCollectionOperations subscriptionOperations, azure_proto_core.Resource subscription)
        {
            return new NicCollection(subscriptionOperations, subscription);
        }

        public static NicCollection Nics(this SubscriptionCollectionOperations subscriptionOperations, string subscriptionId)
        {
            return new NicCollection(subscriptionOperations, $"/subscriptions/{subscriptionId}");
        }

        /// <summary>
        /// Extensions for Nics
        /// </summary>
        /// <param name="subscriptionOperations"></param>
        /// <returns></returns>
        public static NsgCollection Nsgs(this SubscriptionCollectionOperations subscriptionOperations)
        {
            return new NsgCollection(subscriptionOperations, subscriptionOperations.DefaultSubscription);
        }

        public static NsgCollection NiNsgscs(this SubscriptionCollectionOperations subscriptionOperations, ResourceIdentifier subscription)
        {
            return new NsgCollection(subscriptionOperations, subscription);
        }

        public static NsgCollection Nsgs(this SubscriptionCollectionOperations subscriptionOperations, azure_proto_core.Resource subscription)
        {
            return new NsgCollection(subscriptionOperations, subscription);
        }

        public static NsgCollection Nsgs(this SubscriptionCollectionOperations subscriptionOperations, string subscriptionId)
        {
            return new NsgCollection(subscriptionOperations, $"/subscriptions/{subscriptionId}");
        }



    }
}
