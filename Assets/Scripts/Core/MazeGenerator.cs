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
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject floorPrefab;
    [SerializeField] private Transform mazeParent;

    private int[,] maze; // 0: 벽, 1: 길

    private void Start()
    {
        GenerateMaze();
        InstantiateMaze();
    }

    private void GenerateMaze()
    {
        maze = new int[width, height];

        // 미로 초기화: 모든 칸을 벽으로 설정
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                maze[x, y] = 0; // 기본적으로 벽
            }
        }

        // 시작 지점 설정 (홀수 좌표에서 시작)
        GenerateMazeDFS(1, 1);

        // 입구와 출구 만들기
        maze[1, 0] = 1; // 입구
        maze[width - 2, height - 1] = 1; // 출구
    }

    private void GenerateMazeDFS(int x, int y)
    {
        maze[x, y] = 1; // 현재 위치를 길로 설정

        int[] dx = { 0, 0, 2, -2 };
        int[] dy = { 2, -2, 0, 0 };
        List<int> directions = new List<int> { 0, 1, 2, 3 };
        directions.Shuffle();

        foreach (int dir in directions)
        {
            int nx = x + dx[dir];
            int ny = y + dy[dir];

            if (nx > 0 && ny > 0 && nx < width - 1 && ny < height - 1 && maze[nx, ny] == 0)
            {
                // 벽을 부수고 길을 만듦
                maze[x + dx[dir] / 2, y + dy[dir] / 2] = 1;
                GenerateMazeDFS(nx, ny);
            }
        }
    }

    private void InstantiateMaze()
    {
        float cellSize = 12f;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 position = new Vector3(x * cellSize, 0, y * cellSize);
                if (maze[x, y] == 1) // 길(바닥)
                {
                    Instantiate(floorPrefab, position, Quaternion.identity, mazeParent);
                }
                else // 벽
                {
                    Instantiate(wallPrefab, position + Vector3.up * 0.5f, Quaternion.identity, mazeParent);
                }
            }
        }
    }
}
