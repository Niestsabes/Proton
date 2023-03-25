using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyPlanetObject : MonoBehaviour
{
    public GalaxyPlanet planet { get; protected set; }
    
    public List<GalaxyPathObject> listPathObject { get; protected set; } = new List<GalaxyPathObject>();

    /// <summary>
    /// Crée un nouvel objet GalaxyPlanet
    /// </summary>
    /// <param name="planet"></param>
    /// <param name="prefab"></param>
    /// <returns></returns>
    public static GalaxyPlanetObject InstantiateObject(GalaxyPlanet planet, GameObject prefab, Transform parent)
    {
        GalaxyPlanetObject instance = GameObject.Instantiate(prefab, planet.position, Quaternion.identity, parent).GetComponent<GalaxyPlanetObject>();
        if (instance == null) { throw new System.Exception("Pas de script GalaxyPlanetObject dans le prefab"); }
        instance.planet = planet;
        return instance;
    }

    /// <summary>
    /// Ajouter la référence d'un chemin crée à la planéte
    /// </summary>
    /// <param name="pathObject"></param>
    public void AttachPathObject(GalaxyPathObject pathObject)
    {
        this.listPathObject.Add(pathObject);
    }
}
