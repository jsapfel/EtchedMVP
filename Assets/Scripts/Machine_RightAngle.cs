using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine_RightAngle : MonoBehaviour
{
    public LevelManager Manager;
    public Transform P1;
    public Transform P2;
    public RightAngle RAPrefab;

    public void NewRA(Transform P3)
    {
        if (P1 == P2 || P1 == P3 || P2 == P3) return;
        Vector3 v1 = P1.position - P2.position;
        Vector3 v3 = P3.position - P2.position;

        if (!Mathf.Approximately(0, Vector3.Dot(v1, v3))) return;

        bool isNew = true;
        foreach (var ra in Manager.RAs)
        {
            if (ra.P2 != P2) continue;
            if (ra.P1 == P1 && ra.P3 == P3 || ra.P1 == P3 && ra.P3 == P1)
            {
                isNew = false;
                break;
            }
        }

        if (isNew)
        {
            Vector3 dir = Vector3.SignedAngle(v3, v1, Vector3.up) > 0 ? v3 : v1;
            RightAngle RA = Instantiate(RAPrefab, P2.position + 0.033f * Vector3.up, Quaternion.LookRotation(dir));
            RA.Set(P1, P2, P3);
            Manager.RAs.Add(RA);
        }
    }
}
