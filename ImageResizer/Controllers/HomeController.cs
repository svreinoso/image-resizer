using ImageResizer.Helpsers;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace ImageResizer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IImageHelper _imageHelper;
        private readonly string _imageFolder = "~/Images/";
        private readonly string _imageExtention = ".png";

        public HomeController(IImageHelper imageHelper)
        {
            _imageHelper = imageHelper;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UploadImage(HttpPostedFileBase file)
        {
            if (file != null && _imageHelper.IsImage(file.FileName))
            {
                var sizes = new List<int> { 16, 32, 64, 128, 256, 512, 1024 };
                if (!Directory.Exists(Server.MapPath(_imageFolder))) Directory.CreateDirectory(Server.MapPath(_imageFolder));

                var originalImageName = $"image-original{_imageExtention}";
                string imageName = Path.GetFileName(originalImageName);
                string path = Path.Combine(Server.MapPath(_imageFolder), imageName);
                file.SaveAs(path);
                ViewBag.originalImageName = $"/Images/{originalImageName}";

                var image = Image.FromStream(file.InputStream, true, true);

                sizes.ForEach(x => Resize(image, x, x));

                ViewBag.sizes = sizes;

                ViewBag.hasImage = true;
                return View("Index");
            }

            ViewBag.errorMessage = "File is not an image.";
            return View("Index");
        }

        private void Resize(Image image, int width, int height)
        {
            var imageResized50x50 = _imageHelper.ResizeImage(image, width, height);
            var imageName = $"resizedImageName{width}x{height}";
            var imageResizedPath = Path.Combine(Server.MapPath(_imageFolder), imageName + _imageExtention);
            imageResized50x50.Save(imageResizedPath);
            ViewData[imageName] = $"/Images/{imageName}{_imageExtention}";
        }
    }
}
