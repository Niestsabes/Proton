using System.Collections;
using UnityEngine;

public class GalaxyPathObject : MonoBehaviour
{
    public GalaxyPath path { get; protected set; }

    /// <summary>
    /// Crée un nouvel objet GalaxyPath
    /// </summary>
    /// <param name="path"></param>
    /// <param name="prefab"></param>
    /// <returns></returns>
    public static GalaxyPathObject InstantiateObject(GalaxyPath path, GameObject prefab)
    {
        GalaxyPathObject instance = GameObject.Instantiate(prefab).GetComponent<GalaxyPathObject>();
        if (instance == null) { throw new System.Exception("Pas de script GalaxyPathObject dans le prefab"); }
        instance.path = path;
        return instance;
    }
}