using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvantron.Dnn.ExecutionProvider.Models
{
    public class DnnFile
    {
        /// <summary>
        /// ID Модели (PK)
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Абсолютный путь к данной версии модели
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Версия файла
        /// </summary>
        public Version Version { get; set; } = new Version(0, 0);

        /// <summary>
        /// Дата обучения модели
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.Now;

    }
}
