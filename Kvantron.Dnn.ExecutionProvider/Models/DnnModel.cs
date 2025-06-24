using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvantron.Dnn.ExecutionProvider.Models
{
    public class DnnModel
    {
        /// <summary>
        /// Первичный ключ для модели DNN
        /// </summary>
        [Key] public int Id { get; set; }

        /// <summary>
        /// Имя модели (внутренее)
        /// </summary>
        public string Name { get; set; }   

        /// <summary>
        /// Описание модели для удобста представления
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Тип модели
        /// </summary>
        public ModelType Type { get; set; } 

        /// <summary>
        /// Тип исполняемого устройства
        /// </summary>
        public ExecutionProvider Provider { get; set; } = ExecutionProvider.Cuda;

        /// <summary>
        /// Номер исполняемого устройства. В дальнейшем можно передавать через балансировщик
        /// </summary>
        public int DeviceId { get; set; } = 0;

        /// <summary>
        /// Размер входного тензора для кастомных моделей
        /// </summary>
        public List<int> InputTensorSize { get; set; } = new List<int>();

        /// <summary>
        /// Размер выходного тензора для кастомных моделей
        /// </summary>
        public List<int> OutputTensorSize { get; set; } = new List<int>();

        /// <summary>
        /// Имя файлов с нейронками
        /// </summary>
        public List<DnnFile> DnnFiles { get; set; } = new List<DnnFile>();
    }

    public enum ExecutionProvider
    {
        Cpu, Cuda
    }

    public enum ModelType 
    {
        Segmentation, Classification, Detection, Obb, Pose, Custom
    }
}
