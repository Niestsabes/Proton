using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceController : MonoBehaviour
{
    public Vector2 minMaxA = new Vector2(-50.0f, 50.0f);
    public Vector2 minMaxB = new Vector2(-50.0f, 50.0f);
    public float steppingCurve = 10.0f;
    public float steppingKnob = 10.0f;
    public float winThreshold = 10.0f;

    private CurveGenerator curve;
    public GameObject knobA;
    public GameObject knobB;

    void Start()
    {
        curve = GetComponent<CurveGenerator>();
    }

    void Update()
    {
        Vector2 dir = new Vector2(0.0f, 0.0f);
        if (Input.GetKey(KeyCode.Z) )
            dir[0] += 1.0f;
        if (Input.GetKey(KeyCode.S) )
            dir[0] -= 1.0f;
        if (Input.GetKey(KeyCode.D) )
            dir[1] += 1.0f;
        if (Input.GetKey(KeyCode.Q) )
            dir[1] -= 1.0f;

        float tCurve = Time.deltaTime * steppingCurve;
        float tKnob = Time.deltaTime * steppingKnob;
        float oldFactor = curve.factorA;
        curve.factorA = Mathf.Clamp(curve.factorA + tCurve * dir[0], minMaxA[0], minMaxA[1]);
        if (!Mathf.Approximately(curve.factorA, oldFactor))
            knobA.transform.Rotate(0, 0, tKnob * dir[0], Space.Self);
        oldFactor = curve.factorB;
        curve.factorB = Mathf.Clamp(curve.factorB + tCurve * dir[1], minMaxB[0], minMaxB[1]);
        if (!Mathf.Approximately(curve.factorB, oldFactor))
            knobB.transform.Rotate(0, 0, tKnob * dir[1], Space.Self);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CurvedPlanetManager curveMgr = GameObject.Find("CurvedPlanetManager").GetComponent<CurvedPlanetManager>();
            int nearestIdx = 0;
            float dist = curveMgr.GetDistFromNearestCurve(curve.factorA, curve.factorB, ref nearestIdx);
            if (dist < winThreshold)
            {
                Debug.Log("you win! " + dist + " " + nearestIdx);
                // TODO
            }
        }
    }
}
