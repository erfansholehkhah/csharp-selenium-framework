using AutomationFramework.Tests.Config;
using OpenQA.Selenium;

namespace AutomationFramework.Tests.Pages
{
    public class LoginPage : BasePage
    {
        // ─────────────────────────────────────
        // LOCATORS
        // private = only LoginPage needs these
        // ─────────────────────────────────────
        private static readonly By _usernameField =
            By.Id("user-name");
        private static readonly By _passwordField =
            By.Id("password");
        private static readonly By _loginButton =
            By.Id("login-button");
        private static readonly By _errorMessage =
            By.CssSelector("[data-test='error']");

        // ─────────────────────────────────────
        // CONSTRUCTOR
        // ─────────────────────────────────────
        public LoginPage() { }

        // ─────────────────────────────────────
        // ACTIONS
        // ─────────────────────────────────────
        public void EnterUsername(string username) =>
            TypeText(_usernameField, username);

        public void EnterPassword(string password) =>
            TypeText(_passwordField, password);

        public void ClickLoginButton() =>
            Click(_loginButton);

        // ─────────────────────────────────────
        // VERIFICATIONS
        // ─────────────────────────────────────
        public bool IsErrorMessageDisplayed() =>
            IsDisplayed(_errorMessage);

        public string GetErrorMessageText() =>
            GetText(_errorMessage);
    }
}