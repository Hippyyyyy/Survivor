using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] GameObject[,] mapTiles = new GameObject[3, 3];
    [SerializeField] Vector2 tileSize; // Kích thước của mỗi ô vuông (x, y)
    [SerializeField] GameObject mapTilePrefab;
     Vector2 playerLastPos;
    [SerializeField] Camera camera;

    void Start()
    {
        playerLastPos = camera.transform.position;
        InitializeMap();
    }

    void Update()
    {
        UpdateMap();
    }

    void InitializeMap()
    {
        // Khởi tạo các ô vuông ban đầu
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                Vector2 position = new Vector2(x * tileSize.x, y * tileSize.y);
                mapTiles[x, y] = Instantiate(mapTilePrefab, position, Quaternion.identity);
            }
        }
    }

    void UpdateMap()
    {
        Vector2 playerPos = camera.transform.position;
        Vector2 movement = playerPos - playerLastPos;

        if (movement.magnitude >= tileSize.magnitude)
        {
            if (movement.x > 0)
            {
                MoveTiles(Vector2.right);
            }
            else if (movement.x < 0)
            {
                MoveTiles(Vector2.left);
            }

            if (movement.y > 0)
            {
                MoveTiles(Vector2.up);
            }
            else if (movement.y < 0)
            {
                MoveTiles(Vector2.down);
            }

            playerLastPos = playerPos;
        }
    }

    void MoveTiles(Vector2 direction)
    {
        if (direction == Vector2.right)
        {
            for (int y = 0; y < 3; y++)
            {
                Destroy(mapTiles[0, y]);
                for (int x = 0; x < 2; x++)
                {
                    mapTiles[x, y] = mapTiles[x + 1, y];
                }
                Vector2 newPos = mapTiles[1, y].transform.position + Vector3.right * tileSize.x;
                mapTiles[2, y] = Instantiate(mapTilePrefab, newPos, Quaternion.identity);
            }
        }
        else if (direction == Vector2.left)
        {
            for (int y = 0; y < 3; y++)
            {
                Destroy(mapTiles[2, y]);
                for (int x = 2; x > 0; x--)
                {
                    mapTiles[x, y] = mapTiles[x - 1, y];
                }
                Vector2 newPos = mapTiles[1, y].transform.position + Vector3.left * tileSize.x;
                mapTiles[0, y] = Instantiate(mapTilePrefab, newPos, Quaternion.identity);
            }
        }
        else if (direction == Vector2.up)
        {
            for (int x = 0; x < 3; x++)
            {
                Destroy(mapTiles[x, 0]);
                for (int y = 0; y < 2; y++)
                {
                    mapTiles[x, y] = mapTiles[x, y + 1];
                }
                Vector2 newPos = mapTiles[x, 1].transform.position + Vector3.up * tileSize.y;
                mapTiles[x, 2] = Instantiate(mapTilePrefab, newPos, Quaternion.identity);
            }
        }
        else if (direction == Vector2.down)
        {
            for (int x = 0; x < 3; x++)
            {
                Destroy(mapTiles[x, 2]);
                for (int y = 2; y > 0; y--)
                {
                    mapTiles[x, y] = mapTiles[x, y - 1];
                }
                Vector2 newPos = mapTiles[x, 1].transform.position + Vector3.down * tileSize.y;
                mapTiles[x, 0] = Instantiate(mapTilePrefab, newPos, Quaternion.identity);
            }
        }
    }
}

/*using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField]
    private Transform cameraTransform;

    [SerializeField]
    private OpenLayoutController openLayoutPrefab;

    private Dictionary<Vector2, OpenLayoutController> activeTiles;
    private Vector2 cameraCurrentTile;
    private Vector2 tileSize = new Vector2(-30.72f, 30.72f); // Kích thước của mỗi tile

    private void Start()
    {
        activeTiles = new Dictionary<Vector2, OpenLayoutController>();
        cameraCurrentTile = Vector2.zero;

        // Tạo các tile ban đầu xung quanh camera
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Vector2 tilePosition = new Vector2(x, y);
                SpawnTile(tilePosition);
            }
        }
    }

    private void Update()
    {
        Vector2 newCameraTile = new Vector2(
            Mathf.FloorToInt(cameraTransform.position.x / tileSize.x),
            Mathf.FloorToInt(cameraTransform.position.y / tileSize.y)
        );

        if (newCameraTile != cameraCurrentTile)
        {
            cameraCurrentTile = newCameraTile;
            UpdateTiles();
        }
    }

    private void UpdateTiles()
    {
        List<Vector2> newTilePositions = new List<Vector2>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Vector2 tilePosition = cameraCurrentTile + new Vector2(x, y);
                newTilePositions.Add(tilePosition);

                if (!activeTiles.ContainsKey(tilePosition))
                {
                    SpawnTile(tilePosition);
                }
            }
        }

        // Xóa các tile không còn trong phạm vi 3x3 quanh camera
        List<Vector2> tilesToRemove = new List<Vector2>();
        foreach (var tile in activeTiles)
        {
            if (!newTilePositions.Contains(tile.Key))
            {
                Destroy(tile.Value.gameObject);
                tilesToRemove.Add(tile.Key);
            }
        }

        foreach (var tilePos in tilesToRemove)
        {
            activeTiles.Remove(tilePos);
        }
    }

    private void SpawnTile(Vector2 tilePosition)
    {
        Vector3 worldPosition = new Vector3(tilePosition.x * tileSize.x, tilePosition.y * tileSize.y, 0);
        OpenLayoutController newTile = Instantiate(openLayoutPrefab, worldPosition, Quaternion.identity);
        activeTiles[tilePosition] = newTile;
    }
}

[System.Serializable]
public enum LayoutEnum
{
    OpenLayout,
    VerticalLayout,
    SquareLayout
}
*/