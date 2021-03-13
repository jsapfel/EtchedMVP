using UnityEngine;

public class Machine_Justify2 : MonoBehaviour
{
    public Transform P1; public Transform P2; public Transform P3;
    public LineSegment Use1S;
    public Transform Use2P1; public Transform Use2P2; public Transform Use2P3;
    public Transform Use3P1; public Transform Use3P2; public Transform Use3P3;

    public Transform EndPlatform;
    public Transform EndPlatformNewPos;
    
    public bool FirstUseDone;
    public bool SecondUseDone;

    private Vector2 it;
    private Vector2 itToP1;
    private Vector2 itToP3;
    private Vector2 itToOtherOnS;
    private Transform pOnLine2;
    private Transform pOffLine2;
    private Transform pOnLine3;
    private Transform pOffLine3;

    private void Start()
    {
        it = new Vector2(P2.position.x, P2.position.z);
        itToP1 = new Vector2(P1.position.x - it.x, P1.position.z - it.y);
        itToP3 = new Vector2(P3.position.x - it.x, P3.position.z - it.y);
    }

    public void Use1()
    {
        FirstUseDone = false;
        if (Use1S.Point1 == P2)
            itToOtherOnS = Use1S.P2 - it;
        else if (Use1S.Point2 == P2)
            itToOtherOnS = Use1S.P1 - it;
        else return;

        float a1 = Vector2.Angle(itToP1, itToOtherOnS);
        float a3 = Vector2.Angle(itToP3, itToOtherOnS);
        float dot = Vector2.Dot(itToP3, itToOtherOnS);
        if (Mathf.Approximately(a1, a3) && dot > 0)
            FirstUseDone = true;
    }
    
    public void Use2()
    {
        SecondUseDone = true;
        if (Use2P1 == Use2P2 || Use2P1 == Use2P3 || Use2P2 == Use2P3)
        {
            SecondUseDone = false;
            return;
        }

        Vector2 p1 = new Vector2(Use2P1.position.x, Use2P1.position.z);
        Vector2 p2 = new Vector2(Use2P2.position.x, Use2P2.position.z);
        Vector2 p3 = new Vector2(Use2P3.position.x, Use2P3.position.z);
        float p1d = Vector2.Dot(p1 - it, itToOtherOnS);
        float p2d = Vector2.Dot(p2 - it, itToOtherOnS);
        float p3d = Vector2.Dot(p3 - it, itToOtherOnS);

        if (Use2P1 == P2)
        {
            if (Mathf.Approximately(p2d, 1) && !Mathf.Approximately(Mathf.Abs(p3d), 1))
            {
                pOnLine2 = Use2P2;
                pOffLine2 = Use2P3;
                return;
            }
            if (Mathf.Approximately(p3d, 1) && !Mathf.Approximately(Mathf.Abs(p2d), 1))
            {
                pOnLine2 = Use2P3;
                pOffLine2 = Use2P2;
                return;
            }
        }
        else if (Use2P2 == P2)
        {
            if (Mathf.Approximately(p1d, 1) && !Mathf.Approximately(Mathf.Abs(p3d), 1))
            {
                pOnLine2 = Use2P1;
                pOffLine2 = Use2P3;
                return;
            }
            if (Mathf.Approximately(p3d, 1) && !Mathf.Approximately(Mathf.Abs(p1d), 1))
            {
                pOnLine2 = Use2P3;
                pOffLine2 = Use2P1;
                return;
            }
        }
        else if (Use2P3 == P2)
        {
            if (Mathf.Approximately(p1d, 1) && !Mathf.Approximately(Mathf.Abs(p2d), 1))
            {
                pOnLine2 = Use2P1;
                pOffLine2 = Use2P2;
                return;
            }
            if (Mathf.Approximately(p2d, 1) && !Mathf.Approximately(Mathf.Abs(p1d), 1))
            {
                pOnLine2 = Use2P2;
                pOffLine2 = Use2P1;
                return;
            }
        }
        SecondUseDone = false;
    }

    public void Use3()
    {
        if (Use3P1 == Use3P2 || Use3P1 == Use3P3 || Use3P2 == Use3P3) return;

        Vector2 p1 = new Vector2(Use3P1.position.x, Use3P1.position.z);
        Vector2 p2 = new Vector2(Use3P2.position.x, Use3P2.position.z);
        Vector2 p3 = new Vector2(Use3P3.position.x, Use3P3.position.z);
        float p1d = Vector2.Dot(p1 - it, itToOtherOnS);
        float p2d = Vector2.Dot(p2 - it, itToOtherOnS);
        float p3d = Vector2.Dot(p3 - it, itToOtherOnS);

        if (Use3P1 == P2)
        {
            if (Mathf.Approximately(p2d, 1) && !Mathf.Approximately(Mathf.Abs(p3d), 1))
            {
                pOnLine3 = Use3P2;
                pOffLine3 = Use3P3;
                Check();
            }
            if (Mathf.Approximately(p3d, 1) && !Mathf.Approximately(Mathf.Abs(p2d), 1))
            {
                pOnLine3 = Use3P3;
                pOffLine3 = Use3P2;
                Check();
            }
        }
        else if (Use3P2 == P2)
        {
            if (Mathf.Approximately(p1d, 1) && !Mathf.Approximately(Mathf.Abs(p3d), 1))
            {
                pOnLine3 = Use3P1;
                pOffLine3 = Use3P3;
                Check();
            }
            if (Mathf.Approximately(p3d, 1) && !Mathf.Approximately(Mathf.Abs(p1d), 1))
            {
                pOnLine3 = Use3P3;
                pOffLine3 = Use3P1;
                Check();
            }
        }
        else if (Use3P3 == P2)
        {
            if (Mathf.Approximately(p1d, 1) && !Mathf.Approximately(Mathf.Abs(p2d), 1))
            {
                pOnLine3 = Use3P1;
                pOffLine3 = Use3P2;
                Check();
            }
            if (Mathf.Approximately(p2d, 1) && !Mathf.Approximately(Mathf.Abs(p1d), 1))
            {
                pOnLine3 = Use3P2;
                pOffLine3 = Use3P1;
                Check();
            }
        }
    }

    private void Check()
    {
        FirstUseDone = false;
        SecondUseDone = false;
        
        if (pOnLine2 != pOnLine3 || pOffLine2 == pOffLine3) return;

        float distOffOn2 = Vector3.Distance(pOffLine2.position, pOnLine2.position);
        float distOffOn3 = Vector3.Distance(pOffLine3.position, pOnLine3.position);
        if (!Mathf.Approximately(distOffOn2, distOffOn3)) return;
        float distOffIt2 = Vector3.Distance(pOffLine2.position, P2.position);
        float distOffIt3 = Vector3.Distance(pOffLine3.position, P2.position);
        if (Mathf.Approximately(distOffIt2, distOffIt3))
            Justified();
    }

    private void Justified()
    {
        //Done = true;
        EndPlatform.position = EndPlatformNewPos.position;
    }
}
