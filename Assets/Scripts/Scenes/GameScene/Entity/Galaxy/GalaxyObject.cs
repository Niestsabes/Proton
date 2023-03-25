using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyObject : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject planetPrefab;
    [SerializeField] private GameObject pathPrefab;

    public Galaxy galaxy { get; protected set; }
    public GalaxyPlanetObject[] listPlanetObject { get; protected set; }
    public GalaxyPathObject[,] matrixPathObject { get; protected set; }

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
        instance.InstantiatePlanets(galaxy.listPlanet);
        instance.InstantiatePaths(galaxy.matrixPath);
        return instance;
    }

    /// <summary>
    /// Crée les nouvelles planétes pour la galaxie
    /// </summary>
    /// <param name="listPlanet"></param>
    private void InstantiatePlanets(GalaxyPlanet[] listPlanet)
    {
        this.listPlanetObject = new GalaxyPlanetObject[listPlanet.Length];
        for (int i = 0; i < listPlanet.Length; i++) {
            this.listPlanetObject[i] = GalaxyPlanetObject.InstantiateObject(listPlanet[i], this.planetPrefab, this.transform);
        }
    }

    /// <summary>
    /// Crée les chemins pour aller de planéte en planète
    /// </summary>
    /// <param name="listPlanet"></param>
    /// <param name="matrixPath"></param>
    private void InstantiatePaths(GalaxyPath[,] matrixPath)
    {
        this.matrixPathObject = new GalaxyPathObject[matrixPath.GetLength(0), matrixPath.GetLength(1)];
        for (int startIdx = 0; startIdx < matrixPath.GetLength(0); startIdx++) {
            for (int endIdx = 0; endIdx < matrixPath.GetLength(1); endIdx++) {
                var path = matrixPath[startIdx, endIdx];
                if (path != null) {
                    this.matrixPathObject[startIdx, endIdx] = GalaxyPathObject.InstantiateObject(path, this.listPlanetObject[startIdx], this.listPlanetObject[endIdx], this.pathPrefab);
                }
            }
        }
    }
}
