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
    private GameHistory history = new GameHistory(); // Для збереження всіх ігор
    public event Action OnGameOver;
    private float timerInterval = 1f; // Інтервал оновлення таймера в консолі
    private float nextTime = 0f;
    private bool gameEnded = false; // Флаг, щоб уникнути повторного завершення гри

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            savePath = Path.Combine(Application.persistentDataPath, "gameHistory.json");
            LoadGame();

            // Скидаємо параметри при старті нової гри
            data.lives = 2;
            data.levelTime = 0f;
            data.coinsCollected = 0;
            data.collisions = 0;
            SaveGame();

            Debug.Log("GameManager запущено");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Time.timeScale = 1f; // Гарантуємо, що час не зупинений на старті гри
    }

    void Update()
    {
        if (gameEnded) return; // Перевіряємо, чи гра вже закінчилась

        data.levelTime += Time.deltaTime;

        if (Time.time >= nextTime)
        {
            Debug.Log($"Час рівня: {data.levelTime:F1} секунд");
            nextTime = Time.time + timerInterval;
        }

        if (data.levelTime >= data.maxTime && !gameEnded)
        {
            TriggerGameOver();
        }
    }

    public void TriggerGameOver()
    {
        if (gameEnded) return; // Переконуємось, що гра завершується лише один раз

        gameEnded = true;
        Debug.Log("ГРА ЗАКІНЧЕНА!");

        // Вивід шляху до файлу збережень у консоль
        Debug.Log("Дані збережено у: " + savePath);

        // Додаємо поточний результат у список ігор
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
        if (gameEnded) return; // Якщо гра вже закінчена, не робимо нічого

        gameEnded = true;
        Debug.Log("РІВЕНЬ ЗАВЕРШЕНО!");
        Time.timeScale = 0f;
        OnGameOver?.Invoke();
    }

    public void SaveGame()
    {
        string json = JsonUtility.ToJson(history, true); // Зберігаємо всю історію ігор
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
        data.highScores.Sort((a, b) => b.CompareTo(a)); // Сортуємо за спаданням
        if (data.highScores.Count > 10) // Обмеження до 10 записів
        {
            data.highScores.RemoveAt(data.highScores.Count - 1);
        }
        SaveGame(); // Зберігаємо оновлені дані
    }

    public void DisplayHighScores()
    {
        Debug.Log("Топ 10 рекордів:");
        for (int i = 0; i < data.highScores.Count; i++)
        {
            Debug.Log($"{i + 1}. {data.highScores[i]}");
        }
    }


    public void LoseLife()
    {
        if (gameEnded) return;

        data.lives--;
        Debug.Log("Життів залишилось: " + data.lives);

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
