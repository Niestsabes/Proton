using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
[RequireComponent(typeof(CanvasGroup))]
public class TransitionCanvas : MonoBehaviour
{
    [Header("Game Object Components")]
    [SerializeField] private GameObject tileTemplate;

    [Header("Params")]
    [SerializeField] private Vector2 nbTiles;
    [SerializeField] private float transitionTime = 1;

    private GridLayoutGroup gridLayoutGroup;
    private CanvasGroup canvasGroup;
    private List<GameObject> listTile;

    void Awake()
    {
        this.gridLayoutGroup = this.GetComponent<GridLayoutGroup>();
        this.canvasGroup = this.GetComponent<CanvasGroup>();
        this.InstantiateTiles();
    }

    void Start()
    {
        StartCoroutine(this.Open());
    }

    public IEnumerator Open()
    {
        var listRandTile = CustomRandom.RandomizeList(this.listTile);
        foreach (var tile in listRandTile)
        {
            tile.GetComponent<Image>().color = Color.clear;
            yield return new WaitForSeconds(this.transitionTime / (this.nbTiles.x * this.nbTiles.y));
        }
        this.canvasGroup.interactable = false;
        this.canvasGroup.blocksRaycasts = false;
    }

    public IEnumerator Close()
    {
        var listRandTile = CustomRandom.RandomizeList(this.listTile);
        foreach (var tile in listRandTile) {
            tile.GetComponent<Image>().color = Color.black;
            yield return new WaitForSeconds(this.transitionTime / (this.nbTiles.x * this.nbTiles.y));
        }
        this.canvasGroup.interactable = true;
        this.canvasGroup.blocksRaycasts = true;
    }

    private void InstantiateTiles()
    {
        this.gridLayoutGroup.cellSize = new Vector2(Screen.width / this.nbTiles.x, Screen.height / this.nbTiles.y);
        this.listTile = new List<GameObject>();
        for (int x = 0; x < this.nbTiles.x * this.nbTiles.y; x++) {
            GameObject newTile = GameObject.Instantiate(this.tileTemplate, this.transform);
            newTile.SetActive(true);
            this.listTile.Add(newTile);
        }
    }

}
