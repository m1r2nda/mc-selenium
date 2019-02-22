using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests.Pages
{
    public class CourierDeliveryLightbox
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        private static By city = By.CssSelector("input[data-suggeststype='district']");
        private static By cityError = By.CssSelector("span.b-form-error");
        private static By street = By.CssSelector(".js-dlform-wrap input[data-suggeststype='streets']");
        private static By building = By.CssSelector(".js-dlform-wrap [name^=building]");
        private static By flat = By.CssSelector(".js-dlform-wrap [name^=flat]");
        private static By confirm = By.CssSelector(".js-dlform-wrap [value=Готово]");
        private static By lightbox = By.CssSelector(".js-dlform-wrap");
        private static By suggestedCity = By.CssSelector(".suggests-item-txt");

        public CourierDeliveryLightbox(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait;
        }

        public void AddCity(string cityName, bool fromSuggest = true)
        {
            driver.FindElement(city).Clear();
            driver.FindElement(city).SendKeys(cityName);
            if(fromSuggest) { driver.FindElement(suggestedCity).Click(); }
            else driver.FindElement(city).SendKeys(Keys.Enter);
        }
        
        public void AddAddress(string streetName, string buildingNumber, string flatNumber)
        {
            driver.FindElement(street).SendKeys(streetName);
            driver.FindElement(building).SendKeys(buildingNumber);
            driver.FindElement(flat).SendKeys(flatNumber);
        }

        public void SelectDate()
        {
            //Подождать, когда появившийся лоадер подсчета даты ближайшей доставки скроется
            wait.Until(x => !IsLoaderVisible());

            (driver as IJavaScriptExecutor).ExecuteScript($"$('.js-dlform-wrap .js-delivery-date').datepicker('setDate','{DateTime.Today.AddDays(8).ToString("dd.MM.yyyy")}')");
        }

        public void Confirm()
        {
            //Подождать, когда появившийся лоадер подсчета даты ближайшей доставки скроется
            wait.Until(x => !IsLoaderVisible());

            driver.FindElement(confirm).Click();
        }

        private bool IsLoaderVisible()
        {
            try
            {
                var loader = By.ClassName("loading-panel");
                return driver.FindElement(loader).Displayed;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IsInvalidCityErrorVisible()
        {
            return driver.FindElement(cityError).Displayed;
        }

        public bool IsVisible()
        {
            return driver.FindElement(lightbox).Displayed;
        }
    }
}
