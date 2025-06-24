using Kvantron.Dnn.ExecutionProvider.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenCvSharp;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Kvantron.Dnn.ExecutionProvider.Controllers
{
    [ApiController]
    public class DnnController : ControllerBase
    {
        [HttpGet]
        [Route("/api/ping")]
        public ActionResult<string> Ping()
        {
            return "Ok";
        }


        /// <summary>
        /// Запрос для пробрасывания результата по картинке
        /// </summary>
        /// <param name="name">Имя модели нейронной сети</param>
        /// <param name="uploadedFile">Картинка (.png, .jpg, .bmp)</param>
        /// <returns></returns>
        [HttpPost]
        [Route("/api/{name:minlength(3)}")]
        public async Task<ActionResult<string>> AddFile(string name, IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                // Читаем изображение
                MemoryStream ms = new MemoryStream();
                await uploadedFile.CopyToAsync(ms);
                ms.Position = 0;
                var img = SKImage.FromEncodedData(ms);
                if (img == null) return StatusCode(500, "Изображение не соответствует формату");

                // Находим модель по названию
                // Возвращаем результат её работы
                bool x = SessionSettings.Sessions.TryGetValue(name, out var session);

                if (x) 
                    return JsonSerializer.Serialize(session.Calculate(img));
                else 
                    return StatusCode(500, "Модель не найдена");
            }

            return StatusCode(500, "Изображение отсутствует");
        }
        
    }
}
