using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyFactory
{
    public Galaxy GenerateGalaxy(int nbPlanet)
    {
        GalaxyPlanet[] listPlanet = this.GeneratePlanets(nbPlanet);
        GalaxyPath[,] matrixPath = this.GeneratePaths(listPlanet);
        return new Galaxy(listPlanet, matrixPath);
    }

    public GalaxyPlanet[] GeneratePlanets(int nbPlanet)
    {
        GalaxyPlanet[] listPlanet = new GalaxyPlanet[nbPlanet];
        for (int i = 0; i < nbPlanet; i++) {
            listPlanet[i] = new GalaxyPlanet(new Vector2(Random.value * 10, Random.value * 10));
        }
        return listPlanet;
    }

    public GalaxyPath[,] GeneratePaths(GalaxyPlanet[] listPlanet)
    {
        GalaxyPath[,] matrixPath = new GalaxyPath[listPlanet.Length, listPlanet.Length];
        return matrixPath;
    }
}
