using AutomationFramework.Tests.Config;
using NUnit.Framework;

namespace AutomationFramework.Tests.Tests
{
    [TestFixture]
    public class LoginTests : BaseTest
    {
        [Test, Category("Smoke")]
        public void Login_ValidCredentials_ShouldLoadHomePage()
        {
            Login();

            Assert.That(
                HomePage.IsProductListDisplayed(),
                Is.True,
                "Home page did not load after login");
        }

        [Test, Category("Regression")]
        public void Login_InvalidPassword_ShouldShowError()
        {
            Login();

            Assert.That(
                LoginPage.IsErrorMessageDisplayed(),
                Is.True,
                "Error message was not displayed");
        }

        [Test, Category("Regression")]
        public void Login_LockedOutUser_ShouldShowError()
        {
            Login();

            Assert.That(
                LoginPage.GetErrorMessageText(),
                Does.Contain("locked out"),
                "Locked out error was not displayed");
        }

        [Test, Category("Regression")]
        public void Login_EmptyCredentials_ShouldShowError()
        {
            Login();

            Assert.That(
                LoginPage.IsErrorMessageDisplayed(),
                Is.True,
                "Error message was not displayed");
        }
    }
}