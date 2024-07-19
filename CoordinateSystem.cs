//public void XY() {}
public List<Line> XZ(List<Line> list) {
    for (Line line : list) {
        list.Z = list.Y;
    }
    return list;
}
public List<Line> YZ(List<Line> list) {
    for (Line line : list) {
        list.Z = list.X;
    }
    return list;
}
