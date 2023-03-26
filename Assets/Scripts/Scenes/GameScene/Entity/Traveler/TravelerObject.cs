using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelerObject : MonoBehaviour
{
    [Header("Prefabs")]
    public List<Sprite> listTravelerBodySprite;

    [Header("Params")]
    public float moveSpeed;
    public SpriteRenderer iconRenderer;
    public SpriteRenderer bodyRenderer;
    public GalaxyPlanetObject currentPlanet { get; protected set; }
    public GalaxyPlanetObject previousPlanet { get; protected set; }
    public TravelerController controller { get; protected set; }
    public bool isVisible { get; protected set; } = true;
    public int idxBody { get; protected set; } = 0;

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
        instance.transform.SetParent(instance.currentPlanet.transform);
        return instance;
    }

    public virtual void AddController(bool isAI)
    {
        if (isAI) this.controller = this.gameObject.AddComponent<TravelerControllerAI>();
        else this.controller = this.gameObject.AddComponent<TravelerControllerManual>();
    }

    public IEnumerator MoveToPlanet(GalaxyPlanetObject targetPlanet)
    {
        this.transform.SetParent(null);
        GalaxyPathObject path = this.currentPlanet.listPathObject.Find(path => path.endPlanetObject == targetPlanet);
        if (path == null) { throw new System.Exception("Le personage ne peut pas aller sur cette planéte!"); }
        float time = 0;
        Vector2 startPos = this.transform.position;
        float maxTime = (targetPlanet.transform.position - this.transform.position).magnitude / this.moveSpeed;
        while (time < maxTime) {
            time += Time.deltaTime;
            this.transform.position = Vector3.Lerp(startPos, targetPlanet.transform.position, time / maxTime);
            yield return new WaitForFixedUpdate();
        }
        this.transform.position = targetPlanet.transform.position;

        this.previousPlanet = this.currentPlanet;
        this.currentPlanet = targetPlanet;
        this.transform.SetParent(this.currentPlanet.transform);
        this.transform.position = targetPlanet.transform.position;
        yield return null;
    }

    public IEnumerator TeleportToPlanet(GalaxyPlanetObject targetPlanet)
    {
        this.transform.SetParent(null);
        this.previousPlanet = this.currentPlanet;
        this.currentPlanet = targetPlanet;
        this.transform.SetParent(this.currentPlanet.transform);
        this.transform.position = targetPlanet.transform.position;
        yield return null;
    }

    // ===== Getter / Setter

    public void SetVisible(bool isVisible)
    {
        this.isVisible = isVisible;
        this.iconRenderer.gameObject.SetActive(isVisible);
        this.bodyRenderer.gameObject.SetActive(isVisible);
    }

    public void SetBody(int idx)
    {
        this.bodyRenderer.sprite = this.listTravelerBodySprite[idx];
        this.idxBody = idx;
    }
}