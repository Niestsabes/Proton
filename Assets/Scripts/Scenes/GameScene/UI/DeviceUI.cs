using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceUI : MonoBehaviour
{
    private CurvedPlanetManager curveMgr;

    void Start()
    {
         //curveMgr = ;
    }

    public void Open(List<GalaxyPathObject> listPath) {
        if (!this.gameObject.activeInHierarchy)
        {
            int[] planetIds = new int[3];
            List<GalaxyPlanetObject> planets = new List<GalaxyPlanetObject>();
            Debug.Log(listPath.Count);
            for (int i = 0; i < 3; ++i)
            {
                planetIds[i] = (int)listPath[i].path.type;
                planets.Add(listPath[i].endPlanetObject);
            }

            this.gameObject.SetActive(true);
            GameObject.Find("CurvedPlanetManager").GetComponent<CurvedPlanetManager>().Redraw(planetIds, planets);
        }
    }

    public void Close()
    {
        if (this.gameObject.activeInHierarchy) this.gameObject.SetActive(false);
    }
}
