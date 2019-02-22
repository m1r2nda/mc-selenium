using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests.Pages
{
    public class BasketPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        private By chooseCourierDelivery = By.CssSelector("[data-gaid='cart_dlcourier']");

        public BasketPage(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait;
        }

        public void ChooseCourierDelivery()
        {
            driver.FindElement(chooseCourierDelivery).Click();
        }
    }
}
