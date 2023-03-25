using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GalaxyCameraController : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector2 moveThreshold;
    [SerializeField] private Vector2 boundLimit;

    [SerializeField] private float moveTime;
    [SerializeField] private float moveMagnitude;
    [SerializeField] private float nominalOrthographicSize;
    private Bounds boundPlanet;
    private Transform followedTransform;

    // Update is called once per frame
    void Update()
    {
        // this.MoveCameraByMousePos(Mouse.current.position.ReadValue());
    }

    public void MoveCameraByMousePos(Vector2 mousePos)
    {
        if (mousePos.x < Screen.width * moveThreshold.x && this.transform.position.x > this.boundPlanet.min.x + this.boundLimit.x) {
            this.transform.Translate(Vector2.left * this.moveSpeed * Time.deltaTime);
        }
        else if (mousePos.x > Screen.width * (1 - this.moveThreshold.x) && this.transform.position.x < this.boundPlanet.max.x - this.boundLimit.x) {
            this.transform.Translate(Vector2.right * this.moveSpeed * Time.deltaTime);
        }

        if (mousePos.y < Screen.height * this.moveThreshold.y && this.transform.position.y > this.boundPlanet.min.y + this.boundLimit.y) {
            this.transform.Translate(Vector2.down * this.moveSpeed * Time.deltaTime);
        }
        else if (mousePos.y > Screen.height * (1 - this.moveThreshold.y) && this.transform.position.y < this.boundPlanet.max.y - +this.boundLimit.y) {
            this.transform.Translate(Vector2.up * this.moveSpeed * Time.deltaTime);
        }
    }

    public void AttackGalaxyObject(GalaxyObject galaxy)
    {
        this.boundPlanet = new Bounds(Vector2.zero, Vector2.zero);
        foreach (GalaxyPlanetObject planet in galaxy.listPlanetObject) {
            this.boundPlanet.Encapsulate(planet.transform.position);
        }
        this.transform.position = new Vector3(this.boundPlanet.center.x, this.boundPlanet.center.y, this.transform.position.z);
    }

    /// <summary>
    /// Déplace la caméra pour englober les planètes sélectionnées
    /// </summary>
    /// <param name="listPlanetObj"></param>
    /// <returns></returns>
    public IEnumerator MoveCameraToBoundPlanets(List<GalaxyPlanetObject> listPlanetObj)
    {
        Bounds bounds = new Bounds(Vector2.zero, Vector2.zero);
        foreach (GalaxyPlanetObject planet in listPlanetObj) {
            bounds.Encapsulate(planet.transform.position);
        }

        Vector3 startPos = this.transform.position;
        Vector3 targetPos = new Vector3(bounds.center.x, bounds.center.y, this.transform.position.z);
        float startOrtho = Camera.main.orthographicSize;
        float targetOrtho = Mathf.Max((bounds.max.y - bounds.min.y) / 2 + 0.8f, 5);

        float time = 0;
        float timeRate;
        while (time < this.moveTime) {
            time += Time.deltaTime;
            timeRate = time / this.moveTime;
            this.transform.position = Vector3.Lerp(startPos, targetPos, timeRate);
            Camera.main.orthographicSize = targetOrtho * timeRate + startOrtho * (1 - timeRate);
            yield return new WaitForFixedUpdate();
        }
        this.transform.position = targetPos;
        Camera.main.orthographicSize = targetOrtho;
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
    }

    private IEnumerator FollowTransform(Transform transform)
    {
        this.followedTransform = transform;
        while (this.followedTransform == transform) {
            Vector2 dir = this.followedTransform.position - this.transform.position;
            if (dir.magnitude > this.moveMagnitude) dir = dir / dir.magnitude * this.moveMagnitude;
            this.transform.Translate(dir);
            if (Camera.main.orthographicSize > this.nominalOrthographicSize) Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize - Time.deltaTime * 3, this.nominalOrthographicSize);
            yield return new WaitForFixedUpdate();
        }
    }
}
