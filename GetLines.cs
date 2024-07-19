public class GetSelectedLine : IExternalCommand
{
    public Result Execute(
        ExternalCommandData commandData, 
        ElementSet elements)
    {
        // Получение текущего документа
        UIDocument uiDoc = commandData.GetUIDocument();
        Document doc = uiDoc.Document;

        // Получение выбранных элементов
        ICollection<ElementId> selectedIds = uiDoc.Selection.GetElementIds();

        if (selectedIds.Count == 0)
        {
            TaskDialog.Show("Info", "Пожалуйста, выберите линию.");
            return Result.Failed;
        }

        // Обработка каждого выбранного элемента
        foreach (ElementId id in selectedIds)
        {
            Element element = doc.GetElement(id);
            // Проверка, является ли выбранный элемент линией
            if (element is ModelCurve modelCurve && modelCurve.GeometryCurve is Line line)
            {
                // Получение длины
                double length = line.Length;

                // Получение начальной и конечной точек
                XYZ startPoint = line.GetEndPoint(0);
                XYZ endPoint = line.GetEndPoint(1);
                /*
                // Вывод информации о линии
                TaskDialog.Show("Line Info", 
                    $"Длина: {length}\n" +
                    $"Начальная точка: {startPoint}\n" +
                    $"Конечная точка: {endPoint}");*/
            }
            else
            {
                TaskDialog.Show("Info", "Выбранный элемент не является линией.");
            }
        }

        return Result.Succeeded;
    }
}