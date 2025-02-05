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
    [SerializeField] private float cellSize = 12f; // �� ũ��
    [SerializeField] private float cellHeight = 10f; // �� ����
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject floorPrefab;
    [SerializeField] private GameObject stairPrefab; // ��� ������
    [SerializeField] private Transform mazeParent;

    private int[,,] maze; // 0: ��, 1: ��, 2: ���

    private void Start()
    {
        GenerateMaze();
        InstantiateMaze();
    }

    private void GenerateMaze()
    {
        maze = new int[width, height, 2]; // 3���� �迭�� ����

        // �̷� �ʱ�ȭ: ��� ĭ�� ������ ���� (�� ������)
        for (int z = 0; z < 2; z++)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    maze[x, y, z] = 0; // �⺻������ ��
                }
            }
        }

        // ���� ���� ���� (Ȧ�� ��ǥ���� ����, �� ������)
        GenerateMazeDFS(1, 1, 0);
        GenerateMazeDFS(1, 1, 1);

        // �Ա��� �ⱸ ����� (�� ������)
        maze[1, 0, 0] = 1; // 1�� �Ա�
        maze[width - 2, height - 1, 0] = 1; // 1�� �ⱸ
        maze[1, 0, 1] = 1; // 2�� �Ա�
        maze[width - 2, height - 1, 1] = 1; // 2�� �ⱸ

        // �� ���� (���)
        int stairX = width / 2;
        int stairY = height / 2;
        maze[stairX, stairY, 0] = 2; // 1�� ��� ��ġ
        maze[stairX, stairY, 1] = 2; // 2�� ��� ��ġ
    }

    private void GenerateMazeDFS(int x, int y, int z)
    {
        maze[x, y, z] = 1; // ���� ��ġ�� ��� ����

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
                // ���� �μ��� ���� ����
                maze[x + dx[dir] / 2, y + dy[dir] / 2, z] = 1;
                GenerateMazeDFS(nx, ny, z);
            }
        }
    }

    private void InstantiateMaze()
    {
        for (int z = 0; z < 2; z++) // 2��
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Vector3 position = new Vector3(x * cellSize, z * cellHeight, y * cellSize);
                    if (maze[x, y, z] == 1) // ��(�ٴ�)
                    {
                        Instantiate(floorPrefab, position, Quaternion.identity, mazeParent);
                    }
                    else if (maze[x, y, z] == 0) // ��
                    {
                        Instantiate(wallPrefab, position + Vector3.up * 0.5f, Quaternion.identity, mazeParent);
                    }
                    else if (maze[x, y, z] == 2 && z == 0) // ��� 1������ ����
                    {
                        Instantiate(stairPrefab, position, Quaternion.identity, mazeParent);
                    }
                }
            }
        }
    }
}