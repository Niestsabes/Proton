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
                case GalaxyPath.Type.BLUE: { colorKey = new GradientColorKey(new Color(0.2274f, 0.4039f, 0.6431f), 0); break; };
                case GalaxyPath.Type.RED: { colorKey = new GradientColorKey(new Color(199 / 255.0f, 77 / 255.0f, 69 / 255.0f), 0); break; };
                case GalaxyPath.Type.GREEN: { colorKey = new GradientColorKey(new Color(142 / 255.0f, 100 / 255.0f, 176 / 255.0f), 0); break; };
                case GalaxyPath.Type.YELLOW: { colorKey = new GradientColorKey(new Color(254 / 255.0f, 208 / 255.0f, 73 / 255.0f), 0); break; };
            }
            gradient.colorKeys = new GradientColorKey[] { colorKey, new GradientColorKey(Color.white, 1) };
            return gradient;
        }
    }
}