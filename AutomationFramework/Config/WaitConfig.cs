namespace AutomationFramework.Tests.Config
{
    public class WaitConfig
    {
        public TimeSpan Timeout { get; }
        public TimeSpan PollingIntervall { get; }

        public WaitConfig(TimeSpan timeout, TimeSpan pollingInterval)
        {
            Timeout = timeout;
            PollingIntervall = pollingInterval;
        }

        public static WaitConfig Default => new(
            TimeSpan.FromSeconds(Constants.Timeouts.ExplicitWaitSecond),
            TimeSpan.FromMilliseconds(Constants.Timeouts.PollingIntervallMillisecond));

        public static WaitConfig Custom(int timeoutSeconds, int? pollingIntervalMilliseconds = null) => new(
            TimeSpan.FromSeconds(timeoutSeconds),
            TimeSpan.FromMilliseconds(pollingIntervalMilliseconds
                ?? Constants.Timeouts.PollingIntervallMillisecond));
    }
}