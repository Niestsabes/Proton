using System.Collections;
using UnityEngine;

public class TravelerObject : MonoBehaviour
{
    [Header("Params")]
    public float moveSpeed;
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
        GalaxyPathObject path = this.currentPlanet.listPathObject.Find(path => path.endPlanetObject == targetPlanet);
        if (path == null) { throw new System.Exception("Le personage ne peut pas aller sur cette planéte!"); }
        Vector3[] positions = path.positions;
        for (int i = 1; i < positions.Length; i++) {
            float time = 0;
            float maxTime = (positions[i] - positions[i - 1]).magnitude / this.moveSpeed;
            while (time < maxTime) {
                time += Time.deltaTime;
                this.transform.position = Vector3.Lerp(positions[i - 1], positions[i], time / maxTime);
                yield return new WaitForFixedUpdate();
            }
            this.transform.position = positions[i - 1];
        }

        this.currentPlanet = targetPlanet;
        this.transform.position = targetPlanet.transform.position;
        yield return null;
    }
}