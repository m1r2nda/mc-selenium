using NUnit.Framework;
using SeleniumTests.Pages;

namespace SeleniumTests
{
    [TestFixture]
    public class FullTest: SeleniumTestBase
    {
        [Test]
        public void MyFullTest()
        {
            //Основной тест
            var homePage = new HomePage(driver, wait);
            homePage.OpenPage();
            homePage.AddBookToCart();
            var basketPage = new BasketPage(driver, wait);
            basketPage.ChooseCourierDelivery();

            //Вводим несуществующий город
            var lightbox = new CourierDeliveryLightbox(driver, wait);
            lightbox.AddCity("saasdfsdfsdfdffds", fromSuggest: false);

            Assert.IsTrue(lightbox.IsInvalidCityErrorVisible(), "Не появилась ошибка о неизвестном городе");

            lightbox.AddCity("Екатеринбург");
            lightbox.AddAddress("Ленина", "1", "1");
            lightbox.SelectDate();
            lightbox.Confirm();

            Assert.IsFalse(lightbox.IsVisible(), "Не скрылся лайтбокс");
        }
    }
}