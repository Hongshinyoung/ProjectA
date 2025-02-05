using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    // 1. 맵 요소 타입 정의
    public enum TileType
    {
        Empty, // 빈 공간
        Wall, // 벽
        Road, // 길
        Item, // 아이템 위치
        Trap // 함정 위치
    }

    // 2. 맵 크기 및 프리팹 설정
    public int width = 20; // 맵 가로 크기
    public int height = 15; // 맵 세로 크기
    public GameObject wallPrefab; // 벽 프리팹
    public GameObject roadPrefab; // 길 프리팹
    public GameObject itemPrefab; // 아이템 프리팹
    public GameObject trapPrefab; // 함정 프리팹

    // 3. 맵 데이터 저장
    private TileType[,] map;

    // 4. 스크립트 시작 시 맵 생성
    void Start()
    {
        map = GenerateMap(width, height); // 맵 생성
        GenerateMesh(map); // 3D 메시 생성 및 오브젝트 배치
    }

    // 5. 맵 생성 함수
    TileType[,] GenerateMap(int width, int height)
    {
        // 5.1. 2차원 배열 생성
        TileType[,] map = new TileType[width, height];

        // 5.2. 맵 외곽 벽으로 채우기
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

        // 5.3. 내부 공간 생성 (예시: 랜덤한 방 생성)
        int numRooms = 5; // 방 개수
        for (int i = 0; i < numRooms; i++)
        {
            int roomWidth = Random.Range(5, 10); // 방 가로 크기
            int roomHeight = Random.Range(5, 10); // 방 세로 크기
            int roomX = Random.Range(2, width - roomWidth - 2); // 방 시작 x 좌표 (벽과의 간격 고려)
            int roomY = Random.Range(2, height - roomHeight - 2); // 방 시작 y 좌표 (벽과의 간격 고려)

            for (int x = roomX; x < roomX + roomWidth; x++)
            {
                for (int y = roomY; y < roomY + roomHeight; y++)
                {
                    map[x, y] = TileType.Road;
                }
            }
        }

        // 5.4. 통로 연결 (예시: 랜덤한 방 연결, 단순화)
        for (int i = 0; i < numRooms - 1; i++)
        {
            // 임의의 두 방 선택
            int room1X = Random.Range(2, width - 2);
            int room1Y = Random.Range(2, height - 2);
            int room2X = Random.Range(2, width - 2);
            int room2Y = Random.Range(2, height - 2);

            // 두 방 사이에 통로 생성 (직선)
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

        // 5.5. 아이템/함정 배치 (예시: 랜덤 위치에 배치)
        int numItems = 10; // 아이템 개수
        for (int i = 0; i < numItems; i++)
        {
            int x = Random.Range(1, width - 2); // 벽과의 간격 고려
            int y = Random.Range(1, height - 2); // 벽과의 간격 고려
            map[x, y] = TileType.Item;
        }

        int numTraps = 5; // 함정 개수
        for (int i = 0; i < numTraps; i++)
        {
            int x = Random.Range(1, width - 2); // 벽과의 간격 고려
            int y = Random.Range(1, height - 2); // 벽과의 간격 고려
            map[x, y] = TileType.Trap;
        }

        return map; // 생성된 맵 반환
    }

    // 6. 3D 메시 생성 및 오브젝트 배치 함수
    void GenerateMesh(TileType[,] map)
    {
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                Vector3 position = new Vector3(x, 0, y); // 오브젝트 위치 계산

                // 맵 요소 타입에 따라 오브젝트 생성 및 배치
                switch (map[x, y])
                {
                    case TileType.Wall:
                        Instantiate(wallPrefab, position, Quaternion.identity); // 벽 생성
                        break;
                    case TileType.Road:
                        Instantiate(roadPrefab, position, Quaternion.identity); // 길 생성
                        break;
                    case TileType.Item:
                        Instantiate(itemPrefab, position, Quaternion.identity); // 아이템 생성
                        break;
                    case TileType.Trap:
                        Instantiate(trapPrefab, position, Quaternion.identity); // 함정 생성
                        break;
                }
            }
        }
    }
}
