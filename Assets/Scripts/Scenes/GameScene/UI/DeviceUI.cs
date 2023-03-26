using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceUI : MonoBehaviour
{
    public void Open(List<GalaxyPlanetObject> listPlanet) {
        if (!this.gameObject.activeInHierarchy) this.gameObject.SetActive(true);
    }

    public void Close()
    {
        if (this.gameObject.activeInHierarchy) this.gameObject.SetActive(false);
    }
}
