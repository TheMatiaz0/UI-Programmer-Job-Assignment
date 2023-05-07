using System.Collections;
using UnityEngine;

public enum RectCorner
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
    private Cell cellPrefab;
    [SerializeField]
    private RectTransform grid;
    [Header("Values")]
    [SerializeField]
    private float cellSize = 32;
    [SerializeField]
    private Vector2 offset = new(0.2f, 0.2f);
    [SerializeField]
    private RectCorner corner = RectCorner.BottomLeft;
    [SerializeField]
    private Vector2 padding;
    [field: SerializeField]
    public int Columns { get; private set; } = 11;
    [field: SerializeField]
    public int Rows { get; private set; } = 11;

    public Cell[,] Cells { get; private set; }

    private Vector2 vectCorner;
    private Vector2 center;

    private void Awake()
    {
        StartCoroutine(Setup());
    }

    private IEnumerator Setup()
    {
        yield return new WaitForEndOfFrame();
        var worldCorners = new Vector3[4];
        grid.GetWorldCorners(worldCorners);
        vectCorner = (Vector2)worldCorners[(int)corner];
        center = new Vector2(cellSize, cellSize) * .5f;
        Cells = new Cell[Columns, Rows];
    }

    public void SetCell(Vector2Int cellPosition)
    {
        if (IsPossibleCell(cellPosition))
        {
            var gridCell = Cells[cellPosition.x, cellPosition.y];
            if (gridCell.gameObject != null)
            {
                Destroy(gridCell.gameObject);
            }
            else
            {
                SpawnCell(cellPosition);
            }
        }
    }

    public Cell SpawnCell(Vector2Int cellPosition)
    {
        Cell cell = Instantiate(cellPrefab, GetWorldPosition(cellPosition) + center, Quaternion.identity, this.transform);
        Cells[cellPosition.x, cellPosition.y] = cell;
        cell.gameObject.name = $"Cell {cellPosition.x} {cellPosition.y}";
        return cell;
    }

    private Vector2 GetWorldPosition(Vector2Int cellPosition)
    {
        return new Vector2(cellPosition.x + (offset.x * cellPosition.x), cellPosition.y + (offset.y * cellPosition.y)) * cellSize + vectCorner + padding;
    }

    private void GetXY(Vector2 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - vectCorner - padding).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - vectCorner - padding).y / cellSize);
    }

    private bool IsPossibleCell(Vector2Int cellPosition)
    {
        return cellPosition.x >= 0 && cellPosition.y >= 0 && cellPosition.x < Columns && cellPosition.y < Rows;
    }
}
