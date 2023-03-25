using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyPlanet
{
    public Vector2 position { get; protected set; }

    public GalaxyPlanet(Vector2 position)
    {
        this.position = position;
    }
}
