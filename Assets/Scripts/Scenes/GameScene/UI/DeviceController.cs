using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceController : MonoBehaviour
{
    [Header("Curve")]
    public Vector2 minMaxA = new Vector2(-50.0f, 50.0f);
    public Vector2 minMaxB = new Vector2(-50.0f, 50.0f);
    public float steppingCurve = 10.0f;
    public float winThreshold = 10.0f;

    [Header("Knobs")]
    public GameObject knobA;
    public GameObject knobB;
    public float steppingKnob = 10.0f;

    [Header("Audio")]
    public AudioSource whiteNoise;
    public AudioSource searchNote;
    public AudioSource foundNote;
    public AudioSource clicSound;
    public float startFadeThreshold = 5.0f;
    [Range(0.0f, 1.0f)]
    public float deviceVolume = 0.8f;

    private CurveGenerator curve;
    private CurvedPlanetManager curveMgr;

    void Start()
    {
        curve = GetComponent<CurveGenerator>();
        curveMgr = GameObject.Find("CurvedPlanetManager").GetComponent<CurvedPlanetManager>();
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

        if (dir.sqrMagnitude > 0.0f && !clicSound.isPlaying)
            clicSound.PlayOneShot(clicSound.clip);

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

        int nearestIdx = 0;
        float dist = curveMgr.GetDistFromNearestCurve(curve.factorA, curve.factorB, ref nearestIdx);

        float clampedDist = Mathf.Clamp(dist, winThreshold, startFadeThreshold);
        float fadeRatio = (clampedDist - winThreshold) / (startFadeThreshold - winThreshold);
        whiteNoise.volume = fadeRatio;
        searchNote.volume = 1.0f - fadeRatio;

        if (dist < winThreshold)
        {
            foundNote.volume = 1.0f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("you win! " + dist + " " + nearestIdx);
                // TODO
            }
        }
        else
        {
            foundNote.volume = 0.0f;
        }

        UpdateVolume();
    }

    void UpdateVolume()
    {
        clicSound.volume *= deviceVolume;
        whiteNoise.volume *= deviceVolume;
        searchNote.volume *= deviceVolume;
        foundNote.volume *= deviceVolume;
    }
}
