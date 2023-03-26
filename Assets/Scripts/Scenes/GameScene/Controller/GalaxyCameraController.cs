using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GalaxyCameraController : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private float orthoSizeMax = 22;
    [SerializeField] private float orthoSizeMin = 10;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector2 moveThreshold;
    [SerializeField] private Vector2 boundLimit;

    [SerializeField] private float moveTime;
    [SerializeField] private float moveMagnitude;
    [SerializeField] private float nominalOrthographicSize;
    private Bounds boundPlanet;
    private Transform followedTransform;

    public void AttackGalaxyObject(GalaxyObject galaxy)
    {
        this.boundPlanet = new Bounds(Vector2.zero, Vector2.zero);
        foreach (GalaxyPlanetObject planet in galaxy.listPlanetObject) {
            this.boundPlanet.Encapsulate(planet.transform.position);
        }
        this.transform.position = new Vector3(this.boundPlanet.center.x, this.boundPlanet.center.y, this.transform.position.z);
    }

    public IEnumerator ResetCamera()
    {
        yield return this.MoveToPosition(Vector3.zero);
        Camera.main.orthographicSize = this.orthoSizeMax;

    }

    /// <summary>
    /// Déplace la caméra pour englober les planètes sélectionnées
    /// </summary>
    /// <param name="listPlanetObj"></param>
    /// <returns></returns>
    public IEnumerator MoveCameraToBoundPlanets(List<GalaxyPlanetObject> listPlanetObj)
    {
        //Bounds bounds = new Bounds(Vector2.zero, Vector2.zero);
        //foreach (GalaxyPlanetObject planet in listPlanetObj) {
        //    bounds.Encapsulate(planet.transform.position);
        //}

        //Vector3 startPos = this.transform.position;
        //Vector3 targetPos = new Vector3(bounds.center.x, bounds.center.y, this.transform.position.z);
        //// float startOrtho = Camera.main.orthographicSize;
        //// float targetOrtho = Mathf.Max((bounds.max.y - bounds.min.y) / 2 + 0.8f, 5);

        //float time = 0;
        //float timeRate;
        //while (time < this.moveTime) {
        //    time += Time.deltaTime;
        //    timeRate = time / this.moveTime;
        //    this.transform.position = Vector3.Lerp(startPos, targetPos, timeRate);
        //    // Camera.main.orthographicSize = targetOrtho * timeRate + startOrtho * (1 - timeRate);
        //    yield return new WaitForFixedUpdate();
        //}
        //this.transform.position = targetPos;
        //// Camera.main.orthographicSize = targetOrtho;
        yield return null;
    }

    /// <summary>
    /// Déplace la caméra vers une position
    /// </summary>
    /// <param name="position"></param>
    public IEnumerator MoveToPosition(Vector3 targetPos)
    {
        Vector3 startPos = this.transform.position;
        Vector3 endPos = new Vector3(targetPos.x, targetPos.y, this.transform.position.z);
        float time = 0;
        float timeRate;
        while (time < this.moveTime) {
            time += Time.deltaTime;
            timeRate = Mathf.Min(1, time / this.moveTime);
            this.transform.position = Vector3.Lerp(startPos, endPos, timeRate);
            yield return new WaitForFixedUpdate();
        }
        yield return null;
    }

    /// <summary>
    /// Déplace la caméra pour suivre le Traveler
    /// </summary>
    /// <param name="traveler"></param>
    /// <returns></returns>
    public void FollowTraveler(TravelerObject traveler)
    {
        StartCoroutine(this.FollowTransform(traveler.transform));
    }

    public void StopFollow()
    {
        this.followedTransform = null;
        Camera.main.orthographicSize = this.orthoSizeMax;
    }

    private IEnumerator FollowTransform(Transform transform)
    {
        this.followedTransform = transform;
        Camera.main.orthographicSize = this.orthoSizeMin;
        while (this.followedTransform == transform) {
            Vector2 dir = this.followedTransform.position - this.transform.position;
            if (dir.magnitude > this.moveMagnitude) dir = dir / dir.magnitude * this.moveMagnitude;
            this.transform.Translate(dir);
            yield return new WaitForFixedUpdate();
        }
        yield return null;
    }
}
