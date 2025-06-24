using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvantron.Dnn.ExecutionProvider.Models.Result
{
    /// <summary>
    /// Область интереса
    /// </summary>
    public class Rectangle
    {
        /// <summary>
        /// Левая граница
        /// </summary>
        public double Left { get; set; }

        /// <summary>
        /// Верхняя граница
        /// </summary>
        public double Top { get; set; }

        /// <summary>
        /// Ширина области интереса
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// Высота области интереса
        /// </summary>
        public double Height { get; set; }

        // TODO: Хотелось бы еще иметь X, Y, MidX, MidY
    }
}
