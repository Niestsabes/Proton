using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceUI : MonoBehaviour
{
    void Start()
    {
    }

    public void Open(List<GalaxyPathObject> listPath) {
        if (!this.gameObject.activeInHierarchy)
        {
            int[] planetIds = new int[3];
            List<GalaxyPlanetObject> planets = new List<GalaxyPlanetObject>();
            for (int i = 0; i < 3; ++i)
            {
                planetIds[i] = (int)listPath[i].path.type;
                planets.Add(listPath[i].endPlanetObject);
            }

            GameObject.Find("Main Camera").GetComponent<AudioSource>().volume = 0.0f;
            this.gameObject.SetActive(true);
            GameObject.Find("CurvedPlanetManager").GetComponent<CurvedPlanetManager>().Redraw(planetIds, planets);
        }
    }

    public void Close()
    {
        if (this.gameObject.activeInHierarchy) this.gameObject.SetActive(false);
        GameObject.Find("Main Camera").GetComponent<AudioSource>().volume = 1.0f;
    }
}
