using UnityEngine;

public class Machine_Justify1 : MonoBehaviour
{
    public LineSegment S0;
    public LineSegment Use1S1;
    public LineSegment Use1S2;
    public LineSegment Use2S1;
    public LineSegment Use2S2;

    public Transform EndPlatform;
    public Transform EndPlatformNewPos;
    
    public bool FirstUseDone;
    
    private bool use1S1IsS0;
    private bool use2S1IsS0;
    private Transform commonPoint;

    public void Use1()
    {
        FirstUseDone = false;
        if (Use1S1 == Use1S2) return;
        if (Use1S1 == S0)
            use1S1IsS0 = true;
        else if (Use1S2 == S0)
            use1S1IsS0 = false;
        else return;

        if (Use1S1.Point1 == Use1S2.Point1 || Use1S1.Point1 == Use1S2.Point2 || Use1S1.Point2 == Use1S2.Point1 ||
            Use1S1.Point2 == Use1S2.Point2)
            FirstUseDone = true;
    }
    
    public void Use2()
    {
        if (Use2S1 == Use2S2) return;
        if (Use2S1 == S0)
            use2S1IsS0 = true;
        else if (Use2S2 == S0)
            use2S1IsS0 = false;
        else return;

        if (Use2S1.Point1 == Use2S2.Point1 || Use2S1.Point1 == Use2S2.Point2 || Use2S1.Point2 == Use2S2.Point1 ||
            Use2S1.Point2 == Use2S2.Point2)
            Check();
    }
    private void Check()
    {
        FirstUseDone = false;
        
        if (use1S1IsS0 && use2S1IsS0)
        {
            if (Use1S2 == Use2S2) return;

            if (Use1S2.Point1 == Use2S2.Point1 || Use1S2.Point1 == Use2S2.Point2)
                commonPoint = Use1S2.Point1;
            else if (Use1S2.Point2 == Use2S2.Point1 || Use1S2.Point2 == Use2S2.Point2)
                commonPoint = Use1S2.Point2;
            else return;

            if (commonPoint == S0.Point1 || commonPoint == S0.Point2) return;

            if (!Mathf.Approximately(S0.Length, Use1S2.Length) || 
                !Mathf.Approximately(S0.Length, Use2S2.Length)) return;
            Justified();
            return;
        }

        if (use1S1IsS0 && !use2S1IsS0)
        {
            if (Use1S2 == Use2S1) return;
            
            if (Use1S2.Point1 == Use2S1.Point1 || Use1S2.Point1 == Use2S1.Point2)
                commonPoint = Use1S2.Point1;
            else if (Use1S2.Point2 == Use2S1.Point1 || Use1S2.Point2 == Use2S1.Point2)
                commonPoint = Use1S2.Point2;
            else return;

            if (commonPoint == S0.Point1 || commonPoint == S0.Point2) return;

            if (!Mathf.Approximately(S0.Length, Use1S2.Length) || 
                !Mathf.Approximately(S0.Length, Use2S1.Length)) return;
            Justified();
            return;
        }
        
        if (!use1S1IsS0 && !use2S1IsS0)
        {
            if (Use1S1 == Use2S1) return;
            
            if (Use1S1.Point1 == Use2S1.Point1 || Use1S1.Point1 == Use2S1.Point2)
                commonPoint = Use1S1.Point1;
            else if (Use1S1.Point2 == Use2S1.Point1 || Use1S1.Point2 == Use2S1.Point2)
                commonPoint = Use1S1.Point2;
            else return;

            if (commonPoint == S0.Point1 || commonPoint == S0.Point2) return;

            if (!Mathf.Approximately(S0.Length,Use1S1.Length) || 
                !Mathf.Approximately(S0.Length, Use2S1.Length)) return;
            Justified();
            return;
        }
        
        if (!use1S1IsS0 && use2S1IsS0)
        {
            if (Use1S1 == Use2S2) return; //bad
            
            if (Use1S1.Point1 == Use2S2.Point1 || Use1S1.Point1 == Use2S2.Point2)
                commonPoint = Use1S1.Point1;
            else if (Use1S1.Point2 == Use2S2.Point1 || Use1S1.Point2 == Use2S2.Point2)
                commonPoint = Use1S1.Point2;
            else return; //start over

            if (commonPoint == S0.Point1 || commonPoint == S0.Point2) return; //bad

            if (!Mathf.Approximately(S0.Length,Use1S1.Length) || 
                !Mathf.Approximately(S0.Length, Use2S2.Length)) return;
            Justified();
        }
    }

    private void Justified()
    {
        //Done = true;
        EndPlatform.position = EndPlatformNewPos.position;
    }
}