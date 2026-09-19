using UnityEngine;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
    public GameObject floorPrefab;
    public int gridWidth = 40;
    public int gridHeight = 40;
    public int roomCount = 6;
    public int roomMinSize = 4;
    public int roomMaxSize = 8;

    private bool[,] grid;
    private List<RectInt> rooms = new List<RectInt>();

    void Start()
    {
        grid = new bool[gridWidth, gridHeight];
        GenerateRooms();
        ConnectRooms();
        DrawLevel();
    }

    void GenerateRooms()
    {
        for (int i = 0; i < roomCount; i++)
        {
            int w = Random.Range(roomMinSize, roomMaxSize);
            int h = Random.Range(roomMinSize, roomMaxSize);
            int x = Random.Range(1, gridWidth - w - 1);
            int y = Random.Range(1, gridHeight - h - 1);

            RectInt newRoom = new RectInt(x, y, w, h);
            rooms.Add(newRoom);
            CarveRoom(newRoom);
        }
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

    void CarveCorridor(Vector2Int a, Vector2Int b)
    {
        int x = a.x;
        int y = a.y;

        // najpierw idziemy w poziomie
        while (x != b.x)
        {
            grid[x, y] = true;
            x += (b.x > x) ? 1 : -1;
        }
        // potem w pionie
        while (y != b.y)
        {
            grid[x, y] = true;
            y += (b.y > y) ? 1 : -1;
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
}