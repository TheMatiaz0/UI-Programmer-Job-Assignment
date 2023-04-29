using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public GameObject Object;

    public Cell(GameObject obj)
    {
        this.Object = obj;
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

    private Cell[,] gridArray;
    private Vector2 chosenCorner;

    private void Awake()
    {
        var worldCorners = new Vector3[4];
        grid.GetWorldCorners(worldCorners);
        chosenCorner = (Vector2)worldCorners[(int)corner];
    }

    private void Start()
    {
        InitializeCells();
    }

    private void InitializeCells()
    {
        gridArray = new Cell[gridCols, gridRows];
        for (int x = 0; x < gridCols; x++)
        {
            for (int y = 0; y < gridRows; y++)
            {
                SpawnCell(new(x, y));
            }
        }
    }

    private void SpawnCell(Vector2Int cellPosition)
    {
        var newGameObject = Instantiate(cellPrefab, GetWorldPositionCell(cellPosition), Quaternion.identity, this.transform);
        var cell = new Cell(newGameObject);
        gridArray[cellPosition.x, cellPosition.y] = cell;
        newGameObject.name = $"Cell {cellPosition.x} {cellPosition.y}";
    }

    private Vector2 GetWorldPositionCell(Vector2Int cellPosition)
    {
        return GetWorldPosition(cellPosition) + new Vector2(cellSize, cellSize) * .5f;
    }

    private Vector2 GetWorldPosition(Vector2Int cellPosition)
    {
        return new Vector2(cellPosition.x + (offset.x * cellPosition.x), cellPosition.y + (offset.y * cellPosition.y)) * cellSize + chosenCorner + padding;
    }

    private void GetXY(Vector2 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - chosenCorner).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - chosenCorner).y / cellSize);
    }

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

    private bool IsPossibleCell(Vector2Int cellPosition)
    {
        return cellPosition.x >= 0 && cellPosition.y >= 0 && cellPosition.x < gridCols && cellPosition.y < gridRows;
    }
}
