using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class TravelerControllerManual : TravelerController
{
    private bool isActing = false;
    private bool isListeningInput = false;
    private List<GalaxyPathObject> listAccessiblePath;

    public override IEnumerator Act()
    {
        this.isActing = true;

        // Mettre à jour la UI et les interactions
        this.listAccessiblePath = this.travelerObject.currentPlanet.listPathObject
            .Where((path) => path.endPlanetObject != this.travelerObject.previousPlanet).ToList();
        this.listAccessiblePath.ForEach(path => { path.SetVisible(true); path.endPlanetObject.SetSelectable(true); });
        StartCoroutine(this.ListenInput());

        // Attendre que le joueur choisisse une planéte
        GalaxyPlanetObject planetObjSelect = null;
        GameSceneManager.instance.eventManager.onDeviceWin.AddListener(planetObj => { planetObjSelect = planetObj; });
        yield return new WaitUntil(() => planetObjSelect != null);

        // remove device view
        GameSceneManager.instance.deviceUI.Close();

        // Désactiver les interactions
        GalaxyPlanetObject oldPlanet = this.travelerObject.currentPlanet;
        // GameSceneManager.instance.eventManager.onDeviceWin.RemoveAllListeners();
        GameSceneManager.instance.eventManager.planetSelect.RemoveAllListeners();
        oldPlanet.GetNeighborPlanets().ForEach(planet => planet.SetSelectable(false));

        // Déplacer le joueur
        GameSceneManager.instance.cameraController.FollowTraveler(this.travelerObject);
        yield return this.travelerObject.MoveToPlanet(planetObjSelect);
        GameSceneManager.instance.cameraController.StopFollow();

        // Nettoyer la UI
        oldPlanet.SetPathsVisible(false);

        this.isActing = false;
        this.StopListeningInput();
        this.listAccessiblePath = new List<GalaxyPathObject>() { };
    }

    private IEnumerator ListenInput()
    {
        this.isListeningInput = true;
        while(this.isListeningInput && this.isActing) {
            if (Input.GetKey(KeyCode.Return)) { GameSceneManager.instance.deviceUI.Open(this.listAccessiblePath); }
            if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) { GameSceneManager.instance.deviceUI.Close(); }
            yield return new WaitForEndOfFrame();
        }
        this.isListeningInput = false;
    }

    private void StopListeningInput()
    {
        this.isListeningInput = false;
    }
}