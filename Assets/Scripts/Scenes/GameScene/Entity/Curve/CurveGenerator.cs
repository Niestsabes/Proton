using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveGenerator : MonoBehaviour
{
    public int curveSamplingCount = 1000;
    public float factorA = -6.0f;
    public float factorB = 8.0f;
    public float widthX = 10.0f;
    public bool isStatic = false;

    private LineRenderer line1;
    private LineRenderer line2;

    // Start is called before the first frame update
    void Start()
    {
        line1 =  transform.Find("Line1").GetComponent<LineRenderer>();
        line2 =  transform.Find("Line2").GetComponent<LineRenderer>();

        GenCurve();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStatic)
            GenCurve();
    }

    private void GenCurve()
    {
        var pt1 = new Vector3[curveSamplingCount * 2];
        var pt2 = new Vector3[curveSamplingCount * 2];
        int ptCnt1 = 0;
        int ptCnt2 = 0;
        bool hasSecondaryCurve = false;
        for (int i = 0; i < curveSamplingCount; i++)
        {
            float x = Mathf.Lerp(widthX, -widthX, (float)i / (float)curveSamplingCount);
            float y = CurveFunction(x);
            bool isValid = IsValid(y);
            Vector3 pt = new Vector3(x, y, 0.0f);
            if (isValid)
            {
                if (!hasSecondaryCurve)
                    pt1[ptCnt1++] = pt;
                else
                    pt2[ptCnt2++] = pt;
            }
            else if (ptCnt1 > 0)
            {                
                hasSecondaryCurve = true;
            }
        }

        line2.loop = ptCnt2 > 0;

        // add symetry
        ptCnt1 = GenInverse(pt1, ptCnt1);
        ptCnt2 = GenInverse(pt2, ptCnt2);

        line1.positionCount = ptCnt1;
        line1.SetPositions(pt1);     
        line2.positionCount = ptCnt2;
        line2.SetPositions(pt2);     
    }

    private int GenInverse(Vector3[] pts, int size)
    {
        Vector3 negVec = new Vector3(1.0f, -1.0f, 0.0f);
        for (int i = size - 1; i >= 0; --i)
            pts[size++] = Vector3.Scale(pts[i], negVec);

        return size;
    }

    private bool IsValid(float y)
    {
        return !float.IsNaN(y);
    }

    private float CurveFunction(float x)
    {
        return Mathf.Sqrt( x * x * x + factorA * x + factorB );
    }
}
