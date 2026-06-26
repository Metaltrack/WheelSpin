using UnityEngine;
using System.IO;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class LeaderboardSceneManager : MonoBehaviour
{
    [Header("UI Text Display")]
    public TMP_Text leaderboardText;        

    [Header("Arcade Name Entry Panel")]
    public GameObject entryPanel;          
    public TMP_InputField nameInputField;   

    private float timeToRegister = 0f;
    private string tempFilePath;
    void Awake()
    {
        // MUST MATCH THE RESET PATH EXACTLY
        tempFilePath = Path.Combine(Application.dataPath, "pending_run.txt");
    }

    void Start()
    {
        tempFilePath = Path.Combine(Application.dataPath, "pending_run.txt");

        // Render the current leaderboard layout on boot
        DisplayLeaderboard();

        // AUTOMATIC CHECK: Did the player just finish a race?
        if (File.Exists(tempFilePath))
        {
            TryOpenEntryFromTempFile();
        }
        else
        {
            if (entryPanel != null) entryPanel.SetActive(false);
        }
    }

    // PUBLIC FUNCTION: Link this to your "New" Button's OnClick event!
    public void OnNewButtonClick()
    {
        if (File.Exists(tempFilePath))
        {
            TryOpenEntryFromTempFile();
        }
        else
        {
            Debug.Log("No pending race times found to register!");
            // Optional: You could show a quick message saying "Go race first!"
        }
    }

    private void TryOpenEntryFromTempFile()
    {
        if (File.Exists(tempFilePath))
        {
            string fileContent = File.ReadAllText(tempFilePath);
            if (float.TryParse(fileContent, out timeToRegister))
            {
                if (entryPanel != null) entryPanel.SetActive(true);
                if (nameInputField != null) nameInputField.text = ""; // Clear old inputs
            }
        }
    }

    // Call this via your "SUBMIT" Button inside the name entry panel!
    public void SubmitScore()
    {
        string enteredName = "AAA"; // Default fallback

        if (nameInputField != null && !string.IsNullOrEmpty(nameInputField.text))
        {
            // Trim() strips out invisible spaces or newline characters that break JSON compilers
            enteredName = nameInputField.text.Trim();
        }

        // 1. Send it directly to the manager using your chosen path setup
        HighScoreManager.Instance.RegisterNewScore(enteredName, timeToRegister);

        // 2. Wipe the temp text file so it doesn't pop up again on refresh
        if (File.Exists(tempFilePath))
        {
            File.Delete(tempFilePath);
        }

        // 3. Reset state, turn off the window panel, and force the visual text update
        timeToRegister = 0f;
        if (entryPanel != null) entryPanel.SetActive(false);
        
        DisplayLeaderboard();
    }

    public void DisplayLeaderboard()
    {
        if (leaderboardText == null) return;

        List<HighScoreEntry> topScores = HighScoreManager.Instance.GetTopScores();

        string leaderboardString = "RANK   NAME      TIME\n";
        leaderboardString += "---------------------\n";

        for (int i = 0; i < topScores.Count; i++)
        {
            int rank = i + 1;
            string pName = topScores[i].playerName;
            float pTime = topScores[i].raceTime;

            float minutes = Mathf.FloorToInt(pTime / 60);
            float seconds = Mathf.FloorToInt(pTime % 60);
            float milliseconds = (pTime % 1) * 1000;

            string formattedTime = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
            leaderboardString += string.Format("{0:2}.    {1,-6}   {2}\n", rank, pName, formattedTime);
        }

        leaderboardText.text = leaderboardString;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // PUBLIC FUNCTION: Link this to your bottom "Delete" Button's OnClick event!
    public void OnDeleteButtonClick()
    {
        // Deletes the number #1 top spot entry from the file list database
        // (You can change 0 to another index number if you track selections later)
        HighScoreManager.Instance.DeleteEntry(0);

        Debug.Log("Top high score entry deleted from registry file.");

        // FORCE REFRESH: Re-render the UI layout text immediately
        DisplayLeaderboard();
    }

    // PUBLIC FUNCTION: Link this to your bottom "Reset" Button's OnClick event!
    public void OnResetButtonClick()
    {
        // Completely wipes out the highscores.json data registry array
        HighScoreManager.Instance.ResetAllScores();

        Debug.Log("Entire high score database wiped clean!");

        // FORCE REFRESH: Instantly updates the visual panel to be blank/empty
        DisplayLeaderboard();
    }
}