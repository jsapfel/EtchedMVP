using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightAngle : MonoBehaviour
{
    public Transform P1;
    public Transform P2;
    public Transform P3;

    public void Set(Transform p1, Transform p2, Transform p3)
    {
        P1 = p1;
        P2 = p2;
        P3 = p3;
    }
}
