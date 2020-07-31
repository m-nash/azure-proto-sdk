using System.ComponentModel.Design.Serialization;

namespace client
{
    abstract class Scenario
    {
        public ScenarioContext Context { get; private set; }

        public abstract void Execute();

        public Scenario() : this(new ScenarioContext()) { }

        public Scenario(ScenarioContext context)
        {
            Context = context;
        }
    }
}
