namespace AutomationFramework.Tests.Config
{
    public static class Constants
    {
        public static class URL
        {
            public const string BaseUl = "https://www.saucedemo.com";
        }

        public static class Timeouts
        {
            public const int ExplicitWaitSecond = 10;
            public const int PollingIntervallMillisecond = 250;
            public const int PageLoadingSecond = 10;
            public const int ScriptFinishedSeconds = 5;
        }

        public static class Paths
        {
            public static readonly string ScreenshotDirectory =
                Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Screenshots");
            public static readonly string LogDirectory =
                Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Log");
        }

        public static class Browser
        {
            public const bool Incognito = false;

            // CI runners have no display, so default to headless
            // there via the HEADLESS env var; local runs stay headed.
            public static readonly bool HeadLess =
                bool.TryParse(
                    Environment.GetEnvironmentVariable("HEADLESS"),
                    out var headless) && headless;

            public const int WindowWidth = 1920;
            public const int WindowHeight = 1080;
            public const BrowserType DefaultBrowser = BrowserType.Chrome;
        }

        public static class Users
        {
            public const string StandardUser = "standard_user";
            public const string LockedOutUser = "locked_out_user";
            public const string Password = "secret_sauce";
        }
    }
}