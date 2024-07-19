using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using System;
using System.Collections.Generic;

[Transaction(TransactionMode.Manual)]
public class Convert2DTo3D : IExternalCommand
{
    public Result Execute(
      ExternalCommandData commandData, 
      ref string message, 
      ElementSet elements)
    {
        UIApplication uiapp = commandData.Application;
        Document doc = uiapp.ActiveUIDocument.Document;

        // Начало транзакции
        using (Transaction trans = new Transaction(doc, "Convert 2D to 3D"))
        {
            trans.Start();

            // Получение всех импортированных 2D-линий
            FilteredElementCollector collector = new FilteredElementCollector(doc)
                .OfClass(typeof(CurveElement));

            foreach (Element element in collector)
            {
                CurveElement curveElement = element as CurveElement;
                if (curveElement != null)
                {
                    Curve curve = curveElement.GeometryCurve;

                    // Преобразование 2D-линий в 3D-элементы (например, стены)
                    Line line = curve as Line;
                    if (line != null)
                    {
                        XYZ start = line.GetEndPoint(0);
                        XYZ end = line.GetEndPoint(1);

                        // Создание стены на основе линии
                        Wall.Create(doc, Line.CreateBound(start, end), false);
                    }
                }
            }

            trans.Commit();
        }

        return Result.Succeeded;
    }
}