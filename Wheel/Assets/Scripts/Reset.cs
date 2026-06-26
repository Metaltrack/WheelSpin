using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    // Removing the unused collider variable and Start/Update cleanup to save memory

    // FIXED: Unity's collision system expects this to match internally
    private void OnTriggerEnter(Collider other)
    {
        // FIXED: Using CompareTag("Car") is faster and prevents typos crashing your game
        if (other.CompareTag("Car"))
        {
            // Reloads your scene
            SceneManager.LoadScene("SampleScene");
            Debug.Log("Car hit the reset trigger! Reloading scene...");
        }
    }
}
