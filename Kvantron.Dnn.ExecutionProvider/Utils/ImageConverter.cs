using OpenCvSharp;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvantron.Dnn.ExecutionProvider.Utils
{
    public class ImageConverter
    {
        /// <summary>
        /// Конвертация изображения из Cv.Mat в SkImage
        /// </summary>
        public static SKImage ConvertMatToSkImage(Mat openCvImage)
        {
            // Проверяем, что изображение не пустое
            if (openCvImage == null || openCvImage.Empty())
                return null;

            // Получаем данные изображения в виде массива байтов
            byte[] imageData;
            Cv2.ImEncode(".png", openCvImage, out imageData);

            // Создаем SKImage из массива байтов
            using (var stream = new MemoryStream(imageData))
            {
                return SKImage.FromEncodedData(stream);
            }
        }
    }
}
