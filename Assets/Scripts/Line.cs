using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public float A;
    public int B;
    public float C;

    public void Set(float a, int b, float c)
    {
        A = a;
        B = b;
        C = c;
    }
}
