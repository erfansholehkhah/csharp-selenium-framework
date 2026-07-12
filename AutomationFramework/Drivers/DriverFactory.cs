using AutomationFramework.Tests.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace AutomationFramework.Tests.Utilities
{
    internal class Driver
    {
        // ─────────────────────────────────────
        // ThreadLocal ensures each thread gets
        // its own driver instance
        // supports parallel test execution
        // ─────────────────────────────────────
        private static readonly ThreadLocal<IWebDriver> _driver = new();

        // ─────────────────────────────────────
        // INIT — called in BaseTest [SetUp]
        // ─────────────────────────────────────
        internal static void InitDriver()
        {
            SetupDriver(Constants.Browser.DefaultBrowser);
        }

        // ─────────────────────────────────────
        // SETUP — creates the correct browser
        // ─────────────────────────────────────
        private static void SetupDriver(BrowserType browserType)
        {
            switch (browserType)
            {
                case BrowserType.Chrome:
                    var chromeOptions = new ChromeOptions();

                    if (Constants.Browser.Incognito)
                        chromeOptions.AddArgument("--incognito");

                    if (Constants.Browser.HeadLess)
                        chromeOptions.AddArgument("--headless");

                    chromeOptions.AddArgument(
                        $"--window-size={Constants.Browser.WindowWidth}," +
                        $"{Constants.Browser.WindowHeight}");

                    new DriverManager().SetUpDriver(
                        new ChromeConfig(),
                        VersionResolveStrategy.MatchingBrowser);

                    _driver.Value = new ChromeDriver(chromeOptions);
                    break;

                case BrowserType.Edge:
                    var edgeOptions = new EdgeOptions();

                    if (Constants.Browser.Incognito)
                        edgeOptions.AddArgument("--incognito");

                    if (Constants.Browser.HeadLess)
                        edgeOptions.AddArgument("--headless");

                    new DriverManager().SetUpDriver(
                        new EdgeConfig(),
                        VersionResolveStrategy.MatchingBrowser);

                    _driver.Value = new EdgeDriver(edgeOptions);
                    _driver.Value.Manage().Window.Maximize();
                    break;

                case BrowserType.FireFox:
                    var firefoxOptions = new FirefoxOptions();

                    if (Constants.Browser.HeadLess)
                        firefoxOptions.AddArgument("--headless");

                    new DriverManager().SetUpDriver(
                        new FirefoxConfig(),
                        VersionResolveStrategy.MatchingBrowser);

                    _driver.Value = new FirefoxDriver(firefoxOptions);
                    _driver.Value.Manage().Window.Maximize();
                    break;

                default:
                    throw new InvalidOperationException(
                        $"Browser type '{browserType}' is not supported.");
            }

            // set page load and script timeouts
            _driver.Value.Manage().Timeouts().PageLoad =
                TimeSpan.FromSeconds(Constants.Timeouts.PageLoadingSecond);

            _driver.Value.Manage().Timeouts().AsynchronousJavaScript =
                TimeSpan.FromSeconds(Constants.Timeouts.ScriptFinishedSeconds);
        }

        // ─────────────────────────────────────
        // GET — returns current thread's driver
        // ─────────────────────────────────────
        internal static IWebDriver GetDriver()
        {
            return _driver.Value
                ?? throw new InvalidOperationException(
                    "Driver is not initialized. Call InitDriver() first.");
        }

        // ─────────────────────────────────────
        // QUIT — cleans up after each test
        // ─────────────────────────────────────
        internal static void QuitDriver()
        {
            try
            {
                var driver = GetDriver();
                driver?.Quit();
                driver?.Dispose();
                _driver.Value = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error quitting driver: {ex.Message}");
            }
        }
    }
}