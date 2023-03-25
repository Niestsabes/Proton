using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Galaxy
{
    public GalaxyPlanet[] listPlanet { get; protected set; }
    public GalaxyPath[,] matrixPath { get; protected set; }

    public Galaxy(GalaxyPlanet[] listPlanet, GalaxyPath[,] matrixPath)
    {
        this.listPlanet = listPlanet;
        this.matrixPath = matrixPath;
    }
}
