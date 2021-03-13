using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Machine_Extend : MonoBehaviour
{
    public LevelManager Manager;

    public void NewLine(LineSegment s)
    {
        bool v = Mathf.Approximately(s.P1.x, s.P2.x);
        float a = v ? 1 : (s.P2.y - s.P1.y) / (s.P2.x - s.P1.x);
        int b = v ? 0 : -1;
        float c = -(b * s.P2.y + a * s.P2.x);
        
        bool isNew = true;
        List<Vector2> newPoints = new List<Vector2>();

        foreach (var l in Manager.Lines)
        {
            bool sameSlope = l.B == b && Mathf.Approximately(l.A, a);
            if (sameSlope && Mathf.Approximately(l.C, c))
            {
                isNew = false;
                break;
            }

            if (sameSlope) continue;
            Vector2 p = new Vector2((b * l.C - l.B * c) / (a * l.B - l.A * b),
                (l.A * c - a * l.C) / (a * l.B - l.A * b));
            if (p.x < 0 && p.x > -50 && p.y < 25 && p.y > -25 && newPoints.All(p0 => p0 != p))
                newPoints.Add(p);
        }
        if (isNew)
            Manager.NewLine(a, b, c, s, s.Point2, newPoints);
    }
}
