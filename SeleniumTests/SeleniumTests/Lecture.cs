using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    [TestFixture]
    public class Lecture
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void SetUp()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized"); // браузер раскрывается на весь экран
            driver = new ChromeDriver(options);
            //driver = new ChromeDriverSpecificVersion().Create();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5)); //явные ожидания
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5); //неявные ожидания
        }

        [Test]
        public void LectureTest()
        {
            //Selenium получает уникальный id
            //Здесь ищем элемент по имени класса
            driver.Navigate().GoToUrl("https://www.labirint.ru");
            var element = driver.FindElement(By.ClassName("b-header-b-menu-e-text"));
            element.Click();
            //попробуем что-нибудь сделать после обновления страницы и получаем ошибку
            driver.Navigate().Refresh();
            element.Click();
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver = null;
        }
    }
}
