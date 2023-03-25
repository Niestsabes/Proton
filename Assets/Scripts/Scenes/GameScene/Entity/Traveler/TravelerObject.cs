using System.Collections;
using UnityEngine;

public class TravelerObject : MonoBehaviour
{
    public GalaxyPlanetObject currentPlanet { get; protected set; }
    public TravelerController controller { get; protected set; }

    /// <summary>
    /// Crée un nouvel objet TravelerObject
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="planet"></param>
    /// <returns></returns>
    public static TravelerObject InstantiateObject(GameObject prefab, GalaxyPlanetObject planet)
    {
        TravelerObject instance = GameObject.Instantiate(prefab, planet.transform.position, Quaternion.identity).GetComponent<TravelerObject>();
        if (instance == null) { throw new System.Exception("Pas de script TravelerObject dans le prefab"); }
        instance.currentPlanet = planet;
        return instance;
    }

    public void AddController(bool isAI)
    {
        if (isAI) this.controller = this.gameObject.AddComponent<TravelerControllerAI>();
        else this.controller = this.gameObject.AddComponent<TravelerControllerManual>();
    }

    public IEnumerator MoveToPlanet(GalaxyPlanetObject targetPlanet)
    {
        this.currentPlanet = targetPlanet;
        this.transform.position = targetPlanet.transform.position;
        yield return null;
    }
}