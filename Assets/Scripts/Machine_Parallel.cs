using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Machine_Parallel : MonoBehaviour
{
    public LevelManager Manager;
    public LineSegment Segment;

    public void NewLine(Transform t)
    {
        Vector2 p = new Vector2(t.position.x, t.position.z);
        bool v = Mathf.Approximately(Segment.P1.x, Segment.P2.x);
        if (v && Mathf.Approximately(p.x, Segment.P2.x)) return;
        float m = (Segment.P2.y - Segment.P1.y) / (Segment.P2.x - Segment.P1.x);
        float b0 = p.y - m * p.x;
        if (!v && Mathf.Approximately(Segment.P2.y - m * Segment.P2.x, b0)) return;
        
        bool isNew = true;
        List<Vector2> newPoints = new List<Vector2>();
        float a = v ? 1 : m;
        int b = v ? 0 : -1;
        float c = v ? -p.x : b0;
        
        foreach (var l in Manager.Lines)
        {
            bool sameSlope = l.B == b && Mathf.Approximately(l.A, a);
            if (sameSlope && Mathf.Approximately(l.C, c))
            {
                isNew = false;
                break;
            }

            if (sameSlope) continue;
            Vector2 np = new Vector2((b * l.C - l.B * c) / (a * l.B - l.A * b),
                (l.A * c - a * l.C) / (a * l.B - l.A * b));
            if (np.x < 0 && np.x > -50 && np.y < 25 && np.y > -25 && newPoints.All(p0 => p0 != np))
                newPoints.Add(np);
        }
        if (isNew)
            Manager.NewLine(a, b, c, Segment, t, newPoints);
    }
}
