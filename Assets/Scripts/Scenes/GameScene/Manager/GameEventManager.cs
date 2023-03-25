using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameEventManager {
    public UnityEvent<GalaxyPlanetObject> planetSelect = new UnityEvent<GalaxyPlanetObject>();
}