using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GalaxySerializable
{
    public GalaxyPlanetSerializable[] listPlanet;
}

[Serializable]
public class GalaxyPlanetSerializable
{
    public int id;
    public GalaxyPathSerializable[] listNeighborPlanet;
}

[Serializable]
public class GalaxyPathSerializable
{
    public int id;
    public int type;
}