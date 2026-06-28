using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager Instance { get; private set; }

    private string saveFilePath;
    private HighScoreRegistry registry = new HighScoreRegistry();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            // Places the file perfectly inside your game directory 
            saveFilePath = Path.Combine(Application.dataPath, "highscores.json");
            LoadScores();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterNewScore(string name, float time)
    {
        string arcadeName = name.ToUpper();
        if (arcadeName.Length > 3) arcadeName = arcadeName.Substring(0, 3);

        registry.scores.Add(new HighScoreEntry(arcadeName, time));
        
        // Sort from fastest (lowest float) to slowest
        registry.scores.Sort((x, y) => x.raceTime.CompareTo(y.raceTime));

        // Keep it to an old-school Top 10 leaderboard
        if (registry.scores.Count > 10)
        {
            registry.scores.RemoveRange(10, registry.scores.Count - 10);
        }

        SaveScores();
    }

    public void DeleteEntry(int index)
    {
        if (index >= 0 && index < registry.scores.Count)
        {
            registry.scores.RemoveAt(index);
            SaveScores();
        }
    }

    public void ResetAllScores()
    {
        registry.scores.Clear();
        SaveScores();
    }

    public List<HighScoreEntry> GetTopScores() => registry.scores;

    private void SaveScores()
    {
        string json = JsonUtility.ToJson(registry, true);
        File.WriteAllText(saveFilePath, json);
    }

    private void LoadScores()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            registry = JsonUtility.FromJson<HighScoreRegistry>(json);
        }
        else
        {
            registry = new HighScoreRegistry();
        }
    }
}