using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager instance
    {
        get { return GameSceneManager._instance; }
    }
    private static GameSceneManager _instance;

    [Header("Prefabs")]
    [SerializeField] private GameObject galaxyPrefab;

    [Header("GameObjects")]
    [SerializeField] private GalaxyCameraController cameraController;

    private GalaxyObject galaxyObject;
    private GalaxyFactory galaxyFactory = new GalaxyFactory();

    // Start is called before the first frame update
    void Start()
    {
        if (GameSceneManager.instance != null) { Destroy(GameSceneManager.instance); }
        GameSceneManager._instance = this;

        Galaxy galaxy = this.galaxyFactory.GenerateGalaxy(10);
        this.galaxyObject = GalaxyObject.InstantiateObject(galaxy, this.galaxyPrefab);
        this.cameraController.AttackGalaxyObject(this.galaxyObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
