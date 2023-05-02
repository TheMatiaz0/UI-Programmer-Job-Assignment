using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCellSpawner : MonoBehaviour
{
    [SerializeField]
    private GridPlacement grid;
    [SerializeField]
    private Color[] possibleColors;
    [SerializeField]
    private Vector2 minMaxRefreshTime; 
    [SerializeField]
    private bool isDebugging = false;

    private void Start()
    {
        StartCoroutine(RandomSpawn());
    }

    private void Update()
    {
        if (isDebugging)
        {
            for (int x = 0; x < grid.Columns; x++)
            {
                for (int y = 0; y < grid.Rows; y++)
                {
                    Destroy(grid.Cells[x, y].gameObject);
                }
            }
            InitializeCells();
        }
    }

    private void InitializeCells()
    {
        for (int x = 0; x < grid.Columns; x++)
        {
            for (int y = 0; y < grid.Rows; y++)
            {
                grid.SpawnCell(new(x, y));
            }
        }
    }

    private IEnumerator RandomSpawn()
    {
        List<Cell> previousCells = new();
        while (true)
        {
            foreach (var previousCell in previousCells)
            {
                Destroy(previousCell.gameObject);
            }

            var rndCount = Random.Range(2, 5);
            previousCells = new();
            for (int i = 0; i < rndCount; i++)
            {
                var rndColumn = Random.Range(0, grid.Columns);
                var rndRow = Random.Range(0, grid.Rows);
                Vector2Int rndPosition = new(rndColumn, rndRow);
                var cell = grid.SpawnCell(rndPosition);
                cell.Image.color = possibleColors[Random.Range(0, possibleColors.Length)];
                previousCells.Add(cell);
            }
            yield return new WaitForSeconds(Random.Range(minMaxRefreshTime.x, minMaxRefreshTime.y));
        }
    }
}
