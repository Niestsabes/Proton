using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GalaxyPlanetObject : MonoBehaviour
{
    [Header("GameObject Component")]
    [SerializeField] private SpriteRenderer planetBase;
    [SerializeField] private SpriteRenderer planetShadow;

    [Header("Params")]
    [SerializeField] private Vector2 sizeRange;
    [SerializeField] private float orbitSpeed;

    public GalaxyPlanet planet { get; protected set; }
    public List<GalaxyPathObject> listPathObject { get; protected set; } = new List<GalaxyPathObject>();
    public bool isSelectable { get; protected set; }

    protected float currentOrbitAngle;

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
        instance.currentOrbitAngle = planet.orbitAngle;
        instance.StylizePlanet();
        return instance;
    }

    /// <summary>
    /// Fait orbiter la planète autour d'un centre de gravité.
    /// Suppose que le centre de gravité est en (0, 0)
    /// </summary>
    /// <returns></returns>
    public IEnumerator Orbit(Vector3 centerGravity)
    {
        while (true) {
            Vector3 delta = this.transform.position - centerGravity;
            float angle = delta.y > 0 ? -Mathf.PI / 2 : Mathf.PI / 2;
            if (delta.x < 0) angle = Mathf.Atan(delta.y / delta.x) + Mathf.PI;
            if (delta.x > 0) angle = Mathf.Atan(delta.y / delta.x);

            // Planet Position
            this.currentOrbitAngle += this.orbitSpeed * Time.deltaTime;
            this.transform.position = new Vector2(
                this.planet.orbitRadius * Mathf.Cos(this.currentOrbitAngle) * this.planet.orbitFx,
                this.planet.orbitRadius * Mathf.Sin(this.currentOrbitAngle) * this.planet.orbitFy
            );

            // Planet Rotation
            this.planetShadow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle * Mathf.Rad2Deg));

            // Planet Paths
            foreach (var path in this.listPathObject) {
                if (path.isVisible) path.DrawLine(this.transform.position, path.endPlanetObject.transform.position);
            }

            yield return new WaitForFixedUpdate();
        }
    }

    /// <summary>
    /// Applique la coloration et la taille de la plénète
    /// </summary>
    private void StylizePlanet()
    {
        float size = (Random.value * (this.sizeRange.y - this.sizeRange.x) + this.sizeRange.x);
        this.planetBase.transform.localScale = Vector2.one * size;
        this.planetShadow.transform.localScale = Vector2.one * size;
    }

    // ===== GETTTER / SETTER

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
