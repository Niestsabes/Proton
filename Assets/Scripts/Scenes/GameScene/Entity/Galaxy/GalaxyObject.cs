using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyObject : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject planetPrefab;

    public Galaxy galaxy { get; protected set; }
    public GalaxyPlanetObject[] listPlanetObject { get; protected set; }
    public GalaxyPathObject[,] matrixPathObject { get; protected set; }

    /// <summary>
    /// Cr�e un nouvel objet Galaxy
    /// </summary>
    /// <param name="galaxy"></param>
    /// <param name="prefab"></param>
    /// <returns></returns>
    public static GalaxyObject InstantiateObject(Galaxy galaxy, GameObject prefab)
    {
        GalaxyObject instance = GameObject.Instantiate(prefab).GetComponent<GalaxyObject>();
        if (instance == null) { throw new System.Exception("Pas de script GalaxyObject dans le prefab"); }
        instance.galaxy = galaxy;
        instance.InstantiatePlanets(galaxy.listPlanet);
        return instance;
    }

    /// <summary>
    /// Cr�e les nouvelles plan�tes pour la galaxie
    /// </summary>
    /// <param name="listPlanet"></param>
    private void InstantiatePlanets(GalaxyPlanet[] listPlanet)
    {
        this.listPlanetObject = new GalaxyPlanetObject[listPlanet.Length];
        for (int i = 0; i < listPlanet.Length; i++) {
            this.listPlanetObject[i] = GalaxyPlanetObject.InstantiateObject(listPlanet[i], this.planetPrefab, this.transform);
        }
    }
}
