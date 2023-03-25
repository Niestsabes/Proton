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
    private Bounds boundPlanet;

    // Update is called once per frame
    void Update()
    {
        this.MoveCameraByMousePos(Mouse.current.position.ReadValue());
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
}
