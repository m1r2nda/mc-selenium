using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    [TestFixture]
    public class SeleniumTests
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void SetUp()
        {
            //var options = new ChromeOptions();
            //options.AddArgument("--start-maximized"); // браузер раскрывается на весь экран
            //driver = new ChromeDriver(options);
			driver = new ChromeDriverSpecificVersion().Create();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5)); //явные ожидания
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5); //неявные ожидания
        }

        [Test]
        public void MyFirstSeleniumTest()
        {
            driver.Navigate().GoToUrl("https://ru.wikipedia.org/");
            IWebElement queryInput = driver.FindElement(By.Name("search"));
            IWebElement searchButton = driver.FindElement(By.Name("go"));
            queryInput.SendKeys("Selenium");
            searchButton.Click();

            Assert.That(driver.Title.Contains("Selenium — Википедия"), "Перешли на неверную страницу");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver = null;
        }
    }
}
