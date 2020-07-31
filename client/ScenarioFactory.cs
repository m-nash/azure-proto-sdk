namespace client
{
    enum Scenarios
    {
        CreateSingleVmExample,
        ShutdownVmsByName,
        StartStopVm,
        StartFromVm,
        SetTagsOnVm,
        ShutdownVmsByTag,
        CreateMultipleVms
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
                default:
                    return null;
            }
        }
    }
}
