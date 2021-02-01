namespace client
{
    enum Scenarios
    {
        All,
        CreateSingleVmExample,
        CheckLocation,
        ShutdownVmsByName,
        StartStopVm,
        StartFromVm,
        SetTagsOnVm,
        ShutdownVmsByTag,
        CreateMultipleVms,
        GenericEntityLoop,
        ShutdownVmsByLINQ,
        ShutdownVmsByNameAcrossResourceGroups,
        //ShutdownVmsByNameAcrossSubscriptions,
        ListByNameExpanded,
        ClientOptionsOverride,
        GetSubscription,
        NullDataValues
        //RoleAssignment,
        //DeleteGeneric,
        //AddTagToGeneric
    }

    class ScenarioFactory
    {
        public static Scenario GetScenario(Scenarios scenario)
        {
            switch(scenario)
            {
                case Scenarios.CreateSingleVmExample:
                    return new CreateSingleVmExample();
                case Scenarios.ShutdownVmsByName:
                    return new ShutdownVmsByName();
                case Scenarios.StartStopVm:
                    return new StartStopVm();
                case Scenarios.StartFromVm:
                    return new StartFromVm();
                case Scenarios.SetTagsOnVm:
                    return new SetTagsOnVm();
                case Scenarios.ShutdownVmsByTag:
                    return new ShutdownVmsByTag();
                case Scenarios.CreateMultipleVms:
                    return new CreateMultipleVms();
                case Scenarios.GenericEntityLoop:
                    return new GenericEntityLoop();
                case Scenarios.ShutdownVmsByLINQ:
                    return new ShutdownVmsByLINQ();
                case Scenarios.All:
                    return new All();
                case Scenarios.ShutdownVmsByNameAcrossResourceGroups:
                    return new ShutdownVmsByNameAcrossResourceGroups();
                //case Scenarios.ShutdownVmsByNameAcrossSubscriptions:
                //    return new ShutdownVmsByNameAcrossSubscriptions();
                case Scenarios.ListByNameExpanded:
                    return new ListByNameExpanded();
                case Scenarios.ClientOptionsOverride:
                    return new ClientOptionsOverride();
                case Scenarios.GetSubscription:
                    return new GetSubscription();
                case Scenarios.NullDataValues:
                    return new NullDataValues();
                //case Scenarios.RoleAssignment:
                //    return new RoleAssignment();
                //case Scenarios.DeleteGeneric:
                //    return new DeleteGeneric();
                //case Scenarios.AddTagToGeneric:
                //    return new AddTagToGeneric();
                default:
                    return null;
            }
        }
    }
}
