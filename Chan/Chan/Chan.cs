namespace Chan;

public class Chan
{
    // Векторное произведение: >0 — поворот влево, <0 — вправо, =0 — коллинеарно
    public static long Cross(Point o, Point a, Point b)
    {
        return (long)(a.X - o.X) * (b.Y - o.Y) - (long)(a.Y - o.Y) * (b.X - o.X);
    }

    // Graham Scan: находит выпуклую оболочку группы
    public static List<Point> GrahamScan(List<Point> points)
    {
        if (points.Count <= 1) return new List<Point>(points);

        points.Sort();
        var lower = new List<Point>();
        foreach (var p in points)
        {
            while (lower.Count >= 2 && Cross(lower[^2], lower[^1], p) <= 0)
                lower.RemoveAt(lower.Count - 1);
            lower.Add(p);
        }

        var upper = new List<Point>();
        for (int i = points.Count - 1; i >= 0; i--)
        {
            var p = points[i];
            while (upper.Count >= 2 && Cross(upper[^2], upper[^1], p) <= 0)
                upper.RemoveAt(upper.Count - 1);
            upper.Add(p);
        }

        lower.RemoveAt(lower.Count - 1);
        upper.RemoveAt(upper.Count - 1);
        lower.AddRange(upper);
        return lower;
    }

    // Поиск следующей точки по методу Jarvis March среди под-оболочек
    public static Point NextPoint(Point from, List<List<Point>> hulls)
    {
        Point? best = null;
        foreach (var hull in hulls)
        {
            if (hull.Count == 0) continue;
            // Бинарный поиск по направлению в выпуклой оболочке
            int n = hull.Count;
            int left = 0, right = n;
            while (right - left > 1)
            {
                int mid = (left + right) / 2;
                if (Cross(from, hull[mid], hull[(mid + 1) % n]) < 0)
                    right = mid;
                else
                    left = mid;
            }
            Point candidate = hull[left];

            if (best == null || Cross(from, best, candidate) < 0)
                best = candidate;
        }
        return best!;
    }

    public static List<Point> ChanHull(List<Point> points)
    {
        if (points.Count <= 1) return new List<Point>(points);

        for (int t = 1; ; t++)
        {
            int m = 1 << (1 << t);
            int numGroups = (points.Count + m - 1) / m;

            // Построение локальных оболочек
            var hulls = new List<List<Point>>();
            for (int i = 0; i < points.Count; i += m)
            {
                var group = points.GetRange(i, Math.Min(m, points.Count - i));
                hulls.Add(GrahamScan(group));
            }

            var hull = new List<Point>();
            Point start = points.Min(); // Нижняя точка
            hull.Add(start);

            for (int i = 0; i < m; i++)
            {
                Point next = NextPoint(hull[^1], hulls);
                if (next.Equals(hull[0]))
                    return hull;
                hull.Add(next);
            }

            // Не удалось завершить за m шагов — увеличиваем m
        }
    }
}