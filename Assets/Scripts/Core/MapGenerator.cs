using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    // 1. �� ��� Ÿ�� ����
    public enum TileType
    {
        Empty, // �� ����
        Wall, // ��
        Road, // ��
        Item, // ������ ��ġ
        Trap // ���� ��ġ
    }

    // 2. �� ũ�� �� ������ ����
    public int width = 20; // �� ���� ũ��
    public int height = 15; // �� ���� ũ��
    public GameObject wallPrefab; // �� ������
    public GameObject roadPrefab; // �� ������
    public GameObject itemPrefab; // ������ ������
    public GameObject trapPrefab; // ���� ������

    // 3. �� ������ ����
    private TileType[,] map;

    // 4. ��ũ��Ʈ ���� �� �� ����
    void Start()
    {
        map = GenerateMap(width, height); // �� ����
        GenerateMesh(map); // 3D �޽� ���� �� ������Ʈ ��ġ
    }

    // 5. �� ���� �Լ�
    TileType[,] GenerateMap(int width, int height)
    {
        // 5.1. 2���� �迭 ����
        TileType[,] map = new TileType[width, height];

        // 5.2. �� �ܰ� ������ ä���
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                {
                    map[x, y] = TileType.Wall;
                }
            }
        }

        // 5.3. ���� ���� ���� (����: ������ �� ����)
        int numRooms = 5; // �� ����
        for (int i = 0; i < numRooms; i++)
        {
            int roomWidth = Random.Range(5, 10); // �� ���� ũ��
            int roomHeight = Random.Range(5, 10); // �� ���� ũ��
            int roomX = Random.Range(2, width - roomWidth - 2); // �� ���� x ��ǥ (������ ���� ���)
            int roomY = Random.Range(2, height - roomHeight - 2); // �� ���� y ��ǥ (������ ���� ���)

            for (int x = roomX; x < roomX + roomWidth; x++)
            {
                for (int y = roomY; y < roomY + roomHeight; y++)
                {
                    map[x, y] = TileType.Road;
                }
            }
        }

        // 5.4. ��� ���� (����: ������ �� ����, �ܼ�ȭ)
        for (int i = 0; i < numRooms - 1; i++)
        {
            // ������ �� �� ����
            int room1X = Random.Range(2, width - 2);
            int room1Y = Random.Range(2, height - 2);
            int room2X = Random.Range(2, width - 2);
            int room2Y = Random.Range(2, height - 2);

            // �� �� ���̿� ��� ���� (����)
            int x = room1X;
            int y = room1Y;
            while (x != room2X || y != room2Y)
            {
                if (x < room2X) x++;
                else if (x > room2X) x--;

                if (y < room2Y) y++;
                else if (y > room2Y) y--;

                map[x, y] = TileType.Road;
            }
        }

        // 5.5. ������/���� ��ġ (����: ���� ��ġ�� ��ġ)
        int numItems = 10; // ������ ����
        for (int i = 0; i < numItems; i++)
        {
            int x = Random.Range(1, width - 2); // ������ ���� ���
            int y = Random.Range(1, height - 2); // ������ ���� ���
            map[x, y] = TileType.Item;
        }

        int numTraps = 5; // ���� ����
        for (int i = 0; i < numTraps; i++)
        {
            int x = Random.Range(1, width - 2); // ������ ���� ���
            int y = Random.Range(1, height - 2); // ������ ���� ���
            map[x, y] = TileType.Trap;
        }

        return map; // ������ �� ��ȯ
    }

    // 6. 3D �޽� ���� �� ������Ʈ ��ġ �Լ�
    void GenerateMesh(TileType[,] map)
    {
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                Vector3 position = new Vector3(x, 0, y); // ������Ʈ ��ġ ���

                // �� ��� Ÿ�Կ� ���� ������Ʈ ���� �� ��ġ
                switch (map[x, y])
                {
                    case TileType.Wall:
                        Instantiate(wallPrefab, position, Quaternion.identity); // �� ����
                        break;
                    case TileType.Road:
                        Instantiate(roadPrefab, position, Quaternion.identity); // �� ����
                        break;
                    case TileType.Item:
                        Instantiate(itemPrefab, position, Quaternion.identity); // ������ ����
                        break;
                    case TileType.Trap:
                        Instantiate(trapPrefab, position, Quaternion.identity); // ���� ����
                        break;
                }
            }
        }
    }
}
