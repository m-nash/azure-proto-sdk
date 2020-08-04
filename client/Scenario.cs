using System.Collections.Generic;

namespace client
{
    abstract class Scenario
    {
        public ScenarioContext Context { get; private set; }

        public List<string> CleanUp { get; private set; }

        public abstract void Execute();

        public Scenario() : this(new ScenarioContext()) { }

        public Scenario(ScenarioContext context)
        {
            Context = context;
            CleanUp = new List<string>() { context.RgName };
        }
    }
}
