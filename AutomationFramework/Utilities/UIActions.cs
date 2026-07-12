using AutomationFramework.Tests.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace AutomationFramework.Tests.Utilities
{
    internal class UIActions
    {
        // ─────────────────────────────────────
        // no more _waits instance
        // no more _driver passed in
        // we get driver from static Driver class
        // we call static Waits methods directly
        // ─────────────────────────────────────
        private static IWebDriver GetDriver() => Driver.GetDriver();

        // ─────────────────────────────────────
        // FIND
        // ─────────────────────────────────────
        internal static IWebElement FindElement(
            By locator, WaitConfig? config = null) =>
            Waits.UntilElementExists(locator, config);

        // ─────────────────────────────────────
        // CLICK
        // ─────────────────────────────────────
        internal static void Click(
            By locator, WaitConfig? config = null)
        {
            Waits.IsClickable(locator, config).Click();
        }

        internal static void JavaScriptClick(
            By locator, WaitConfig? config = null)
        {
            var element = Waits.IsVisible(locator, config);
            ((IJavaScriptExecutor)GetDriver())
                .ExecuteScript("arguments[0].click();", element);
        }

        internal static void DoubleClick(
            By locator, WaitConfig? config = null)
        {
            var element = Waits.IsVisible(locator, config);
            new Actions(GetDriver())
                .DoubleClick(element)
                .Perform();
        }

        internal static void RightClick(
            By locator, WaitConfig? config = null)
        {
            var element = Waits.IsVisible(locator, config);
            new Actions(GetDriver())
                .ContextClick(element)
                .Perform();
        }

        // ─────────────────────────────────────
        // TYPE
        // ─────────────────────────────────────
        internal static void TypeText(
            By locator, string text, WaitConfig? config = null)
        {
            var element = Waits.IsVisible(locator, config);
            element.Clear();
            element.SendKeys(text);
        }

        internal static void PressKey(
            By locator, string key, WaitConfig? config = null)
        {
            Waits.IsVisible(locator, config).SendKeys(key);
        }

        internal static void ClearField(
            By locator, WaitConfig? config = null)
        {
            Waits.IsVisible(locator, config).Clear();
        }

        // ─────────────────────────────────────
        // GET
        // ─────────────────────────────────────
        internal static string GetText(
            By locator, WaitConfig? config = null) =>
            Waits.IsVisible(locator, config).Text;

        internal static string GetAttribute(
            By locator, string attributeName, WaitConfig? config = null) =>
            Waits.IsVisible(locator, config)
                .GetAttribute(attributeName);

        // ─────────────────────────────────────
        // STATE CHECKS
        // ─────────────────────────────────────
        internal static bool IsDisplayed(By locator)
        {
            try
            {
                return GetDriver().FindElement(locator).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        internal static bool IsEnabled(By locator)
        {
            try
            {
                return GetDriver().FindElement(locator).Enabled;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        // ─────────────────────────────────────
        // DROPDOWNS
        // ─────────────────────────────────────
        internal static void SelectDropdownByText(
            By locator, string visibleText, WaitConfig? config = null)
        {
            var element = Waits.IsVisible(locator, config);
            new SelectElement(element).SelectByText(visibleText);
        }

        internal static void SelectDropdownByValue(
            By locator, string value, WaitConfig? config = null)
        {
            var element = Waits.IsVisible(locator, config);
            new SelectElement(element).SelectByValue(value);
        }

        // ─────────────────────────────────────
        // SCROLL
        // ─────────────────────────────────────
        internal static void ScrollToElement(
            By locator, WaitConfig? config = null)
        {
            var element = Waits.UntilElementExists(locator, config);
            ((IJavaScriptExecutor)GetDriver())
                .ExecuteScript(
                    "arguments[0].scrollIntoView(true);", element);
        }

        // ─────────────────────────────────────
        // HOVER
        // ─────────────────────────────────────
        internal static void HoverOver(
            By locator, WaitConfig? config = null)
        {
            var element = Waits.IsVisible(locator, config);
            new Actions(GetDriver())
                .MoveToElement(element)
                .Perform();
        }

        // ─────────────────────────────────────
        // DRAG AND DROP
        // ─────────────────────────────────────
        internal static void DragAndDrop(
            By sourceLocator, By targetLocator, WaitConfig? config = null)
        {
            var source = Waits.IsVisible(sourceLocator, config);
            var target = Waits.IsVisible(targetLocator, config);
            new Actions(GetDriver())
                .DragAndDrop(source, target)
                .Perform();
        }

        // ─────────────────────────────────────
        // LIST OF ELEMENTS
        // ─────────────────────────────────────
        internal static IList<IWebElement> GetAllElements(
            By locator, WaitConfig? config = null) =>
            Waits.WaitForAllElementsToBeVisible(locator, config);

        internal static IList<IWebElement> GetAllElements(
            IList<IWebElement> elements, WaitConfig? config = null) =>
            Waits.WaitForAllElementsToBeVisible(elements, config);
    }
}