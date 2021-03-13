using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Machine_LineSegment : MonoBehaviour
{
    public LevelManager Manager;
    public Transform NewPoint;

    public void NewSegment(Transform t)
    {
        if (NewPoint == t) return;
        bool isNew = true;
        List<Vector2> newPoints = new List<Vector2>();
        Vector2 p1 = new Vector2(NewPoint.position.x, NewPoint.position.z);
        Vector2 p2 = new Vector2(t.position.x, t.position.z);
        Vector2 s1 = new Vector2(p2.x - p1.x, p2.y - p1.y);
        bool v1 = Mathf.Approximately(s1.x, 0);
        float a1 = v1 ? 1 : s1.y / s1.x;
        //int b1 = v1 ? 0 : -1;
        //float c1 = -(b1 * p2.y + a1 * p2.x);

        foreach (var s in Manager.Segments)
        {
            if (s.P1 == p1 && s.P2 == p2 || s.P2 == p1 && s.P1 == p2)
            {
                isNew = false;
                break;
            }
            
            Vector2 s2 = new Vector2(s.P2.x - s.P1.x, s.P2.y - s.P1.y);
            bool v2 = Mathf.Approximately(s2.x, 0);
            if (v1 && v2 || !v1 && !v2 && Mathf.Approximately(s1.y/s1.x, s2.y/s2.x))
                continue;
            float t1 = (-s1.y * (p1.x - s.P1.x) + s1.x * (p1.y - s.P1.y)) / (-s2.x * s1.y + s1.x * s2.y);
            float t2 = (s2.x * (p1.y - s.P1.y) - s2.y * (p1.x - s.P1.x)) / (-s2.x * s1.y + s1.x * s2.y);
            if (t1 > 0 && t1 < 1 && t2 > 0 && t2 < 1)
            {
                Vector2 p = new Vector2(p1.x + t2 * s1.x, p1.y + t2 * s1.y);
                if (newPoints.All(p0 => p0 != p))
                    newPoints.Add(p);
            }

            /*int b2 = v2 ? 0 : -1;
                float c2 = -(b2 * s.P2.y + a2 * s.P2.x);
                Vector2 p = new Vector2((b1 * c2 - b2 * c1) / (a1 * b2 - a2 * b1),
                    (a2 * c1 - a1 * c2) / (a1 * b2 - a2 * b1));
                if (newPoints.All(n => n != p))
                {
                    bool inNew = p.x < p2.x && p.x > p1.x || p.x < p1.x && p.x > p2.x
                        || p.y < p2.y && p.y > p1.y || p.y < p1.y && p.y > p2.y;
                    bool inS = p.x < s.P2.x && p.x > s.P1.x || p.x < p1.x && p.x > p2.x
                        || p.y < p2.y && p.y > p1.y || p.y < p1.y && p.y > p2.y;
                    newPoints.Add(p);*/
        }

        if (isNew)
            Manager.NewSegment(NewPoint, t, newPoints);
    }
}