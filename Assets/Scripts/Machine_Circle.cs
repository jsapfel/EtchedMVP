using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Machine_Circle : MonoBehaviour
{
    public LevelManager Manager;
    public Transform NewC;
    
    public void NewCircle(float r)
    {
        if (Mathf.Approximately(r, 0f)) return;
        
        bool isNew = true;
        List<Vector2> newPoints = new List<Vector2>();
        Vector2 newC = new Vector2(NewC.position.x, NewC.position.z);
        Vector2 diff = Vector2.zero;
        float d;
        
        foreach (var c in Manager.Circles)
        {
            if (c.C == newC && Mathf.Approximately(c.R, r))
            {
                isNew = false;
                break;
            }
            diff = c.C - newC;
            d = diff.magnitude;
            if (d > c.R + r + Mathf.Epsilon || d + Mathf.Epsilon < Mathf.Abs(c.R - r))
                continue;
            if (Mathf.Approximately(d, c.R + r) && 
                newPoints.All(n => n != newC + r/d * diff))
                newPoints.Add(newC + r/d * diff);
            else
            {
                float a = (r*r - c.R*c.R + d*d)/(2*d);
                float h = Mathf.Sqrt(r*r - a*a);
                Vector2 p = newC + a/d * diff;
                Vector2 v = h/d * diff;
                Vector2 n1 = new Vector2(p.x + v.y, p.y - v.x);
                Vector2 n2 = new Vector2(p.x - v.y, p.y + v.x);
                if (newPoints.All(n => n != n1))
                    newPoints.Add(n1);
                if (newPoints.All(n => n != n2))
                    newPoints.Add(n2);
            }
        }

        if (isNew)
            Manager.NewCircle(NewC, newC, r, newPoints);
    }
}