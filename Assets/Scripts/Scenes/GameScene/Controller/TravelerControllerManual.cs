using System.Collections;
using UnityEngine;

public class TravelerControllerManual : TravelerController
{
    public override IEnumerator Act()
    {
        // Mettre à jour la UI et les interactions
        this.travelerObject.currentPlanet.listPathObject.ForEach(path => {
            if (path.endPlanetObject != this.travelerObject.previousPlanet) {
                path.SetVisible(true);
                path.endPlanetObject.SetSelectable(true);
            }
        });

        // Attendre que le joueur choisisse une planéte
        GalaxyPlanetObject planetObjSelect = null;
        GameSceneManager.instance.eventManager.planetSelect.AddListener(planetObj => { planetObjSelect = planetObj; });
        yield return new WaitUntil(() => planetObjSelect != null);

        // Désactiver les interactions
        GalaxyPlanetObject oldPlanet = this.travelerObject.currentPlanet;
        GameSceneManager.instance.eventManager.planetSelect.RemoveAllListeners();
        oldPlanet.GetNeighborPlanets().ForEach(planet => planet.SetSelectable(false));

        // Déplacer le joueur
        GameSceneManager.instance.cameraController.FollowTraveler(this.travelerObject);
        yield return this.travelerObject.MoveToPlanet(planetObjSelect);
        GameSceneManager.instance.cameraController.StopFollow();

        // Nettoyer la UI
        oldPlanet.SetPathsVisible(false);
    }
}