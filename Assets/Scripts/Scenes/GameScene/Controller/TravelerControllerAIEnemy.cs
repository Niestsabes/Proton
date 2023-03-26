using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class TravelerControllerAIEnemy : TravelerController
{
    public override IEnumerator Act()
    {
        // Selection de la planète
        GalaxyPlanetObject targetPlanet;
        var listNeighbor = this.travelerObject.currentPlanet.GetNeighborPlanets();
        var listPlanetTakenByEnemy = GameSceneManager.instance.listEnemyTravelerObject.Select(enemy => enemy.currentPlanet).ToList();
        var listFreePlanet = listNeighbor.Where(planet => listPlanetTakenByEnemy.IndexOf(planet) < 0).ToList();
        if (listFreePlanet.Count > 0) targetPlanet = CustomRandom.RandomInList(listFreePlanet);
        else targetPlanet = CustomRandom.RandomInList(this.travelerObject.currentPlanet.GetNeighborPlanets());

        // Mouvement
        GameSceneManager.instance.cameraController.FollowTraveler(this.travelerObject);
        if (this.travelerObject.isVisible) yield return this.travelerObject.MoveToPlanet(targetPlanet);
        else yield return this.travelerObject.TeleportToPlanet(targetPlanet);
        GameSceneManager.instance.cameraController.StopFollow();
    }
}