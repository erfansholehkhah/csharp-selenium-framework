using AutomationFramework.Tests.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomationFramework.Tests.Utilities
{
    internal class Waits
    {
        // ─────────────────────────────────────
        // BUILD WAIT
        // private static — only used internally
        // builds WebDriverWait with config
        // ─────────────────────────────────────
        private static WebDriverWait GetWait(WaitConfig? config = null)
        {
            var waitConfig = config ?? WaitConfig.Default;
            var wait = new WebDriverWait(
                Driver.GetDriver(),
                waitConfig.Timeout)
            {
                PollingInterval = waitConfig.PollingIntervall
            };
            wait.IgnoreExceptionTypes(
                typeof(NoSuchElementException),
                typeof(StaleElementReferenceException));
            return wait;
        }

        // ─────────────────────────────────────
        // WAIT METHODS
        // internal static — called directly
        // without creating a Waits object
        // e.g. Waits.UntilElementExists(by)
        // ─────────────────────────────────────

        internal static IWebElement UntilElementExists(
            By locator, WaitConfig? config = null) =>
            GetWait(config).Until(drv =>
                drv.FindElement(locator));

        internal static IWebElement IsVisible(
            By locator, WaitConfig? config = null) =>
            GetWait(config).Until(drv =>
            {
                var element = drv.FindElement(locator);
                return element.Displayed ? element : null!;
            });

        internal static IWebElement IsClickable(
            By locator, WaitConfig? config = null) =>
            GetWait(config).Until(drv =>
            {
                var element = drv.FindElement(locator);
                return (element.Displayed && element.Enabled)
                    ? element : null!;
            });

        // ─────────────────────────────────────
        // LIST OF ELEMENTS WAITS
        // ─────────────────────────────────────

        // wait until all elements in a list are visible
        // used when you already have IList<IWebElement>
        internal static IList<IWebElement> WaitForAllElementsToBeVisible(
            IList<IWebElement> elements, WaitConfig? config = null) =>
            GetWait(config).Until(drv =>
            {
                try
                {
                    foreach (var element in elements)
                    {
                        if (!element.Displayed)
                            return null!;
                    }
                    return elements;
                }
                catch (StaleElementReferenceException)
                {
                    return null!;
                }
            });

        // wait until all elements found by locator are visible
        // used when you have a By locator
        internal static IList<IWebElement> WaitForAllElementsToBeVisible(
            By locator, WaitConfig? config = null) =>
            GetWait(config).Until(drv =>
            {
                try
                {
                    var elements = drv.FindElements(locator);

                    if (!elements.Any())
                        return null!;

                    foreach (var element in elements)
                    {
                        if (!element.Displayed)
                            return null!;
                    }
                    return elements;
                }
                catch (StaleElementReferenceException)
                {
                    return null!;
                }
            });

        // wait until all elements in a list are invisible
        internal static bool WaitForAllElementsToDisappear(
            IList<IWebElement> elements, WaitConfig? config = null) =>
            GetWait(config).Until(drv =>
            {
                try
                {
                    foreach (var element in elements)
                    {
                        if (element.Displayed)
                            return false;
                    }
                    return true;
                }
                catch (StaleElementReferenceException)
                {
                    // element gone from DOM
                    // counts as invisible
                    return true;
                }
            });

        internal static bool IsTextPresentInElement(
            By locator, string text, WaitConfig? config = null) =>
            GetWait(config).Until(drv =>
                drv.FindElement(locator).Text.Contains(text));

        internal static bool IsElementPresent(
            By locator, WaitConfig? config = null)
        {
            try
            {
                GetWait(config).Until(drv =>
                    drv.FindElement(locator));
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }
    }
}