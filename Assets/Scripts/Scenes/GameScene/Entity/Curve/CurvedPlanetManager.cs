using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurvedPlanetManager : MonoBehaviour
{
    public GameObject planetPrefab;
    public List<GameObject> texturePrefabs;

    private List<GameObject> planets = new List<GameObject>();
    private List<CurveGenerator> curves = new List<CurveGenerator>();

    // Start is called before the first frame update
    void Start()
    {
        Vector3[] pos = {   new Vector3 { x = -3.22f, y = -2.44f, z = 0 }, 
                            new Vector3 { x = 6.99f, y = 4.67f, z = 0 }, 
                            new Vector3 { x = 11.31f, y = -5.5f, z = 0 } };
        for (int i = 0; i < 3; ++i)
        {
            GameObject newPlanet = (GameObject)Instantiate(planetPrefab, pos[i], Quaternion.identity);
            planets.Add(newPlanet);
            newPlanet.transform.parent = transform;
            var curve = newPlanet.transform.Find("Curve").GetComponent<CurveGenerator>();
            curves.Add(curve);
            curve.InitCurve(20.0f);
            int textureIdx = i; // TODO
            GameObject texture = (GameObject)Instantiate(texturePrefabs[i], Vector3.zero, Quaternion.identity);
            texture.transform.parent = newPlanet.transform;
            texture.transform.localPosition = Vector3.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetDistFromNearestCurve(float factorA, float factorB, ref int outNearestIdx)
    {
        float minDist = 100000000.0f;
        for (int i = 0; i < curves.Count; ++i)
        {
            float curDist = Mathf.Abs(curves[i].factorA - factorA) + Mathf.Abs(curves[i].factorB - factorB);
            if (curDist < minDist)
            {
                outNearestIdx = i;
                minDist = curDist;
            }
        }

        return minDist;
    }
}