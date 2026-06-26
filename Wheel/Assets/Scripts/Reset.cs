using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.InputSystem;

public class Reset : MonoBehaviour
{
    public TMPro.TMP_Text timerText; 
    private float raceTimer = 0f;

    void Update()
    {
        // DEV CHEAT: Pressing the 'T' key simulates an instant win
        // if (Keyboard.current != null && Keyboard.current.tKey.wasPressedThisFrame)
        // {
        //     // Mandating the exact path inside your active project directory
        //     string tempPath = Path.Combine(Application.dataPath, "pending_run.txt");
            
        //     float mockTime = raceTimer > 0 ? raceTimer : 45.20f; 
        //     File.WriteAllText(tempPath, mockTime.ToString());

        //     Debug.Log($"[CHEAT] Win simulated! Temp file written to: {tempPath}");

        //     UnityEngine.SceneManagement.SceneManager.LoadScene("HighScoreScene");
        //     return; 
        // }

        raceTimer += Time.deltaTime;
        
        float minutes = Mathf.FloorToInt(raceTimer / 60);
        float seconds = Mathf.FloorToInt(raceTimer % 60);
        float milliseconds = (raceTimer % 1) * 1000;

        if (timerText != null)
        {
            timerText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            // Save the time to a temporary file right next to the game files
            string tempPath = Path.Combine(Application.dataPath, "pending_run.txt");
            File.WriteAllText(tempPath, raceTimer.ToString());

            Debug.Log($"Race over! Time saved to temporary storage. Loading Leaderboard...");
            SceneManager.LoadScene("HighScoreScene");
        }
    }
}   