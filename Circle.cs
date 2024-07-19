class Circle{
    public XYZ Center { get; set; }
    public double Radius { get; set; }
    public XYZ normal { get;}

    public Circle(XYZ center, double radius, Arc arc)
    {
        Center = center;
        Radius = radius;
        XYZ normal = arc.GetPlane().Normal;
    }
}

public class IrregularCircle
{
    public List<Line> Lines { get; private set; }

    public IrregularCircle()
    {
        Lines = new List<Line>();
    }

    public void AddLine(XYZ start, XYZ end)
    {
        Line line = Line.CreateBound(start, end);
        Lines.Add(line);
    }

    public void PrintLines()
    {
        List<string> lineInfo = new List<string>();
        foreach (var line in Lines)
        {
            lineInfo.Add($"Start: {line.GetEndPoint(0)}, End: {line.GetEndPoint(1)}");
        }
        TaskDialog.Show("Irregular Circle Lines", string.Join("\n", lineInfo));
    }

    public bool IsClosed()
    {
        if (Lines.Count < 2) return false;

        XYZ firstStart = Lines[0].GetEndPoint(0);
        XYZ lastEnd = Lines[Lines.Count - 1].GetEndPoint(1);

        return firstStart.IsAlmostEqualTo(lastEnd);
    }
}