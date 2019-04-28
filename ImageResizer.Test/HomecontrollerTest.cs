using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using ImageResizer.Controllers;
using ImageResizer.Helpsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ImageResizer.Test
{
    [TestClass]
    public class HomecontrollerTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var mockImageHelper = new Mock<IImageHelper>();
            var homeController = new HomeController(mockImageHelper.Object);
            mockImageHelper.Setup(x => x.IsImage("image.png")).Returns(true);

            var byteBuffer = new byte[10];
            var rnd = new Random();
            rnd.NextBytes(byteBuffer);
            var testStream = new MemoryStream(byteBuffer);
            var file = new HttpPostedFileBaseMock(testStream, "test/content", "test-file.png");
            var view = homeController.UploadImage(file);

            Assert.IsInstanceOfType(view, typeof(ActionResult));
        }
    }
}
