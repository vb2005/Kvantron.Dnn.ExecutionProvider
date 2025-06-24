using Kvantron.Dnn.ExecutionProvider.Data;
using Kvantron.Utils.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvantron.Dnn.ExecutionProvider.Core
{
    public class InitProvider
    {
        public static void Init()
        {
            foreach (var m in LocalSettings.Instance.Models)
            {
                try
                {
                    DnnSession ds = new DnnSession(m);
                    Logger.AddEvent(0, $"Модель {m.Name} загружена", ErrorLevel.Information);
                    SessionSettings.Sessions.Add(m.Name, ds);
                }
                catch (Exception)
                {
                    Logger.AddEvent(0, $"Модель {m.Name} не может быть загружена", ErrorLevel.Warning);
                }
            }
        }
    }
}
