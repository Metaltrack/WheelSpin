using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] public GameObject PauseMenu;
    public bool Paused;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UnPause();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (Paused)
            {
                UnPause();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        UnPause();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        PauseMenu.SetActive(true);
        Paused = true;
    }

    public void UnPause()
    {
        Time.timeScale = 1f;
        PauseMenu.SetActive(false);
        Paused = false;
    }
}
