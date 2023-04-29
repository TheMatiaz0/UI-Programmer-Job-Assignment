using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell
{
    public GameObject Object;

    public Cell(GameObject obj)
    {
        this.Object = obj;
    }
}

public class ImageCell : Cell
{
    public Image Image;

    public ImageCell(GameObject obj, Image image) : base(obj)
    {
        this.Image = image;
    }
}

public enum Corner
{
    BottomLeft,
    TopLeft,
    TopRight,
    BottomRight,
}

public class GridPlacement : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private GameObject cellPrefab;
    [SerializeField]
    private RectTransform grid;
    [Header("Values")]
    [SerializeField]
    private int gridCols = 11;
    [SerializeField]
    private int gridRows = 11;
    [SerializeField]
    private float cellSize = 32;
    [SerializeField]
    private Vector2 offset = new(0.2f, 0.2f);
    [SerializeField]
    private Corner corner = Corner.BottomLeft;
    [SerializeField]
    private Vector2 padding;
    [SerializeField]
    private Color[] possibleColors;

    private ImageCell[,] gridArray;
    private Vector2 chosenCorner;
    private Vector2 center;

    private void Awake()
    {
        var worldCorners = new Vector3[4];
        grid.GetWorldCorners(worldCorners);
        chosenCorner = (Vector2)worldCorners[(int)corner];
        center = new Vector2(cellSize, cellSize) * .5f;
        gridArray = new ImageCell[gridCols, gridRows];
    }

    private void Start()
    {
        InitializeCells();
        // StartCoroutine(UpdateCells());
        // StartCoroutine(RandomSpawn());
    }

    private void Update()
    {
        for (int x = 0; x < gridCols; x++)
        {
            for (int y = 0; y < gridRows; y++)
            {
                Destroy(gridArray[x, y]?.Object);
            }
        }
        InitializeCells();
    }

    /*
    private IEnumerator UpdateCells()
    {
        while (true)
        {
            for (int x = 0; x < gridCols; x++)
            {
                for (int y = 0; y < gridRows; y++)
                {
                    Destroy(gridArray[x, y]?.Object);
                }
            }
            InitializeCells();
            yield return new WaitForSeconds(2);
        }
    }
    */

    public void SetCell(Vector2Int cellPosition)
    {
        if (IsPossibleCell(cellPosition))
        {
            var gridCell = gridArray[cellPosition.x, cellPosition.y];
            if (gridCell.Object != null)
            {
                Destroy(gridCell.Object);
            }
            else
            {
                SpawnCell(cellPosition);
            }
        }
    }

    private IEnumerator RandomSpawn()
    {
        List<Vector2Int> previousCells = new();
        while (true)
        {
            foreach (var previousCell in previousCells)
            {
                var gridCell = gridArray[previousCell.x, previousCell.y];
                Destroy(gridCell.Object);
            }

            var rndCount = Random.Range(2, 5);
            for (int i = 0; i < rndCount; i++)
            {
                Vector2Int rndPosition = new(Random.Range(0, gridCols), Random.Range(0, gridRows));
                var imageCell = SpawnCell(rndPosition) as ImageCell;
                imageCell.Image.color = possibleColors[Random.Range(0, possibleColors.Length)];
                previousCells.Add(rndPosition);
            }
            yield return new WaitForSeconds(2);
        }
    }

    private void InitializeCells()
    {
        for (int x = 0; x < gridCols; x++)
        {
            for (int y = 0; y < gridRows; y++)
            {
                SpawnCell(new(x, y));
            }
        }
    }

    private Cell SpawnCell(Vector2Int cellPosition)
    {
        var newGameObject = Instantiate(cellPrefab, GetWorldPosition(cellPosition) + center, Quaternion.identity, this.transform);
        ImageCell cell = new(newGameObject, newGameObject.GetComponent<Image>());
        gridArray[cellPosition.x, cellPosition.y] = cell;
        newGameObject.name = $"Cell {cellPosition.x} {cellPosition.y}";
        return cell;
    }

    private Vector2 GetWorldPosition(Vector2Int cellPosition)
    {
        return new Vector2(cellPosition.x + (offset.x * cellPosition.x), cellPosition.y + (offset.y * cellPosition.y)) * cellSize + chosenCorner + padding;
    }

    private void GetXY(Vector2 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - chosenCorner - padding).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - chosenCorner - padding).y / cellSize);
    }

    private bool IsPossibleCell(Vector2Int cellPosition)
    {
        return cellPosition.x >= 0 && cellPosition.y >= 0 && cellPosition.x < gridCols && cellPosition.y < gridRows;
    }
}
