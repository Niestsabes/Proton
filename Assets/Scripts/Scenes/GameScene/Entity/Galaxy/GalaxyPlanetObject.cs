using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyPlanetObject : MonoBehaviour
{
    public GalaxyPlanet planet { get; protected set; }

    /// <summary>
    /// Crée un nouvel objet GalaxyPlanet
    /// </summary>
    /// <param name="planet"></param>
    /// <param name="prefab"></param>
    /// <returns></returns>
    public static GalaxyPlanetObject InstantiateObject(GalaxyPlanet planet, GameObject prefab)
    {
        GalaxyPlanetObject instance = GameObject.Instantiate(prefab).GetComponent<GalaxyPlanetObject>();
        if (instance == null) { throw new System.Exception("Pas de script GalaxyPlanetObject dans le prefab"); }
        instance.planet = planet;
        return instance;
    }
}
