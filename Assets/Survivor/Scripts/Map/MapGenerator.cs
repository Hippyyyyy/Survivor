using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField]
    Transform cameraTransform;

    [SerializeField]
    OpenLayoutController openLayoutPrefab;

    [SerializeField]
    Sprite mapSprite;

    Dictionary<Vector2, OpenLayoutController> activeTiles;
    Queue<OpenLayoutController> inactiveTiles;
    Vector2 cameraCurrentTile;
    Vector2 tileSize = new Vector2(30.72f, 30.72f);

    private void Start()
    {
        InitializeTiles();
        InitializeStartingTiles();
    }

    private void Update()
    {
        Vector2 newCameraTile = GetCameraTilePosition();
        if (newCameraTile != cameraCurrentTile)
        {
            cameraCurrentTile = newCameraTile;
            UpdateTiles();
        }
    }

    private void InitializeTiles()
    {
        activeTiles = new Dictionary<Vector2, OpenLayoutController>();
        inactiveTiles = new Queue<OpenLayoutController>();
        cameraCurrentTile = Vector2.zero;
    }

    private void InitializeStartingTiles()
    {
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Vector2 tilePosition = new Vector2(x, y);
                SpawnTile(tilePosition);
            }
        }
    }

    private Vector2 GetCameraTilePosition()
    {
        return new Vector2(
            Mathf.FloorToInt(cameraTransform.position.x / tileSize.x),
            Mathf.FloorToInt(cameraTransform.position.y / tileSize.y)
        );
    }

    private void UpdateTiles()
    {
        HashSet<Vector2> newTilePositions = GetNewTilePositions();

        RepositionOrSpawnTiles(newTilePositions);

        RemoveInactiveTiles(newTilePositions);
    }

    private HashSet<Vector2> GetNewTilePositions()
    {
        HashSet<Vector2> newTilePositions = new HashSet<Vector2>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Vector2 tilePosition = cameraCurrentTile + new Vector2(x, y);
                newTilePositions.Add(tilePosition);
            }
        }

        return newTilePositions;
    }

    private void RepositionOrSpawnTiles(HashSet<Vector2> newTilePositions)
    {
        foreach (var tilePosition in newTilePositions)
        {
            if (!activeTiles.ContainsKey(tilePosition))
            {
                RepositionTile(tilePosition);
            }
        }
    }

    private void RemoveInactiveTiles(HashSet<Vector2> newTilePositions)
    {
        List<Vector2> tilesToRemove = new List<Vector2>();

        foreach (var tile in activeTiles)
        {
            if (!newTilePositions.Contains(tile.Key))
            {
                inactiveTiles.Enqueue(tile.Value);
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
        Vector3 worldPosition = GetWorldPosition(tilePosition);
        OpenLayoutController newTile = Instantiate(openLayoutPrefab, worldPosition, Quaternion.identity);
        newTile.SetMapSprite(mapSprite);
        activeTiles[tilePosition] = newTile;
    }

    private void RepositionTile(Vector2 newTilePosition)
    {
        if (inactiveTiles.Count > 0)
        {
            OpenLayoutController tileToReuse = inactiveTiles.Dequeue();
            Vector3 worldPosition = GetWorldPosition(newTilePosition);
            tileToReuse.transform.position = worldPosition;
            activeTiles[newTilePosition] = tileToReuse;
        }
        else
        {
            SpawnTile(newTilePosition);
        }
    }

    private Vector3 GetWorldPosition(Vector2 tilePosition)
    {
        return new Vector3(tilePosition.x * tileSize.x, tilePosition.y * tileSize.y, 0);
    }
}

[System.Serializable]
public enum LayoutEnum
{
    OpenLayout,
    VerticalLayout,
    SquareLayout
}
