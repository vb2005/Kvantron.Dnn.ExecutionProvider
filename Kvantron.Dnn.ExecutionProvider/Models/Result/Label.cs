using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvantron.Dnn.ExecutionProvider.Models.Result
{
    /// <summary>
    /// Метка результата
    /// </summary>
    public class Label
    {
        /// <summary>
        /// Числовой идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Понятное наименование
        /// </summary>
        public string Name { get; set; }
    }
}
