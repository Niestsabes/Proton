using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    public enum Phase { PLAYER, ALLY, ENEMY, OTHER }

    public static GameSceneManager instance
    {
        get { return GameSceneManager._instance; }
    }
    private static GameSceneManager _instance;
    

    [Header("Prefabs")]
    [SerializeField] private GameObject galaxyPrefab;
    [SerializeField] private GameObject travelerPrefab;

    [Header("GameObjects")]
    [SerializeField] private GalaxyCameraController cameraController;

    // ===== Game status
    public Phase currentPhase { get; private set; } = GameSceneManager.Phase.OTHER;
    public int nbTurn { get; private set; }
    private GalaxyObject galaxyObject;
    private GalaxyPlanetObject startPlanetObject;
    private TravelerObject playerTravelerObject;
    private TravelerObject allyTravelerObject;
    private List<TravelerObject> listEnemyTravelerObject;

    // ===== Services
    public GameEventManager eventManager { get; private set; } = new GameEventManager();
    public GalaxyFactory galaxyFactory { get; private set; } = new GalaxyFactory();
    public GalaxyRepository galaxyRepository { get; private set; } = new GalaxyRepository();

    void Awake()
    {
        if (GameSceneManager.instance != null) { Destroy(GameSceneManager.instance); }
        GameSceneManager._instance = this;
    }

    void Start()
    {
        this.GenerateGalaxy();
        this.GenerateTravelers();
        this.cameraController.AttackGalaxyObject(this.galaxyObject);

        StartCoroutine(this.RunGame());
    }

    private void GenerateGalaxy()
    {
        Galaxy galaxy = this.galaxyFactory.GenerateGalaxyFromSerializable(this.galaxyRepository.GetById(1));
        this.galaxyObject = GalaxyObject.InstantiateObject(galaxy, this.galaxyPrefab);
    }

    private void GenerateTravelers()
    {
        this.startPlanetObject = CustomRandom.RandomInArray(this.galaxyObject.listPlanetObject);
        this.playerTravelerObject = TravelerObject.InstantiateObject(this.travelerPrefab, this.startPlanetObject);
        this.playerTravelerObject.AddController(false);
        this.allyTravelerObject = TravelerObject.InstantiateObject(this.travelerPrefab, this.startPlanetObject);
        this.allyTravelerObject.AddController(true);
        this.listEnemyTravelerObject = new List<TravelerObject>();
    }

    private void GenerateEnemyTraveler()
    {
        TravelerObject newEnemy = TravelerObject.InstantiateObject(this.travelerPrefab, this.startPlanetObject);
        newEnemy.AddController(true);
        this.listEnemyTravelerObject.Add(newEnemy);
    }

    private IEnumerator RunGame()
    {
        this.nbTurn = 0;
        while (true) {
            this.nbTurn++;
            yield return this.RunPlayerPhase();
            yield return this.RunAllyPhase();
            yield return this.RunEnemyPhase();
        }
    }

    private IEnumerator RunPlayerPhase()
    {
        this.currentPhase = Phase.PLAYER;
        yield return this.playerTravelerObject.controller.Act();
    }

    private IEnumerator RunAllyPhase()
    {
        this.currentPhase = Phase.ALLY;
        yield return this.allyTravelerObject.controller.Act();
    }

    private IEnumerator RunEnemyPhase()
    {
        this.currentPhase = Phase.ENEMY;
        foreach (TravelerObject enemy in this.listEnemyTravelerObject) {
            yield return enemy.controller.Act();
        }
        if (this.nbTurn % 5 == 1) { this.GenerateEnemyTraveler(); }
        yield return null;
    }
}
