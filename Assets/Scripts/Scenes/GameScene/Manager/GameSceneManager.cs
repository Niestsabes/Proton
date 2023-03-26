using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : AbstractSceneManager
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
    [SerializeField] private GameObject enemyTravelerPrefab;

    [Header("GameObjects")]
    public GalaxyCameraController cameraController;
    public DeviceUI deviceUI;

    [Header("Params")]
    public int nbTurnBeforeEnemySpawn = 2;
    public int nbTurnBeforeSwap = 3;

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
        this.SetupUI();
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
        this.playerTravelerObject.SetBody(0);
        this.allyTravelerObject = TravelerObject.InstantiateObject(this.travelerPrefab, this.startPlanetObject);
        this.allyTravelerObject.AddController(true);
        this.allyTravelerObject.SetBody(1);
        this.allyTravelerObject.SetVisible(false);
        this.listEnemyTravelerObject = new List<TravelerObject>();
    }

    private void GenerateEnemyTraveler()
    {
        TravelerObject newEnemy = TravelerObject.InstantiateObject(this.enemyTravelerPrefab, this.startPlanetObject);
        newEnemy.AddController(true);
        this.listEnemyTravelerObject.Add(newEnemy);
    }

    private void SetupUI()
    {
        this.deviceUI.Close();
    }

    private IEnumerator RunGame()
    {
        this.nbTurn = 0;
        while (true) {
            this.nbTurn++;
            yield return this.RunPlayerPhase();
            yield return this.RunAllyPhase();
            yield return this.RunEnemyPhase();
            if (this.nbTurn == this.nbTurnBeforeSwap) yield return this.RunSwapPhase();
        }
    }

    private IEnumerator RunPlayerPhase()
    {
        this.currentPhase = Phase.PLAYER;

        yield return this.cameraController.ResetCamera();
        var listPlanetToShow = this.playerTravelerObject.currentPlanet.GetNeighborPlanets();
        listPlanetToShow.Add(this.playerTravelerObject.currentPlanet);
        yield return this.cameraController.MoveCameraToBoundPlanets(listPlanetToShow);
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
            yield return this.cameraController.MoveToPosition(enemy.transform.position);
            yield return new WaitForSeconds(0.5f);
            yield return enemy.controller.Act();
            yield return new WaitForSeconds(0.2f);
        }
        if (this.nbTurn % this.nbTurnBeforeEnemySpawn == 1) {
            yield return this.cameraController.MoveToPosition(this.startPlanetObject.transform.position);
            yield return new WaitForSeconds(0.5f);
            this.GenerateEnemyTraveler();
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(0.5f); ;
    }

    private IEnumerator RunSwapPhase()
    {
        yield return null;
    }
}
