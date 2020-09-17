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
            foreach (var sub in client.ListSubscriptions())
            {
                foreach (var vm in sub.ListVms("MyFilterString"))
                {

                    Console.WriteLine($"Found VM {vm.Context.Name}");
                    Console.WriteLine("--------Stopping VM--------");
                    vm.Stop();
                    Console.WriteLine("--------Starting VM--------");
                    vm.Start();
                }
            }
        }
    }
}
