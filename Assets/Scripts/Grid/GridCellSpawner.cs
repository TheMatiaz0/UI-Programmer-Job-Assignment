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
        List<Vector2Int> previousCells = new();
        while (true)
        {
            foreach (var previousCell in previousCells)
            {
                var gridCell = grid.Cells[previousCell.x, previousCell.y];
                Destroy(gridCell.gameObject);
            }

            var rndCount = Random.Range(2, 5);
            previousCells = new();
            for (int i = 0; i < rndCount; i++)
            {
                Vector2Int rndPosition = new(Random.Range(0, grid.Columns), Random.Range(0, grid.Rows));
                var cell = grid.SpawnCell(rndPosition);
                cell.Image.color = possibleColors[Random.Range(0, possibleColors.Length)];
                previousCells.Add(rndPosition);
            }
            yield return new WaitForSeconds(Random.Range(minMaxRefreshTime.x, minMaxRefreshTime.y));
        }
    }
}
