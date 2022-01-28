using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPI_Lab2._4
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            FilteredElementCollector collector = new FilteredElementCollector(doc);
            ICollection<Element> этаж = collector.OfClass(typeof(Level)).ToElements();
            var query = from element in collector where element.Name == "Level 1" select element;

            List<Element> Этаж1 = query.ToList<Element>();
            ElementId ЭтажId = Этаж1[0].Id;
            ElementLevelFilter фильтр1этаж = new ElementLevelFilter(ЭтажId, false);
            collector = new FilteredElementCollector(doc);
            ICollection<Element> allVentsOnLevel1 = collector.OfClass(typeof(MEPCurve)).WherePasses(фильтр1этаж).ToElements();

            TaskDialog.Show("1 этаж:", allVentsOnLevel1.Count.ToString());

            List<Element> Этаж2 = query.ToList<Element>();
            ElementId ЭтажId2 = Этаж2[0].Id;
            ElementLevelFilter фильтр2этаж = new ElementLevelFilter(ЭтажId, false);
            collector = new FilteredElementCollector(doc);
            ICollection<Element> allVentsOnLevel2 = collector.OfClass(typeof(MEPCurve)).WherePasses(фильтр2этаж).ToElements();

            TaskDialog.Show("2 этаж:", allVentsOnLevel1.Count.ToString());

            return Result.Succeeded;
        }
    }
}
