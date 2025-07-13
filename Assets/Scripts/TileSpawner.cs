using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    [SerializeField] private GameObject dollarTilePrefab;
    [SerializeField] private Vector2 gridOrigin = Vector2.zero;
    [SerializeField] private int gridWidth = 15;
    [SerializeField] private int gridHeight = 7;
    [SerializeField] private float gridSize = 1f;

    private void Start()
    {
        SpawnDollarTiles();
    }

    private void SpawnDollarTiles()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector2 spawnPos = gridOrigin + new Vector2(x * gridSize, y * gridSize);

                // Check if a GoalTile or FallenTile is already at that position
                Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPos, 0.1f);
                bool skip = false;
                foreach (var col in colliders)
                {
                    if (col.CompareTag("GoalTile") || col.CompareTag("FallenTile"))
                    {
                        skip = true;
                        break;
                    }
                }

                if (!skip)
                {
                    Instantiate(dollarTilePrefab, spawnPos, Quaternion.identity);
                }
            }
        }
    }
}
