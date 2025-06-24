using Kvantron.Dnn.ExecutionProvider;
using Kvantron.Dnn.ExecutionProvider.Core;
using Kvantron.Dnn.ExecutionProvider.Data;
using Kvantron.Dnn.ExecutionProvider.Models;
using Kvantron.Utils.Logs;

// Костыль для заполнения начальной базы тестовыми нейронками
DnnFile f1 = new DnnFile() { FileName = @"C:\Users\Andrey\Desktop\РГРТУ\RL\4d_june_3.onnx" };
DnnModel m1 = new DnnModel() { Name = "Test", Description = "Тестовая", DeviceId = 0, Provider = ExecutionProvider.Cpu, Type = ModelType.Detection };
m1.DnnFiles.Add(f1);
LocalSettings.Instance.Models.Add(m1);


// Проверка запуска 2х копий программы
bool onlyInstance;
Mutex mtx = new Mutex(true, "Kvantron", out onlyInstance);
if (!onlyInstance)
{
    Logger.AddEvent(1002, "Запущено более одной копии программы. Текущая будет закрыта", ErrorLevel.CriticalError);
    Console.ReadLine();
    SessionSettings.ExitFlag = true;
    return;
}


// Запуск ядра системы
Logger.AddEvent(1001, "Сервер запускается...", ErrorLevel.Information);

// Старт серврера
Server.Start();

// Старт загрузки моделей в RAM
InitProvider.Init();

Console.ReadLine();