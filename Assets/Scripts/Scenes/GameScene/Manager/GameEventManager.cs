using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameEventManager {
    public UnityEvent<GalaxyPlanetObject> planetSelect { get; private set; } = new UnityEvent<GalaxyPlanetObject>();
    public UnityEvent<bool> showDevice { get; private set; } = new UnityEvent<bool>();
    public UnityEvent<GalaxyPlanetObject> onDeviceWin { get; private set; } = new UnityEvent<GalaxyPlanetObject>();
}