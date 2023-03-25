using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject galaxyPrefab;

    private GalaxyObject galaxyObject;
    private GalaxyFactory galaxyFactory = new GalaxyFactory();

    // Start is called before the first frame update
    void Start()
    {
        Galaxy galaxy = this.galaxyFactory.GenerateGalaxy(10);
        this.galaxyObject = GalaxyObject.InstantiateObject(galaxy, this.galaxyPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
