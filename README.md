# Kvantron.Dnn.ExecutionProvider

Работает на CPU и NVIDIA GPU. Поддерживает ONNX в форматах YOLO. Пока сделал только детекцию объектов. В перспективе добавить возможность запуска любых ONNX моделей. 

## Конвертация YOLO -> ONNX

Я делаю это в Google Colab следующими командами:

1. Ставим Ultralytics
   ```
   !pip install ultralytics
   ```

2. Читаем из файла модель и сохраняем её в файл
   ``` python
    from ultralytics import YOLO
    model = YOLO(r"last.pt")
    model.export(format="onnx") 
```

3. Загружаем к себе и profit
