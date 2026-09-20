using UnityEngine;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
    public GameObject floorPrefab;
    public Transform player;
    public int gridWidth = 40;
    public int gridHeight = 40;
    public int roomCount = 6;
    public int roomMinSize = 6;
    public int roomMaxSize = 14;
    public GameObject enemyPrefab;
    public float minEnemyDistanceFromPlayer = 15f;

    private bool[,] grid;
    private List<RectInt> rooms = new List<RectInt>();

    void Start()
    {
        grid = new bool[gridWidth, gridHeight];
        GenerateRooms();
        ConnectRooms();
        DrawLevel();
        PlacePlayer();
        SpawnEnemy();
    }

    void GenerateRooms()
    {
        int maxAttempts = 100;

        for (int i = 0; i < roomCount; i++)
        {
            RectInt newRoom = new RectInt();
            bool validPlacement = false;
            int attempts = 0;

            while (!validPlacement && attempts < maxAttempts)
            {
                int w = Random.Range(roomMinSize, roomMaxSize);
                int h = Random.Range(roomMinSize, roomMaxSize);
                int x = Random.Range(1, gridWidth - w - 1);
                int y = Random.Range(1, gridHeight - h - 1);

                newRoom = new RectInt(x, y, w, h);

                if (!RoomOverlapsAny(newRoom))
                {
                    validPlacement = true;
                }

                attempts++;
            }

            if (validPlacement)
            {
                rooms.Add(newRoom);
                CarveRoom(newRoom);
            }
        }
    }

    bool RoomOverlapsAny(RectInt room)
    {
        RectInt roomWithBuffer = new RectInt(
            room.x - 1, room.y - 1,
            room.width + 2, room.height + 2
        );

        foreach (RectInt existingRoom in rooms)
        {
            if (roomWithBuffer.Overlaps(existingRoom))
            {
                return true;
            }
        }
        return false;
    }

    void CarveRoom(RectInt room)
    {
        for (int x = room.xMin; x < room.xMax; x++)
        {
            for (int y = room.yMin; y < room.yMax; y++)
            {
                grid[x, y] = true;
            }
        }
    }

    void ConnectRooms()
    {
        for (int i = 1; i < rooms.Count; i++)
        {
            Vector2Int centerA = Vector2Int.RoundToInt(rooms[i - 1].center);
            Vector2Int centerB = Vector2Int.RoundToInt(rooms[i].center);
            CarveCorridor(centerA, centerB);
        }
    }

    public int corridorWidth = 2;

    void CarveCorridor(Vector2Int a, Vector2Int b)
    {
        int x = a.x;
        int y = a.y;

        while (x != b.x)
        {
            CarveThickPoint(x, y);
            x += (b.x > x) ? 1 : -1;
        }
        while (y != b.y)
        {
            CarveThickPoint(x, y);
            y += (b.y > y) ? 1 : -1;
        }
    }

    void CarveThickPoint(int centerX, int centerY)
    {
        for (int x = centerX; x < centerX + corridorWidth; x++)
        {
            for (int y = centerY; y < centerY + corridorWidth; y++)
            {
                if (x >= 0 && x < gridWidth && y >= 0 && y < gridHeight)
                {
                    grid[x, y] = true;
                }
            }
        }
    }

    void DrawLevel()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if (grid[x, y])
                {
                    Vector3 pos = new Vector3(x, 0f, y);
                    Instantiate(floorPrefab, pos, Quaternion.identity);
                }
            }
        }
    }

    void PlacePlayer()
    {
        Vector2 center = rooms[0].center;
        player.position = new Vector3(center.x, 1f, center.y);
    }

    void SpawnEnemy()
    {
        List<RectInt> validRooms = new List<RectInt>();

        foreach (RectInt room in rooms)
        {
            float distance = Vector2.Distance(room.center, rooms[0].center);
            if (distance >= minEnemyDistanceFromPlayer)
            {
                validRooms.Add(room);
            }
        }

        if (validRooms.Count == 0)
        {
            Debug.Log("Brak pokoju wystarczająco daleko od gracza - pomijam spawn przeciwnika");
            return;
        }

        RectInt chosenRoom = validRooms[Random.Range(0, validRooms.Count)];
        Vector2 center = chosenRoom.center;
        Vector3 spawnPos = new Vector3(center.x, 1f, center.y);

        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        enemy.GetComponent<EnemyAI>().player = player;
    }
}