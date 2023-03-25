using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GalaxyPlanetObject : MonoBehaviour
{
    public GalaxyPlanet planet { get; protected set; }
    public List<GalaxyPathObject> listPathObject { get; protected set; } = new List<GalaxyPathObject>();
    public bool isSelectable { get; protected set; }

    void OnMouseUp()
    {
        if (this.isSelectable) {
            GameSceneManager.instance.eventManager.planetSelect.Invoke(this);
        }
    }

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

    public List<GalaxyPlanetObject> GetNeighborPlanets()
    {
        List<GalaxyPlanetObject> listPlanet = new List<GalaxyPlanetObject>();
        foreach(var path in this.listPathObject) {
            listPlanet.Add(path.startPlanetObject != this ? path.startPlanetObject : path.endPlanetObject);
        }
        return listPlanet;
    }

    public void SetPathsVisible(bool isVisible)
    {
        foreach (GalaxyPathObject pathObj in this.listPathObject) {
            pathObj.SetVisible(isVisible);
        }
    }

    public void SetSelectable(bool isSelectable)
    {
        this.isSelectable = isSelectable;
    }
}
