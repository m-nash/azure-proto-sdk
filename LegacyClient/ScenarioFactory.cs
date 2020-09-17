namespace client
{
    enum Scenarios
    {
        All,
        CreateSingleVmExample,
        ShutdownVmsByTag,
        CreateMultipleVms,
        GenericEntityLoop,
        ShutdownVmsByNameAcrossSubscriptions
    }

    class ScenarioFactory
    {
        public static Scenario GetScenario(Scenarios scenario)
        {
            switch(scenario)
            {
                case Scenarios.CreateSingleVmExample:
                    return new CreateSingleVmExample();
                case Scenarios.ShutdownVmsByTag:
                    return new ShutdownVmsByTag();
                case Scenarios.CreateMultipleVms:
                    return new CreateMultipleVms();
                case Scenarios.GenericEntityLoop:
                    return new GenericEntityLoop();
                case Scenarios.All:
                    return new All();
                case Scenarios.ShutdownVmsByNameAcrossSubscriptions:
                    return new ShutdownVmsByNameAcrossSubscriptions();
                default:
                    return null;
            }
        }
    }
}
