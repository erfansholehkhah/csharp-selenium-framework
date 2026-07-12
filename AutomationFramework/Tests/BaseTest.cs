using AutomationFramework.Tests.Config;
using AutomationFramework.Tests.Pages;
using AutomationFramework.Tests.Utilities;
using NUnit.Framework;

namespace AutomationFramework.Tests.Tests
{
    public class BaseTest
    {
        // ─────────────────────────────────────
        // PAGE OBJECTS
        // protected = available in ALL test
        // classes that inherit BaseTest
        // no new() needed in tests ever
        // ─────────────────────────────────────
        protected LoginPage LoginPage;
        protected HomePage HomePage;
        protected CartPage CartPage;

        // ─────────────────────────────────────
        // SETUP — runs before EVERY test
        // initializes driver + page objects
        // ─────────────────────────────────────
        [SetUp]
        public void Setup()
        {
            // start browser
            Driver.InitDriver();

            // initialize all page objects once
            // available in all test classes
            LoginPage = new LoginPage();
            HomePage = new HomePage();
            CartPage = new CartPage();
        }

        // ─────────────────────────────────────
        // TEARDOWN — runs after EVERY test
        // ─────────────────────────────────────
        [TearDown]
        public void Teardown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status ==
                NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                BasePage.TakeScreenshot(
                    TestContext.CurrentContext.Test.Name);
            }

            Driver.QuitDriver();
        }

        // ─────────────────────────────────────
        // LOGIN
        // protected = callable in any test
        // called directly in test body
        // ─────────────────────────────────────
        protected void Login(
            string username = Constants.Users.StandardUser,
            string password = Constants.Users.Password)
        {
            LoginPage.NavigateTo(Constants.URL.BaseUl);
            LoginPage.EnterUsername(username);
            LoginPage.EnterPassword(password);
            LoginPage.ClickLoginButton();
        }
    }
}