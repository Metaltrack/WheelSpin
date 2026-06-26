using System;
using System.Collections.Generic;

[Serializable]
public class HighScoreEntry
{
    public string playerName;
    public float raceTime;

    public HighScoreEntry(string name, float time)
    {
        playerName = name;
        raceTime = time;
    }
}

[Serializable]
public class HighScoreRegistry
{
    public List<HighScoreEntry> scores = new List<HighScoreEntry>();
}