using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    public Transform Center;
    public Vector2 C;
    public float R;

    public void Set(float r, Transform NewC, Vector2 newC)
    {
        transform.localScale = new Vector3(r, 1f, r);
        R = r;
        Center = NewC;
        C = newC;
    }
}
