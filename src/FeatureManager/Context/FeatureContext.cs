namespace FeatureManagerFramework.Context
{
    public class FeatureContext
    {
        public string ActiveScenario { get; set; }

        public FeatureContext(string activeScenario)
        {
            ActiveScenario = activeScenario;
        }
    }
}
