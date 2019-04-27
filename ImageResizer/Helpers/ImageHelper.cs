using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ImageResizer.Helpsers
{
    public interface IImageHelper
    {
        Image ResizeImage(Image image, int maxWidth, int maxHeight);
        bool IsImage(string imageName);
    }

    public class ImageHelper : IImageHelper
    {
        public Image ResizeImage(Image image, int maxWidth, int maxHeight)
        {
            var width = (double)maxWidth / image.Width;
            var height = (double)maxHeight / image.Height;
            var ratio = Math.Min(width, height);
        
            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);
        
            var bitMap = new Bitmap(newWidth, newHeight);
            var rectangle = new Rectangle(0, 0, bitMap.Width, bitMap.Height);
            var graphics = Graphics.FromImage(bitMap);
            graphics.DrawImage(image, rectangle);
        
            return bitMap;
        }

        public bool IsImage(string imageName)
        {
            var validExtensions = new List<string> { "jpg", "gif", "png", "jpeg" };
            return validExtensions.Any(x => imageName.ToLower().EndsWith(x.ToLower()));
        }
    }
}
