using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelerControllerAI : TravelerController
{
    public override IEnumerator Act()
    {
        GalaxyPlanetObject targetPlanet = CustomRandom.RandomInList(this.travelerObject.currentPlanet.GetNeighborPlanets());
        
        // Déplacer le joueur
        yield return this.travelerObject.MoveToPlanet(targetPlanet);
    }
}