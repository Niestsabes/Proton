using UnityEditor;
using UnityEngine;

public class GalaxyPath
{
    public enum Type { BLUE, RED, GREEN, YELLOW }

    public GalaxyPlanet startPlanet { get; protected set; }
    public GalaxyPlanet endPlanet { get; protected set; }
    public Type type { get; protected set; }

    public GalaxyPath(GalaxyPlanet startPlanet, GalaxyPlanet endPlanet, Type type)
    {
        this.startPlanet = startPlanet;
        this.endPlanet = endPlanet;
        this.type = type;
    }

    public Gradient colorGradient
    {
        get {
            Gradient gradient = new Gradient();
            GradientColorKey colorKey = new GradientColorKey(Color.white, 1);
            switch (this.type) {
                case GalaxyPath.Type.BLUE: { colorKey = new GradientColorKey(Color.blue, 0); break; };
                case GalaxyPath.Type.RED: { colorKey = new GradientColorKey(Color.red, 0); break; };
                case GalaxyPath.Type.GREEN: { colorKey = new GradientColorKey(Color.green, 0); break; };
                case GalaxyPath.Type.YELLOW: { colorKey = new GradientColorKey(Color.yellow, 0); break; };
            }
            gradient.colorKeys = new GradientColorKey[] { colorKey, new GradientColorKey(Color.white, 1) };
            return gradient;
        }
    }
}