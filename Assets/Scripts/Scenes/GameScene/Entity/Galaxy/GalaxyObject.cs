using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyObject : MonoBehaviour
{
    public Galaxy galaxy { get; protected set; }
    public GalaxyPlanetObject[] listPlanetObject { get; protected set; }
    public GalaxyPathObject[][] matrixPathObject { get; protected set; }

    /// <summary>
    /// Crée un nouvel objet Galaxy
    /// </summary>
    /// <param name="galaxy"></param>
    /// <param name="prefab"></param>
    /// <returns></returns>
    public static GalaxyObject InstantiateObject(Galaxy galaxy, GameObject prefab)
    {
        GalaxyObject instance = GameObject.Instantiate(prefab).GetComponent<GalaxyObject>();
        if (instance == null) { throw new System.Exception("Pas de script GalaxyObject dans le prefab"); }
        instance.galaxy = galaxy;
        return instance;
    }
}
