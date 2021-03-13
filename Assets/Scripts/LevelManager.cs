using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<Circle> Circles;
    public List<LineSegment> Segments;
    public List<Line> Lines;
    public List<RightAngle> RAs;
    public List<Vector2> Points;

    //Segments.AddRange(Object.FindObjectsOfType<LineSegment>());
    
    public float PointY;

    public Circle CirclePrefab;
    public LineSegment SegmentPrefab;
    public Line LinePrefab;
    public GameObject PointPrefab;

    private void Start()
    {
        foreach (var s in Segments)
        {
            Vector3 p1Pos = s.Point1.position;
            Vector3 p2Pos = s.Point2.position;
            Vector3 diff = p1Pos - p2Pos;
            float length = diff.magnitude;
            Vector3 midpoint = Vector3.Lerp(p1Pos, p2Pos, 0.5f);
            var t = s.transform;
            t.position = midpoint + 0.04f * Vector3.up;
            t.rotation = Quaternion.LookRotation(diff);
            t.localScale = new Vector3(0.3f, 0.1f, length);
            s.Length = length;
            s.P1 = new Vector2(p1Pos.x, p1Pos.z);
            s.P2 = new Vector2(p2Pos.x, p2Pos.z);
        }
    }
    
    public void NewCircle(Transform NewC, Vector2 newC, float r, List<Vector2> newPoints)
    {
        Circle c1 = Instantiate(CirclePrefab, NewC.position + 0.06f * Vector3.up, Quaternion.identity);
        c1.Set(r, NewC, newC);
        Circles.Add(c1);
        
        int len = Points.Count;
        foreach (var p in newPoints)
            TryAddPoint(p, len);
        foreach (var s in Segments)
            SegmentCircleIntersect(s.P1, s.P2, newC, r);
        foreach (var l in Lines)
            LineCircleIntersect(l.A, l.B, l.C, newC, r);
    }

    public void NewSegment(Transform t1, Transform t2, List<Vector2> newPoints)
    {
        var p1 = t1.position;
        var p2 = t2.position;
        Vector3 diff = p1 - p2;
        float length = diff.magnitude;
        Vector3 midpoint = Vector3.Lerp(p1, p2, 0.5f);
        LineSegment s = Instantiate(SegmentPrefab, midpoint + 0.04f * Vector3.up, Quaternion.LookRotation(diff));
        s.transform.localScale = new Vector3(0.3f, 0.1f, length);
        s.Set(t1, t2, new Vector2(p1.x, p1.z), new Vector2(p2.x, p2.z), length);
        Segments.Add(s);

        int len = Points.Count;
        foreach (var p in newPoints)
            TryAddPoint(p, len);
        foreach (var c in Circles)
            SegmentCircleIntersect(s.P1, s.P2, c.C, c.R);
        foreach (var l in Lines)
            SegmentLineIntersect(l.A, l.B, l.C, s.P1, s.P2);
    }

    public void NewLine(float a, int b, float c, LineSegment s, Transform t, List<Vector2> newPoints)
    {
        Vector3 diff = s.Point1.transform.position - s.Point2.transform.position;
        Line l = Instantiate(LinePrefab, t.position + 0.037f * Vector3.up, Quaternion.LookRotation(diff));
        l.Set(a, b, c);
        Lines.Add(l);
        
        int len = Points.Count;
        foreach (var p in newPoints)
            TryAddPoint(p, len);
        foreach (var cir in Circles)
            LineCircleIntersect(a, b, c, cir.C, cir.R);
        foreach (var seg in Segments)
            SegmentLineIntersect(a, b, c, seg.P1, seg.P2);
    }

    private Transform TryAddPoint(Vector2 p, int l)
    {
        for (int i = 0; i < l; ++i)
            if (Points[i] == p)
                return null;
        Points.Add(p);
        GameObject point = Instantiate(PointPrefab, new Vector3(p.x, PointY, p.y), Quaternion.identity);
        return point.transform;
    }
    private Transform TryAddPoint(Vector2 p)
    {
        if (Points.Any(ep => ep == p)) return null;
        Points.Add(p);
        GameObject point = Instantiate(PointPrefab, new Vector3(p.x, PointY, p.y), Quaternion.identity);
        return point.transform;
    }
    
   //public bool InPoints(Vector2 p1) { return Points.Any(p => p == p1); }
   private void SegmentCircleIntersect(Vector2 p1, Vector2 p2, Vector2 center, float r)
   {
       //Transform added1 = null;
       //Transform added2 = null;
       bool vertical = Mathf.Approximately(p2.x, p1.x);
       float a = vertical ? 1 : (p2.y - p1.y) / (p2.x - p1.x);
       int b = vertical ? 0 : -1;
       float c = b * (center.y - p2.y) - a * (p2.x - center.x);
       float f = a * a + b * b;
       Vector2 p = new Vector2(-a*c/f + center.x, -b*c/f + center.y);

       if (c * c > r * r * f + Mathf.Epsilon)
           return;
       if (Mathf.Approximately(c * c, r * r * f))
       {
           if (p.x < p2.x && p.x > p1.x || p.x < p1.x && p.x > p2.x 
            || p.y < p2.y && p.y > p1.y || p.y < p1.y && p.y > p2.y)
               TryAddPoint(p);
       }
       else
       {
           float d = r*r - c*c/f;
           float g = Mathf.Sqrt(d/f);
           Vector2 np1 = new Vector2(p.x + b*g, p.y - a*g);
           Vector2 np2 = new Vector2(p.x -b*g, p.y + a*g);
           if (np1.x < p2.x && np1.x > p1.x || np1.x < p1.x && np1.x > p2.x
            || np1.y < p2.y && np1.y > p1.y || np1.y < p1.y && np1.y > p2.y)
               TryAddPoint(np1);
           if (np2.x < p2.x && np2.x > p1.x || np2.x < p1.x && np2.x > p2.x
            || np2.y < p2.y && np2.y > p1.y || np2.y < p1.y && np2.y > p2.y)
               TryAddPoint(np2);
       }
   }

   private void LineCircleIntersect(float a, int b, float c0, Vector2 center, float r)
   {
       float c = c0 + b * center.y + a * center.x;
       float f = a * a + b * b;
       Vector2 p = new Vector2(-a*c/f + center.x, -b*c/f + center.y);

       if (c * c > r * r * f + Mathf.Epsilon)
           return;
       if (Mathf.Approximately(c * c, r * r * f))
           TryAddPoint(p);
       else
       {
           float d = r*r - c*c/f;
           float g = Mathf.Sqrt(d/f);
           TryAddPoint(new Vector2(p.x + b*g, p.y - a*g));
           TryAddPoint(new Vector2(p.x -b*g, p.y + a*g));
       }
   }

   private void SegmentLineIntersect(float a, int b, float c, Vector2 p1, Vector2 p2)
   {
       bool sv = Mathf.Approximately(p1.x, p2.x);
       if (sv && b == 0) return;
       float sm = (p2.y - p1.y) / (p2.x - p1.x);
       if (!sv && b != 0 && Mathf.Approximately(a, sm)) return;
        
       float sa = sv ? 1 : sm;
       int sb = sv ? 0 : -1;
       float sc = sv ? -p1.x : p1.y - sm * p1.x;
       
       Vector2 p = new Vector2((b * sc - sb * c) / (a * sb - sa * b),
           (sa * c - a * sc) / (a * sb - sa * b));
       if (p.x < p2.x && p.x > p1.x || p.x < p1.x && p.x > p2.x
        || p.y < p2.y && p.y > p1.y || p.y < p1.y && p.y > p2.y) 
           TryAddPoint(p);
   }
}
