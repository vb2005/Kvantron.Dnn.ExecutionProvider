using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoloDotNet.Models;

namespace Kvantron.Dnn.ExecutionProvider.Models.Result
{
    public class DetectionResult
    {
        public Label Label { get; set; }
        public double Confidence { get; set; }
        public Rectangle Roi { get; set; }
        public static IEnumerable<DetectionResult> Cast(IEnumerable<ObjectDetection> src)
        {
            foreach (ObjectDetection obj in src)
            {
                var o = new DetectionResult();
                o.Label = new Label() { Id = obj.Label.Index, Name = obj.Label.Name };
                o.Confidence = obj.Confidence;
                o.Roi = new Rectangle() { Left = obj.BoundingBox.Left, Top = obj.BoundingBox.Top, Width = obj.BoundingBox.Width, Height = obj.BoundingBox.Height };
                yield return o;
            }
        }
    }
}