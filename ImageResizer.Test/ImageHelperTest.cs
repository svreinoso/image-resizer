using ImageResizer.Helpsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

namespace ImageResizer.Test
{
    [TestClass]
    public class ImageHelperTest
    {
        [TestMethod]
        public void ShouldCheckIfIsImageFile()
        {
            var imageHelper = new ImageHelper();
            var isImage = imageHelper.IsImage("hola.png");
            Assert.IsTrue(isImage);
            isImage = imageHelper.IsImage("IsNotAnImage");
            Assert.IsFalse(isImage);
            isImage = imageHelper.IsImage(null);
            Assert.IsFalse(isImage);
        }

        [TestMethod]
        public void ShouldResizeImage()
        {
            var imageHelper = new ImageHelper();
            var image = new Bitmap(1000, 1000);
            var resizeImage = imageHelper.ResizeImage(image, 100, 100);
            Assert.AreEqual(resizeImage.Width, 100);
            Assert.AreEqual(resizeImage.Height, 100);
            Assert.IsNotNull(resizeImage);

            resizeImage = imageHelper.ResizeImage(null, 100, 100);
            Assert.IsNull(resizeImage);
        }

    }
}
