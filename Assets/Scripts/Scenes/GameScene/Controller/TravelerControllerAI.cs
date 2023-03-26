using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelerControllerAI : TravelerController
{
    public override IEnumerator Act()
    {
        GalaxyPlanetObject targetPlanet = CustomRandom.RandomInList(this.travelerObject.currentPlanet.GetNeighborPlanets());
        GameSceneManager.instance.cameraController.FollowTraveler(this.travelerObject);
        if (this.travelerObject.isVisible) yield return this.travelerObject.MoveToPlanet(targetPlanet);
        else yield return this.travelerObject.TeleportToPlanet(targetPlanet);
        GameSceneManager.instance.cameraController.StopFollow();
    }
}