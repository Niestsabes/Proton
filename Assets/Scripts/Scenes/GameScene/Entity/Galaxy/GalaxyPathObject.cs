using System.Collections;
using UnityEngine;

public class GalaxyPathObject : MonoBehaviour
{
    [Header("GameObject Components")]
    [SerializeField] protected LineRenderer lineRenderer;

    [Header("Params")]
    [SerializeField] protected int nbPointPerUnit;
    [SerializeField] protected float waveMagnitude;

    public GalaxyPath path { get; protected set; }
    public GalaxyPlanetObject startPlanetObject { get; protected set; }
    public GalaxyPlanetObject endPlanetObject { get; protected set; }

    void Update()
    {
        this.AnimateLineOndulation();
    }

    /// <summary>
    /// Crée un nouvel objet GalaxyPath
    /// </summary>
    /// <param name="path"></param>
    /// <param name="prefab"></param>
    /// <returns></returns>
    public static GalaxyPathObject InstantiateObject(GalaxyPath path, GalaxyPlanetObject startPlanetObject, GalaxyPlanetObject endPlanetObject, GameObject prefab)
    {
        GalaxyPathObject instance = GameObject.Instantiate(prefab, startPlanetObject.transform).GetComponent<GalaxyPathObject>();
        if (instance == null) { throw new System.Exception("Pas de script GalaxyPathObject dans le prefab"); }
        instance.path = path;
        instance.startPlanetObject = startPlanetObject;
        instance.endPlanetObject = endPlanetObject;
        instance.DrawLine(startPlanetObject.transform.position, endPlanetObject.transform.position);
        instance.SetVisible(false);
        startPlanetObject.AttachPathObject(instance);
        return instance;
    }

    /// <summary>
    /// Dessine le trait reliant les deux planétes
    /// </summary>
    /// <param name="startPos"></param>
    /// <param name="endPos"></param>
    public void DrawLine(Vector3 startPos, Vector3 endPos)
    {
        int nbPoint = Mathf.CeilToInt((endPos - startPos).magnitude * this.nbPointPerUnit) + 2;
        Vector3[] listPoint = new Vector3[nbPoint];
        for (int i = 0; i < nbPoint; i++) {
            listPoint[i] = Vector3.Lerp(startPos, endPos, (float)(i) / (nbPoint - 1));
        }
        this.lineRenderer.positionCount = nbPoint;
        this.lineRenderer.SetPositions(listPoint);
        this.lineRenderer.colorGradient = this.path.colorGradient;
    }

    /// <summary>
    /// Affiche ou cache le rendu du chemin reliant les planétes
    /// </summary>
    /// <param name="isVisible"></param>
    public void SetVisible(bool isVisible)
    {
        this.lineRenderer.enabled = isVisible;
    }

    private void AnimateLineOndulation()
    {
        int nbPoint = this.lineRenderer.positionCount;
        Vector2 startPos = this.lineRenderer.GetPosition(0);
        Vector2 endPos = this.lineRenderer.GetPosition(nbPoint - 1);
        Vector2 perpendicular = Vector2.Perpendicular(endPos - startPos).normalized;

        for (int i = 1; i < nbPoint - 1; i++) {
            this.lineRenderer.SetPosition(i, this.InterpolateOndulation(startPos, endPos, (float)(i) / (nbPoint - 1), perpendicular));
        }
    }

    private Vector2 InterpolateOndulation(Vector2 start, Vector2 end, float xLinePos, Vector2 dir)
    {
        float waveOffset = Mathf.Sin(xLinePos * 2 * Mathf.PI);
        return Vector2.Lerp(start, end, xLinePos) + dir * waveOffset * this.waveMagnitude;
    }

    public Vector3[] positions
    {
        get {
            Vector3[] output = new Vector3[this.lineRenderer.positionCount];
            this.lineRenderer.GetPositions(output);
            return output;
        }
    }
}