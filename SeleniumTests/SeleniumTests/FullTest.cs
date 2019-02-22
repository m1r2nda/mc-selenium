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
            var basketPage = homePage.AddBookToCart();
            basketPage.ChooseCourierDelivery();

            //Вводим несуществующий город
            basketPage.CourierDeliveryLightbox.AddCity("saasdfsdfsdfdffds", fromSuggest: false);

            Assert.IsTrue(basketPage.CourierDeliveryLightbox.IsInvalidCityErrorVisible, "Не появилась ошибка о неизвестном городе");

            basketPage.CourierDeliveryLightbox.AddCity("Екатеринбург");
            // Альтернативный вариант: basketPage.CourierDeliveryLightbox.City = "Екатеринбург";
            basketPage.CourierDeliveryLightbox.AddAddress("Ленина", "1", "1");
            basketPage.CourierDeliveryLightbox.SelectDate();
            basketPage.CourierDeliveryLightbox.Confirm();

            Assert.IsFalse(basketPage.CourierDeliveryLightbox.IsVisible, "Не скрылся лайтбокс");
        }
    }
}