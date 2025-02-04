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
    [SerializeField] private int width = 21;  // ���� (Ȧ��)
    [SerializeField] private int height = 21; // ���� (Ȧ��)
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject floorPrefab;
    [SerializeField] private Transform mazeParent;

    private int[,] maze; // 0: ��, 1: ��

    private void Start()
    {
        GenerateMaze();
        InstantiateMaze();
    }

    private void GenerateMaze()
    {
        maze = new int[width, height];

        // �̷� �ʱ�ȭ: ��� ĭ�� ������ ����
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                maze[x, y] = 0; // �⺻������ ��
            }
        }

        // ���� ���� ���� (Ȧ�� ��ǥ���� ����)
        GenerateMazeDFS(1, 1);

        // �Ա��� �ⱸ �����
        maze[1, 0] = 1; // �Ա�
        maze[width - 2, height - 1] = 1; // �ⱸ
    }

    private void GenerateMazeDFS(int x, int y)
    {
        maze[x, y] = 1; // ���� ��ġ�� ��� ����

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
                // ���� �μ��� ���� ����
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
                if (maze[x, y] == 1) // ��(�ٴ�)
                {
                    Instantiate(floorPrefab, position, Quaternion.identity, mazeParent);
                }
                else // ��
                {
                    Instantiate(wallPrefab, position + Vector3.up * 0.5f, Quaternion.identity, mazeParent);
                }
            }
        }
    }
}
