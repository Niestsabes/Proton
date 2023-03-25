using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelerControllerAI : TravelerController
{
    public override IEnumerator Act()
    {
        GalaxyPlanetObject targetPlanet = CustomRandom.RandomInList(this.travelerObject.currentPlanet.GetNeighborPlanets());
        GameSceneManager.instance.cameraController.FollowTraveler(this.travelerObject);
        yield return this.travelerObject.MoveToPlanet(targetPlanet);
        GameSceneManager.instance.cameraController.StopFollow();
    }
}