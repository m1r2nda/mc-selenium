using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
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
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized"); // браузер раскрывается на весь экран
            driver = new ChromeDriver(options);
			//driver = new ChromeDriverSpecificVersion().Create();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5)); //явные ожидания
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5); //неявные ожидания
        }

        [Test]
        public void MyFirstSelect()
        {
            //Переходим на страницу Лабиринта "Вопрос-ответ"
            driver.Navigate().GoToUrl("https://www.labirint.ru/guestbook/");

            //Кликаем "Оставить сообщение"
            var writeMessage = driver.FindElement(By.CssSelector("#aone"));
            writeMessage.Click();

            //В поле "Имя" вводим свое имя
            var name = driver.FindElement(By.CssSelector("#a_writer input[name='name']"));
            name.SendKeys("Тестовое имя");

            //В поле "E-mail" вводим e-mail
            var email = driver.FindElement(By.CssSelector("#a_writer input[name='email']"));
            email.SendKeys("test@test.ru");

            //Кликаем по "получить ответ на e-mail"
            var sendAnswer = driver.FindElement(By.Id("send_answer"));
            sendAnswer.Click();

            // В поле с сообщением вводим текст
            var message = driver.FindElement(By.CssSelector("#guesttextarea"));
            message.SendKeys("Простой вопрос");

            //Очищаем поле с сообщением
            message.Clear();

            //В селекте выбираем тему вопроса двумя разными способами
            var themeElement = driver.FindElement(By.Name("theme"));
            var themeSelector = new SelectElement(themeElement);
            themeSelector.SelectByText("Конкурс");
            themeSelector.SelectByIndex(7);

            //Проверяем, что выбралась нужная тема
            Assert.AreEqual("Приложение", themeSelector.SelectedOption.Text.Trim(), "Неверно выбран элемент на странице в теме");

            // Кликаем по "Для чего он не предназначен"
            var hint = driver.FindElement(By.Id("hd"));
            hint.Click();

            //Проверяем, что открылась подсказка после клика по "Для чего он не предназначен"
            var lightbox = driver.FindElement(By.Id("notForGuestbook"));
            Assert.IsTrue(lightbox.Displayed, "Не отобразился лайтбокс после клика по \"Для чего он не предназначен\"");
        }

        [Test]
        public void MySecondSelect()
        {
            driver.Navigate().GoToUrl("https://www.metric-conversions.org/ru/length/feet-to-yards.htm");
            (driver as IJavaScriptExecutor).ExecuteScript("$(\"#incs\")[0].selectedIndex=1");
            (driver as IJavaScriptExecutor).ExecuteScript("$(\"#incs\")[0].dispatchEvent(new Event(\"change\"))");
        }

        [Test]
        public void MyFirstCalendar()
        {
            driver.Navigate().GoToUrl("https://www.labirint.ru/books/");
            driver.FindElement(By.XPath("(//*[contains(@class,'product')]//a[contains(@class,'buy-link')])[1]")).Click();
            driver.Navigate().GoToUrl("https://www.labirint.ru/cart/checkout/");
            driver.FindElement(By.Id("basket-default-begin-order")).Click();
            wait.Until(e => e.FindElement(By.XPath("//*[contains(@data-gaid,'cart_dlcourier')]")));
            driver.FindElement(By.XPath("//*[contains(@data-gaid,'cart_dlcourier')]")).Click();
            (driver as IJavaScriptExecutor).ExecuteScript($"$('.js-delivery-date').datepicker('setDate','{DateTime.Today.AddDays(8).ToString("dd.MM.yyyy")}')");
        }

        [Test]
        public void MySecondCalendar()
        {
            driver.Navigate().GoToUrl("http://jqueryui.com/datepicker/");
            var frame = driver.FindElement(By.ClassName("demo-frame"));
            driver.SwitchTo().Frame(frame);
            (driver as IJavaScriptExecutor).ExecuteScript($"$('#datepicker').datepicker('setDate','03/11/2018')");
        }

        [Test]
        public void MyFirstAdvancedInteractionsAPI()
        {
            var myLabirint = By.CssSelector(".b-header-b-personal-e-list .js-b-autofade-text");
            var registrationInLabirint = By.CssSelector(".b-header-b-personal-wrapper .popup-window.dropdown-block-opened a[data-sendto='registration']");
            var registrationLightbox = By.Id("registration");

            driver.Navigate().GoToUrl("https://www.labirint.ru/");
            new Actions(driver)
                .MoveToElement(driver.FindElement(myLabirint))
                .Build()
                .Perform();

            //Ждем, когда появится всплывающее окно при наведении и кликаем по "Вступить в Лабиринт"
            wait.Until(ExpectedConditions.ElementIsVisible(registrationInLabirint));
            new Actions(driver)
                .MoveToElement(driver.FindElement(registrationInLabirint))
                .Click()
                .Build()
                .Perform();

            Assert.IsTrue(driver.FindElement(registrationLightbox).Displayed, "Не отобразился лайтбокс регистрации");
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
