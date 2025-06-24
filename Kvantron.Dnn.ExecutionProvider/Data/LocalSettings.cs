
using Kvantron.Dnn.ExecutionProvider.Models;
using System.ComponentModel;
using System.Text.Json;
using static System.Environment;

namespace Kvantron.Dnn.ExecutionProvider.Data
{
    /// <summary>
    /// Список сохранённых моделей нейронок.
    /// Храни, где угодно. У меня в JSON. Можно в базе данных
    /// </summary>
    public class LocalSettings
    {
        public List<DnnModel> Models { get; set; } = new List<DnnModel>();


        #region Singleton

        private static LocalSettings _instance;
        public static LocalSettings Instance
        {
            get
            {
                if (_instance == null)
                {
                    try
                    {
                        var path = Path.Combine(GetFolderPath(SpecialFolder.CommonApplicationData), "Kogerent", "Settings.json");
                        _instance = JsonSerializer.Deserialize<LocalSettings>(File.ReadAllText(path));
                    }
                    catch (Exception)
                    {
                        _instance = new LocalSettings();
                    }
                }
                return _instance;
            }
        }

        [DisplayName("Граница температуры камер для оповещения")]
        [Description("Укажите температуру, при которой необходимо выдавать предупреждение о перегреве камеры")]
        [Category("Внешние устройства")]
        public float MaxTemperatureOnCameras { get; set; } = 85;
        public string AdminPassword { get; set; } = "default_0001";

        public LocalSettings()
        {

        }

        public void Save()
        {
            JsonSerializerOptions js = new JsonSerializerOptions();
            js.WriteIndented = true;
            string data = JsonSerializer.Serialize(this, js);
            var path = Path.Combine(GetFolderPath(SpecialFolder.CommonApplicationData), "Kogerent", "Settings.json");
            File.WriteAllText(path, data);

            //TODO: LOG
            //FileController.Send2("Settings Modified");

        }

        #endregion
    }
}
