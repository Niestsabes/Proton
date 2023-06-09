using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyFactory
{
    private const int PLANET_LOCATION_RETRY = 50;
    private const float PLANET_MIN_SPACE = 1;
    private const int NB_PATH_TYPE = 2;

    public Galaxy GenerateGalaxy(int nbPlanet)
    {
        GalaxyPlanet[] listPlanet = this.GeneratePlanets(nbPlanet);
        GalaxyPath[,] matrixPath = this.GeneratePaths(listPlanet);
        return new Galaxy(listPlanet, matrixPath);
    }

    public Galaxy GenerateGalaxyFromSerializable(GalaxySerializable galaxySerial)
    {
        GalaxyPlanet[] listPlanet = this.GeneratePlanets(galaxySerial.listPlanet.Length);
        GalaxyPath[,] matrixPath = this.GeneratePathsFromSerializable(galaxySerial, listPlanet);
        return new Galaxy(listPlanet, matrixPath);
    }

    public GalaxyPlanet[] GeneratePlanets(int nbPlanet)
    {
        List<Vector2> listPlanetPos = new List<Vector2>();
        GalaxyPlanet[] listPlanet = new GalaxyPlanet[nbPlanet];
        for (int i = 0; i < nbPlanet; i++) {
            int retry = 0;
            Vector2 newPlanetPos;
            float angle, radius;
            do {
                retry++;
                angle = Mathf.PI * 2 * Random.value;
                radius = Random.value * 18 + 5;
                newPlanetPos = new Vector2(radius * Mathf.Cos(angle) * 1.6f, radius * Mathf.Sin(angle) * 0.9f);
            } while (!this.IsPlanetLocationValid(newPlanetPos, listPlanetPos) && retry < PLANET_LOCATION_RETRY);
            listPlanet[i] = new GalaxyPlanet(newPlanetPos);
            listPlanet[i].SetOrbitParam(radius, angle, 1.6f, 0.9f);
            listPlanetPos.Add(newPlanetPos);
        }
        return listPlanet;
    }

    public GalaxyPath[,] GeneratePaths(GalaxyPlanet[] listPlanet)
    {
        GalaxyPath[,] matrixPath = new GalaxyPath[listPlanet.Length, listPlanet.Length];
        for (int startIdx = 0; startIdx < matrixPath.GetLength(0); startIdx++) {
            for (int endIdx = 0; endIdx < matrixPath.GetLength(1); endIdx++) {
                if (Random.value > 0.5) matrixPath[startIdx, endIdx] = new GalaxyPath(listPlanet[startIdx], listPlanet[endIdx], this.RandomType());
            }
        }
        return matrixPath;
    }

    public GalaxyPath[,] GeneratePathsFromSerializable(GalaxySerializable galaxySerial, GalaxyPlanet[] listPlanet)
    {
        GalaxyPath[,] matrixPath = new GalaxyPath[galaxySerial.listPlanet.Length, galaxySerial.listPlanet.Length];
        for (int startIdx = 0; startIdx < matrixPath.GetLength(0); startIdx++) {
            foreach (var pathSerial in galaxySerial.listPlanet[startIdx].listNeighborPlanet) {
                matrixPath[startIdx, pathSerial.id] = new GalaxyPath(listPlanet[startIdx], listPlanet[pathSerial.id], (GalaxyPath.Type)(pathSerial.type));
                matrixPath[pathSerial.id, startIdx] = new GalaxyPath(listPlanet[pathSerial.id], listPlanet[startIdx], (GalaxyPath.Type)(pathSerial.type + NB_PATH_TYPE));
            }
        }
        return matrixPath;
    }

    private bool IsPlanetLocationValid(Vector2 newLocation, List<Vector2> listPreviousLocations)
    {
        foreach (Vector2 prevLocation in listPreviousLocations) {
            if ((prevLocation - newLocation).magnitude <= 1.6f + PLANET_MIN_SPACE) return false;
        }
        return true;
    }

    private GalaxyPath.Type RandomType()
    {
        return (GalaxyPath.Type)Mathf.FloorToInt(Random.value * 3);
    }
}
