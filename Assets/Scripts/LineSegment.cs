using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSegment : MonoBehaviour
{
    public Transform Point1;
    public Transform Point2;
    public Vector2 P1;
    public Vector2 P2;
    public float Length;

    public void Set(Transform t1, Transform t2, Vector2 p1, Vector2 p2, float l)
    {
        Point1 = t1;
        Point2 = t2;
        P1 = p1;
        P2 = p2;
        Length = l;
    }
}
