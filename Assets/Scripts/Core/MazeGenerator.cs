using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ListExtensions
{
    private static System.Random rng = new System.Random();

    public static void Shuffle<T>(this List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }
    }
}

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] private int width = 21;  // 가로 (홀수)
    [SerializeField] private int height = 21; // 세로 (홀수)
    [SerializeField] private float cellSize = 12f; // 셀 크기
    [SerializeField] private float cellHeight = 13f; // 층 높이
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject floorPrefab;
    [SerializeField] private GameObject stairPrefab; // 계단 프리팹
    [SerializeField] private GameObject prison;
    [SerializeField] private Transform mazeParent;

    private int[,,] maze; // 0: 벽, 1: 길, 2: 계단

    private void Start()
    {
        GenerateMaze();
        InstantiateMaze();
    }

    private void GenerateMaze()
    {
        maze = new int[width, height, 2]; // 3차원 배열로 변경

        // 미로 초기화: 모든 칸을 벽으로 설정 (각 층별로)
        for (int z = 0; z < 2; z++)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    maze[x, y, z] = 0; // 기본적으로 벽
                }
            }
        }

        // 시작 지점 설정 (홀수 좌표에서 시작, 각 층별로)
        GenerateMazeDFS(1, 1, 0);
        GenerateMazeDFS(1, 1, 1);

        // 입구와 출구 만들기 (각 층별로)
        maze[1, 0, 0] = 1; // 1층 입구
        maze[width - 2, height - 1, 0] = 1; // 1층 출구
        maze[1, 0, 1] = 1; // 2층 입구
        maze[width - 2, height - 1, 1] = 1; // 2층 출구

        // 층 연결 (계단)
        int stairX = width / 2;
        int stairY = height / 2;
        maze[stairX, stairY, 0] = 2; // 1층 계단 위치
        maze[stairX, stairY, 1] = 2; // 2층 계단 위치
    }

    private void GenerateMazeDFS(int x, int y, int z)
    {
        maze[x, y, z] = 1; // 현재 위치를 길로 설정

        int[] dx = { 0, 0, 2, -2 };
        int[] dy = { 2, -2, 0, 0 };
        List<int> directions = new List<int> { 0, 1, 2, 3 };
        directions.Shuffle();

        foreach (int dir in directions)
        {
            int nx = x + dx[dir];
            int ny = y + dy[dir];

            if (nx > 0 && ny > 0 && nx < width - 1 && ny < height - 1 && maze[nx, ny, z] == 0)
            {
                // 벽을 부수고 길을 만듦
                maze[x + dx[dir] / 2, y + dy[dir] / 2, z] = 1;
                GenerateMazeDFS(nx, ny, z);
            }
        }
    }

    private void InstantiateMaze()
    {
        List<Vector3> availablePositions = new List<Vector3>();
        for (int z = 0; z < 2; z++) // 2층
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Vector3 position = new Vector3(x * cellSize, z * cellHeight, y * cellSize);
                    if (maze[x, y, z] == 1) // 길(바닥)
                    {
                        Instantiate(floorPrefab, position, Quaternion.identity, mazeParent);
                        availablePositions.Add(position);
                    }
                    else if (maze[x, y, z] == 0) // 벽
                    {
                        Instantiate(wallPrefab, position + Vector3.up * 0.5f, Quaternion.identity, mazeParent);
                    }
                    else if (maze[x, y, z] == 2 && z == 0) // 계단
                    {
                        Instantiate(stairPrefab, position, Quaternion.identity, mazeParent);
                    }
                    //Instantiate(prison, position, Quaternion.identity, mazeParent);
                }
            }
        }

        if (availablePositions.Count > 0)
        {
            int randomIndex = Random.Range(0, availablePositions.Count);
            Instantiate(prison, availablePositions[randomIndex], Quaternion.identity, mazeParent);
        }
    }
}