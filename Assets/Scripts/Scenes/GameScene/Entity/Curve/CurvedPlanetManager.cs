using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurvedPlanetManager : MonoBehaviour
{
    public GameObject planetPrefab;
    public List<GameObject> texturePrefabs;

    private List<GameObject> planets = new List<GameObject>();
    private List<CurveGenerator> curves = new List<CurveGenerator>();
    private List<GalaxyPlanetObject> planetDests = new List<GalaxyPlanetObject>();

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Redraw(int[] _planetIds, List<GalaxyPlanetObject> _planets)
    {
        foreach (var planet in planets)
            Destroy(planet);
        planets.Clear();
        curves.Clear();
        planetDests = _planets;

        Vector3[] pos = {   new Vector3 { x = -1f, y = -2f, z = 0 }, 
                            new Vector3 { x = 2.5f, y = 2f, z = 0 }, 
                            new Vector3 { x = 6f, y = -2f, z = 0 } };
        for (int i = 0; i < 3; ++i)
        {
            GameObject newPlanet = (GameObject)Instantiate(planetPrefab, transform);
            newPlanet.transform.localPosition = pos[i];
            planets.Add(newPlanet);
            var curve = newPlanet.transform.Find("Curve").GetComponent<CurveGenerator>();
            // texture.GetComponent<SpriteRenderer>().sortingLayerName = "Device";
            curves.Add(curve);
            curve.InitCurve(15.0f);
            int textureIdx = _planetIds[i];
            GameObject texture = (GameObject)Instantiate(texturePrefabs[i], Vector3.zero, Quaternion.identity);
            texture.transform.parent = newPlanet.transform;
            texture.transform.localPosition = Vector3.zero;
            texture.GetComponent<SpriteRenderer>().sortingLayerName = "Device";
            var lines = newPlanet.GetComponentsInChildren<LineRenderer>();
            foreach (var line in lines) {
                line.sortingLayerName = "Device";
            }
            newPlanet.transform.localScale = Vector3.one * 0.6f;
        }
    }

    public float GetDistFromNearestCurve(float factorA, float factorB, ref GalaxyPlanetObject destPlanet)
    {
        float minDist = 100000000.0f;
        for (int i = 0; i < curves.Count; ++i)
        {
            float curDistA = Mathf.Abs(curves[i].factorA - factorA);
            float curDistB = Mathf.Abs(curves[i].factorB - factorB);
            float maxCurDist = Mathf.Max(curDistA, curDistB);
            if (maxCurDist < minDist)
            {
                destPlanet = planetDests[i];
                minDist = maxCurDist;
            }
        }

        return minDist;
    }
}
