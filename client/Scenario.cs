using System.Collections.Generic;

namespace client
{
    abstract class Scenario
    {
        public ScenarioContext Context { get; private set; }

        public readonly static HashSet<string> CleanUp = new HashSet<string>();

        public abstract void Execute();

        public Scenario() : this(new ScenarioContext()) { }

        public Scenario(ScenarioContext context)
        {
            Context = context;
        }
    }
}
