using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyPlanet
{
    public Vector2 position { get; protected set; }
    public float orbitRadius { get; protected set; }
    public float orbitAngle { get; protected set; }
    public float orbitFx { get; protected set; }
    public float orbitFy { get; protected set; }

    public GalaxyPlanet(Vector2 position)
    {
        this.position = position;
    }

    public void SetOrbitParam(float r, float a, float fx, float fy)
    {
        this.orbitRadius = r;
        this.orbitAngle = a;
        this.orbitFx = fx;
        this.orbitFy = fy;
    }
}
