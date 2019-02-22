using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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

            //Дожидаемся показа "Все книги" (локатор: allBooks)

            //Кликаем по "Все книги" (локатор: allBooks)

            //Проверяем, что перешли на url https://www.labirint.ru/books/

            //Кликаем у первой книги по кнопке "В корзину" (локатор: addBookInCart)

            //Кликаем у первой книги по кнопке "Оформить" (локатор: issueOrder)

            //Кликаем по кнопке "Начать оформление" (локатор: beginOrder)

            //Выбираем курьерскую доставку (локатор: chooseCourierDelivery)

            //Вводим город некорректный (локатор: city)

            //Убираем фокус с поля, например, кликаем Tab

            //Проверяем, что отобразилась ошибка "Неизвестный город" (локатор: cityError)

            //Очищаем поле ввода и вводим город Екатеринбург (локатор: city)

            //Кликаем по появившейся подсказке (локатор: suggestedCity)

            //Вводим название улицы (локатор: street)

            //Вводим номер дома (локатор: building)

            //Вводим номер квартиры (локатор: flat)

            //Подождать, когда появившийся лоадер подсчета даты ближайшей доставки скроется

            //Указываем день доставки равный сегодня + 8 дней

            //Подождать, когда появившийся лоадер подсчета даты ближайшей доставки скроется

            //И кликаем по кнопке "Готово" (локатор: confirm)

            //Проверяем, что лайтбокс курерской доставки не виден (локатор: courierDeliveryLightbox)
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver = null;
        }
    }
}