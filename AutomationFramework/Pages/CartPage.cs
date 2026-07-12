using AutomationFramework.Tests.Config;
using OpenQA.Selenium;

namespace AutomationFramework.Tests.Pages
{
    public class CartPage : BasePage
    {
        // ─────────────────────────────────────
        // LOCATORS
        // ─────────────────────────────────────
        private static readonly By _cartItems =
            By.ClassName("cart_item");
        private static readonly By _checkoutButton =
            By.Id("checkout");
        private static readonly By _continueShoppingButton =
            By.Id("continue-shopping");

        // ─────────────────────────────────────
        // CONSTRUCTOR
        // ─────────────────────────────────────
        public CartPage() { }

        // ─────────────────────────────────────
        // ACTIONS
        // ─────────────────────────────────────
        public void ClickOnProduct(string productName)
        {
            var productLink = By.XPath(
                $"//div[@class='inventory_item_name'" +
                $" and text()='{productName}']");
            Click(productLink);
        }

        public void ClickCheckout() =>
            Click(_checkoutButton);

        public void ClickContinueShopping() =>
            Click(_continueShoppingButton);

        public void RemoveProduct(string productName)
        {
            var removeButton = By.XPath(
                $"//div[text()='{productName}']" +
                $"/ancestor::div[@class='cart_item']" +
                $"//button[text()='Remove']");
            Click(removeButton);
        }

        // ─────────────────────────────────────
        // VERIFICATIONS
        // ─────────────────────────────────────
        public bool IsProductDisplayed(string productName)
        {
            var productLocator = By.XPath(
                $"//div[@class='inventory_item_name'" +
                $" and text()='{productName}']");
            return IsDisplayed(productLocator);
        }

        public bool IsCartEmpty() =>
            !IsDisplayed(_cartItems);

        public int GetCartItemCount() =>
            GetDriver().FindElements(_cartItems).Count;

        public bool IsOnCartPage() =>
            IsOnPage("/cart.html");
    }
}