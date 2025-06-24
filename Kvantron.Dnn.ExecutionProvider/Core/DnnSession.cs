using Kvantron.Dnn.ExecutionProvider.Models;
using Kvantron.Dnn.ExecutionProvider.Models.Result;
using Microsoft.ML.OnnxRuntime;
using OpenCvSharp;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoloDotNet;
using YoloDotNet.Enums;
using YoloDotNet.Extensions;
using YoloDotNet.Models;

namespace Kvantron.Dnn.ExecutionProvider.Core
{
    public class DnnSession
    {
        private Yolo _yolo = null;
        private InferenceSession _onnx = null;

        private DnnModel _model = null;
        private DnnFile _file = null;

        /// <summary>
        /// Значение уверенности для некоторых моделей
        /// </summary>
        public float Confidence { get; set; } = 0.25f;

        /// <summary>
        /// Значение уверенности для точек в сегментации
        /// </summary>
        public float PixelConfidence { get; set; } = 0.25f;

        /// <summary>
        /// IoU для подавления немаксимумов
        /// </summary>
        public float IoU { get; set; } = 0.7f;

        /// <summary>
        /// Метод для загрузки модели
        /// </summary>
        /// <param name="model">Сведения о модели</param>
        /// <param name="file">Сведения о версии. Если null - берется последняя</param>
        public DnnSession(DnnModel model, DnnFile file = null)
        {
            _model = model;
            if (file == null) if (model.DnnFiles.Count == 0) throw new Exception("Нет файлов, связанных с моделью");
            if (file == null) file = model.DnnFiles.OrderBy(s => s.Version).Last();

            switch (model.Type)
            {
                case Models.ModelType.Segmentation:
                    _yolo = new Yolo(new YoloOptions() { OnnxModel = file.FileName, Cuda = model.Provider == Models.ExecutionProvider.Cuda, GpuId = model.DeviceId, ModelType = YoloDotNet.Enums.ModelType.Segmentation });
                    break;
                case Models.ModelType.Classification:
                    _yolo = new Yolo(new YoloOptions() { OnnxModel = file.FileName, Cuda = model.Provider == Models.ExecutionProvider.Cuda, GpuId = model.DeviceId, ModelType = YoloDotNet.Enums.ModelType.Classification });
                    break;
                case Models.ModelType.Detection:
                    _yolo = new Yolo(new YoloOptions() { OnnxModel = file.FileName, Cuda = model.Provider == Models.ExecutionProvider.Cuda, GpuId = model.DeviceId, ModelType = YoloDotNet.Enums.ModelType.ObjectDetection });
                    break;
                case Models.ModelType.Obb:
                    _yolo = new Yolo(new YoloOptions() { OnnxModel = file.FileName, Cuda = model.Provider == Models.ExecutionProvider.Cuda, GpuId = model.DeviceId, ModelType = YoloDotNet.Enums.ModelType.ObbDetection });
                    break;
                case Models.ModelType.Pose:
                    _yolo = new Yolo(new YoloOptions() { OnnxModel = file.FileName, Cuda = model.Provider == Models.ExecutionProvider.Cuda, GpuId = model.DeviceId, ModelType = YoloDotNet.Enums.ModelType.PoseEstimation });
                    break;
                case Models.ModelType.Custom:
                    _onnx = new InferenceSession(file.FileName);
                    break;
                default:
                    throw new Exception("Заданный тип модели недоступен!");
            }
        }


        /// <summary>
        /// Метод для обсчёта картинки через нейросеть
        /// </summary>
        /// <param name="img">Входное изображение</param>
        /// <returns></returns>
        public object Calculate(SKImage img)
        {
            if (_yolo != null)
            {
                // TODO: Следовало бы сделать ресайз

                if (_model.Type == Models.ModelType.Segmentation)
                    return _yolo.RunSegmentation(img, Confidence, PixelConfidence, IoU); //TODO: Добавить конвертер

                if (_model.Type == Models.ModelType.Classification) 
                    return _yolo.RunClassification(img); 

                if (_model.Type == Models.ModelType.Obb)
                    return _yolo.RunObbDetection(img, Confidence, IoU); //TODO: Добавить конвертер

                if (_model.Type == Models.ModelType.Detection)
                    return DetectionResult.Cast(_yolo.RunObjectDetection(img, Confidence, IoU)).ToList();

                if (_model.Type == Models.ModelType.Pose)
                    return _yolo.RunPoseEstimation(img, Confidence, IoU); //TODO: Добавить конвертер

                if (_model.Type == Models.ModelType.Custom)
                    throw new NotImplementedException();
            }

            if (_onnx != null)
            {
                // Реализовать свои нейронки (не YOLO) через ONNX
            }

            throw new NotImplementedException();
        }
    }
}
