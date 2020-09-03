using azure_proto_compute;
using azure_proto_core;
using System;

namespace client
{
    class StartStopVm : Scenario
    {
        public override void Execute()
        {
            var createVm = new CreateSingleVmExample(Context);
            createVm.Execute();

            var client = new ArmClient();
            var subscription = client.Subscriptions(Context.SubscriptionId);


            //subscription.ResourceGroups() --Container<T> //allows for Create/Delete methods builder overloads
            //subscription.ListResourceGroups() --Pageable<Entity<T>> //sync/async has filter overloads
            //foreach(var rg in subscription.ListResourceGroups()) { //do stuff }
            //subscription.ResourceGroup(rgName) --Entity<T> //no network call so has empty Model with Operations
            //subscription.ListVirtualMachines() --Pageable<Entity<T>>
            //subscription.ResourceGroup(rgName).ListVirtualMachines() --Pageable<Entity<T>>

            //recommend if we want common functionality in addition to what Pageable<T> provides we create an ArmPageable<T> to add that functionality
            //this removes confusion on collection vs container but maintains the majority of that separation

            //subscription.ResourceGroups().List() --Pagabel<Entity<T>>
            //subscription.ListNics() --Pageabel<Entity<T>>
            //subscription.ResourceGroup(rgName).ListNics() --Pageabel<Entity<T>>

            //armClient.ListNics() --Pageabel<Entity<T>>
            //armClient.DefaultSubscription.ResourceGroups() --Container<T>
            //armClient.Subscription(subId) --Entity<T>


            var resourceGroup = subscription.ResourceGroup(Context.RgName);
            var vm = resourceGroup.Vm(Context.VmName);
            Console.WriteLine($"Found VM {Context.VmName}");
            Console.WriteLine("--------Stopping VM--------");
            vm.Stop();
            Console.WriteLine("--------Starting VM--------");
            vm.Start();
        }
    }
}
