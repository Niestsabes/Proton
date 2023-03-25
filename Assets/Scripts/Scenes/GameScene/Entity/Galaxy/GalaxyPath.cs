using UnityEditor;
using UnityEngine;

public class GalaxyPath
{
    public GalaxyPlanet startPlanet { get; protected set; }
    public GalaxyPlanet endPlanet { get; protected set; }

    public GalaxyPath(GalaxyPlanet startPlanet, GalaxyPlanet endPlanet)
    {

    }
}