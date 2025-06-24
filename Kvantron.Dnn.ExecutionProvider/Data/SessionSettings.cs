using Kvantron.Dnn.ExecutionProvider.Core;

namespace Kvantron.Dnn.ExecutionProvider.Data
{
    /// <summary>
    /// Настройки текущего сеанса. Отчищаются после перезапуска программы
    /// </summary>
    public class SessionSettings
    {
        public static bool ExitFlag { get; set; } = false;

        /// <summary>
        /// Набор загруженных нейронок
        /// </summary>
        public static Dictionary<string, DnnSession> Sessions { get; set; } = new Dictionary<string, DnnSession>();
    }
}
