using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    [TestFixture]
    public class FullTest
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
        public void MyFullTest()
        {
            //Основной тест
            driver.Navigate().GoToUrl("https://www.labirint.ru");

            //Локаторы
            var booksMenu = By.CssSelector("[data-toggle='header-genres']"); // Ссылка "Книги" в шапке 
            var allBooks = By.CssSelector(".b-menu-second-container [href='/books/']");  //Ссылка "Все книги" в раскрывшемся меню
            var addBookInCart = By.XPath("(//a[contains(@class,'btn')][contains(@class,'buy-link')])[1]"); //Кнопка "В корзину" или "Предзаказ" у первой книги
            var issueOrder = By.XPath("(//a[contains(@class,'buy-link')][contains(@class,'btn-more')])[1]"); //Кнопка "Оформить" у первой книги
            var beginOrder = By.CssSelector("#basket-default-begin-order"); //Кнопка "Начать оформление"
            var chooseCourierDelivery = By.CssSelector("[data-gaid='cart_dlcourier']"); //Галочка для выбора курьерской доставки
            var city = By.CssSelector("input[data-suggeststype='district']"); //Поле ввода названия города
            var cityError = By.CssSelector("span.b-form-error"); //Локатор ошибки о неизвестном городе
            var suggestedCity = By.CssSelector(".suggests-item-txt"); //Локатор подсказки названия города
            var street = By.CssSelector(".js-dlform-wrap input[data-suggeststype='streets']"); //Название улицы
            var building = By.CssSelector(".js-dlform-wrap [name^=building]"); //Номер дома
            var flat = By.CssSelector(".js-dlform-wrap [name^=flat]"); //Номер квартиры
            var confirm = By.CssSelector(".js-dlform-wrap [value=Готово]"); //Кнопка "Готово"
            var courierDeliveryLightbox = By.CssSelector(".js-dlform-wrap"); //Локатор лайтбокса курьерской доставки

            //Наводим на пункт "Книги" (локатор: booksMenu)
            new Actions(driver)
                .MoveToElement(driver.FindElement(booksMenu))
                .Build()
                .Perform();

            //Дожидаемся показа "Все книги" (локатор: allBooks)
            wait.Until(ExpectedConditions.ElementIsVisible(allBooks));

            //Кликаем по "Все книги" (локатор: allBooks)
            driver.FindElement(allBooks).Click();

            //Проверяем, что перешли на url https://www.labirint.ru/books/
            Assert.AreEqual("https://www.labirint.ru/books/", driver.Url, "Перешли на неверную страницу");

            //Кликаем у первой книги по кнопке "В корзину" (локатор: addBookInCart)
            driver.FindElement(addBookInCart).Click();

            //Кликаем у первой книги по кнопке "Оформить" (локатор: issueOrder)
            driver.FindElement(issueOrder).Click();

            //Кликаем по кнопке "Начать оформление" (локатор: beginOrder)
            driver.FindElement(beginOrder).Click();

            //Выбираем курьерскую доставку (локатор: chooseCourierDelivery)
            driver.FindElement(chooseCourierDelivery).Click();

            //Вводим город некорректный (локатор: city)
            driver.FindElement(city).SendKeys("saasdfsdfsdfdffds");

            //Убираем фокус с поля, например, кликаем Tab
            driver.FindElement(city).SendKeys(Keys.Tab);

            //Проверяем, что отобразилась ошибка "Неизвестный город" (локатор: cityError)
            Assert.IsTrue(driver.FindElement(cityError).Displayed, "Не появилась ошибка о неизвестном городе");

            //Очищаем поле ввода и вводим город Екатеринбург (локатор: city)
            driver.FindElement(city).Clear();
            driver.FindElement(city).SendKeys("Екатеринбург");

            //Кликаем по появившейся подсказке (локатор: suggestedCity)
            driver.FindElement(suggestedCity).Click();

            //Вводим название улицы (локатор: street)
            driver.FindElement(street).SendKeys("Ленина");

            //Вводим номер дома (локатор: building)
            driver.FindElement(building).SendKeys("1");

            //Вводим номер квартиры (локатор: flat)
            driver.FindElement(flat).SendKeys("1");

            //Подождать, когда появившийся лоадер подсчета даты ближайшей доставки скроется
            wait.Until(x => !IsLoaderVisible());

            //Указываем день доставки равный сегодня + 8 дней
            (driver as IJavaScriptExecutor).ExecuteScript($"$('.js-dlform-wrap .js-delivery-date').datepicker('setDate','{DateTime.Today.AddDays(8).ToString("dd.MM.yyyy")}')");

            //Подождать, когда появившийся лоадер подсчета даты ближайшей доставки скроется
            wait.Until(x => !IsLoaderVisible());

            //И кликаем по кнопке "Готово" (локатор: confirm)
            driver.FindElement(confirm).Click();

            //Проверяем, что лайтбокс курерской доставки не виден (локатор: courierDeliveryLightbox)
            Assert.IsFalse(driver.FindElement(courierDeliveryLightbox).Displayed, "Не скрылся лайтбокс курьерской доставки");
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

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver = null;
        }
    }
}