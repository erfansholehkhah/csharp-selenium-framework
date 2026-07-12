using AutomationFramework.Tests.Config;
using AutomationFramework.Tests.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomationFramework.Tests.Pages
{
    public class BasePage
    {
        // ─────────────────────────────────────
        // DRIVER
        // get from static Driver class
        // ─────────────────────────────────────
        protected static IWebDriver GetDriver() => Driver.GetDriver();

        // ─────────────────────────────────────
        // UI ACTION WRAPPERS
        // page objects call these directly
        // no _ui. prefix needed
        // ─────────────────────────────────────
        protected static void Click(
            By locator, WaitConfig? config = null) =>
            UIActions.Click(locator, config);

        protected static void TypeText(
            By locator, string text, WaitConfig? config = null) =>
            UIActions.TypeText(locator, text, config);

        protected static string GetText(
            By locator, WaitConfig? config = null) =>
            UIActions.GetText(locator, config);

        protected static bool IsDisplayed(By locator) =>
            UIActions.IsDisplayed(locator);

        protected static bool IsEnabled(By locator) =>
            UIActions.IsEnabled(locator);

        protected static string GetAttribute(
            By locator, string attribute, WaitConfig? config = null) =>
            UIActions.GetAttribute(locator, attribute, config);

        protected static void HoverOver(
            By locator, WaitConfig? config = null) =>
            UIActions.HoverOver(locator, config);

        protected static void ScrollToElement(
            By locator, WaitConfig? config = null) =>
            UIActions.ScrollToElement(locator, config);

        protected static void JavaScriptClick(
            By locator, WaitConfig? config = null) =>
            UIActions.JavaScriptClick(locator, config);

        protected static void DoubleClick(
            By locator, WaitConfig? config = null) =>
            UIActions.DoubleClick(locator, config);

        protected static void RightClick(
            By locator, WaitConfig? config = null) =>
            UIActions.RightClick(locator, config);

        protected static void SelectDropdownByText(
            By locator, string text, WaitConfig? config = null) =>
            UIActions.SelectDropdownByText(locator, text, config);

        protected static void SelectDropdownByValue(
            By locator, string value, WaitConfig? config = null) =>
            UIActions.SelectDropdownByValue(locator, value, config);

        protected static void PressKey(
            By locator, string key, WaitConfig? config = null) =>
            UIActions.PressKey(locator, key, config);

        protected static void ClearField(
            By locator, WaitConfig? config = null) =>
            UIActions.ClearField(locator, config);

        protected static void DragAndDrop(
            By source, By target, WaitConfig? config = null) =>
            UIActions.DragAndDrop(source, target, config);

        protected static IList<IWebElement> GetAllElements(
            By locator, WaitConfig? config = null) =>
            UIActions.GetAllElements(locator, config);

        // ─────────────────────────────────────
        // NAVIGATION
        // ─────────────────────────────────────
        public static void NavigateTo(string url) =>
            Driver.GetDriver().Navigate().GoToUrl(url);

        public static string GetCurrentUrl() =>
            Driver.GetDriver().Url;

        public static string GetPageTitle() =>
            Driver.GetDriver().Title;

        public static void RefreshPage() =>
            Driver.GetDriver().Navigate().Refresh();

        public static void NavigateBack() =>
            Driver.GetDriver().Navigate().Back();

        public static void NavigateForward() =>
            Driver.GetDriver().Navigate().Forward();

        // ─────────────────────────────────────
        // PAGE VERIFICATION
        // ─────────────────────────────────────
        public static bool IsOnPage(string expectedUrlPart) =>
            Driver.GetDriver().Url.Contains(expectedUrlPart);

        public static bool WaitForUrlToContain(
            string partialUrl, WaitConfig? config = null)
        {
            try
            {
                var waitConfig = config ?? WaitConfig.Default;
                var wait = new WebDriverWait(
                    Driver.GetDriver(),
                    waitConfig.Timeout);
                return wait.Until(d =>
                    d.Url.Contains(partialUrl));
            }
            catch
            {
                return false;
            }
        }

        // ─────────────────────────────────────
        // ALERTS
        // ─────────────────────────────────────
        public static void AcceptAlert() =>
            Driver.GetDriver().SwitchTo().Alert().Accept();

        public static void DismissAlert() =>
            Driver.GetDriver().SwitchTo().Alert().Dismiss();

        public static string GetAlertText() =>
            Driver.GetDriver().SwitchTo().Alert().Text;

        // ─────────────────────────────────────
        // TABS
        // ─────────────────────────────────────
        public static void SwitchToTab(int tabIndex)
        {
            var tabs = Driver.GetDriver().WindowHandles.ToList();
            Driver.GetDriver().SwitchTo().Window(tabs[tabIndex]);
        }

        public static void SwitchToNewTab()
        {
            var tabs = Driver.GetDriver().WindowHandles.ToList();
            Driver.GetDriver().SwitchTo()
                .Window(tabs[tabs.Count - 1]);
        }

        public static void CloseCurrentTab()
        {
            Driver.GetDriver().Close();
            SwitchToTab(0);
        }

        // ─────────────────────────────────────
        // IFRAMES
        // ─────────────────────────────────────
        public static void SwitchToIframe(By locator)
        {
            var iframe = Driver.GetDriver().FindElement(locator);
            Driver.GetDriver().SwitchTo().Frame(iframe);
        }

        public static void SwitchToMainContent() =>
            Driver.GetDriver().SwitchTo().DefaultContent();

        // ─────────────────────────────────────
        // SCREENSHOTS
        // ─────────────────────────────────────
        public static void TakeScreenshot(string fileName)
        {
            var screenshot = ((ITakesScreenshot)Driver.GetDriver())
                .GetScreenshot();

            string path = Path.Combine(
                Constants.Paths.ScreenshotDirectory,
                $"{fileName}_{DateTime.Now:yyyyMMdd_HHmmss}.png");

            Directory.CreateDirectory(
                Path.GetDirectoryName(path)!);
            screenshot.SaveAsFile(path);
            Console.WriteLine($"Screenshot saved: {path}");
        }
    }
}