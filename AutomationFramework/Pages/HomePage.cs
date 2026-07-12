using AutomationFramework.Tests.Config;
using OpenQA.Selenium;

namespace AutomationFramework.Tests.Pages
{
    public class HomePage : BasePage
    {
        // ─────────────────────────────────────
        // LOCATORS
        // ─────────────────────────────────────
        private static readonly By _cartIcon =
            By.ClassName("shopping_cart_link");
        private static readonly By _productList =
            By.ClassName("inventory_list");
        private static readonly By _cartBadge =
            By.ClassName("shopping_cart_badge");
        private static readonly By _menuButton =
            By.Id("react-burger-menu-btn");

        // ─────────────────────────────────────
        // CONSTRUCTOR
        // ─────────────────────────────────────
        public HomePage() { }

        // ─────────────────────────────────────
        // ACTIONS
        // ─────────────────────────────────────
        public void ClickOnCartPage() =>
            Click(_cartIcon);

        public void ClickMenuButton() =>
            Click(_menuButton);

        public void AddProductToCart(string productName)
        {
            var addToCartButton = By.XPath(
                $"//div[text()='{productName}']" +
                $"/ancestor::div[@class='inventory_item']" +
                $"//button");
            Click(addToCartButton);
        }

        // ─────────────────────────────────────
        // VERIFICATIONS
        // ─────────────────────────────────────
        public bool IsProductListDisplayed() =>
            IsDisplayed(_productList);

        public string GetCartItemCount() =>
            GetText(_cartBadge);
    }
}