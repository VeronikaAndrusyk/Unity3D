using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;

[Serializable]
public class GameData
{
    public int lives = 3;
    public int coinsCollected = 0;
    public int collisions = 0;
    public float levelTime = 0f;
    public List<int> highScores = new List<int>();
    public float maxTime = 60f;
}

[Serializable]
public class GameHistory
{
    public List<GameData> gameSessions = new List<GameData>();
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameData data = new GameData();
    private string savePath;
    private GameHistory history = new GameHistory(); // ��� ���������� ��� ����
    public event Action OnGameOver;
    private float timerInterval = 1f; // �������� ��������� ������� � ������
    private float nextTime = 0f;
    private bool gameEnded = false; // ����, ��� �������� ���������� ���������� ���

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            savePath = Path.Combine(Application.persistentDataPath, "gameHistory.json");
            LoadGame();

            // ������� ��������� ��� ����� ���� ���
            data.lives = 2;
            data.levelTime = 0f;
            data.coinsCollected = 0;
            data.collisions = 0;
            SaveGame();

            Debug.Log("GameManager ��������");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Time.timeScale = 1f; // ���������, �� ��� �� ��������� �� ����� ���
    }

    void Update()
    {
        if (gameEnded) return; // ����������, �� ��� ��� ����������

        data.levelTime += Time.deltaTime;

        if (Time.time >= nextTime)
        {
            Debug.Log($"��� ����: {data.levelTime:F1} ������");
            nextTime = Time.time + timerInterval;
        }

        if (data.levelTime >= data.maxTime && !gameEnded)
        {
            TriggerGameOver();
        }
    }

    public void TriggerGameOver()
    {
        if (gameEnded) return; // ������������, �� ��� ����������� ���� ���� ���

        gameEnded = true;
        Debug.Log("��� ��ʲ�����!");

        // ���� ����� �� ����� ��������� � �������
        Debug.Log("��� ��������� �: " + savePath);

        // ������ �������� ��������� � ������ ����
        history.gameSessions.Add(data);
        SaveGame();

        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        if (player != null)
        {
            player.StopPlayer();
        }
        Time.timeScale = 0f;
        OnGameOver?.Invoke();
    }

    public void TriggerLevelComplete()
    {
        if (gameEnded) return; // ���� ��� ��� ��������, �� ������ �����

        gameEnded = true;
        Debug.Log("в���� ���������!");
        Time.timeScale = 0f;
        OnGameOver?.Invoke();
    }

    public void SaveGame()
    {
        string json = JsonUtility.ToJson(history, true); // �������� ��� ������ ����
        File.WriteAllText(savePath, json);
    }

    public void LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            history = JsonUtility.FromJson<GameHistory>(json);
        }
    }

    public void AddHighScore(int newScore)
    {
        data.highScores.Add(newScore);
        data.highScores.Sort((a, b) => b.CompareTo(a)); // ������� �� ���������
        if (data.highScores.Count > 10) // ��������� �� 10 ������
        {
            data.highScores.RemoveAt(data.highScores.Count - 1);
        }
        SaveGame(); // �������� ������� ���
    }

    public void DisplayHighScores()
    {
        Debug.Log("��� 10 �������:");
        for (int i = 0; i < data.highScores.Count; i++)
        {
            Debug.Log($"{i + 1}. {data.highScores[i]}");
        }
    }


    public void LoseLife()
    {
        if (gameEnded) return;

        data.lives--;
        Debug.Log("����� ����������: " + data.lives);

        if (data.lives <= 0)
        {
            TriggerGameOver();
        }
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
