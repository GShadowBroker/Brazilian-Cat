using UnityEngine;
using UnityEngine.Events;

public class GameState : MonoBehaviour
{
    public static bool paused = false;

    public static bool showCredits = false;

    public GameObject pauseScreen;
    public GameObject creditsScreen;

    [System.Serializable] public class TogglePauseEvent : UnityEvent<bool> { };
    public TogglePauseEvent togglePauseEvent;

    [System.Serializable] public class ToggleCreditsEvent : UnityEvent<bool> { };
    public ToggleCreditsEvent toggleCreditsEvent;

    private void Start()
    {
        if (togglePauseEvent == null)
        {
            togglePauseEvent = new TogglePauseEvent();
        }

        if (toggleCreditsEvent == null)
        {
            toggleCreditsEvent = new ToggleCreditsEvent();
        }
    }

    public void TogglePause()
    {
        if (paused)
        {
            Time.timeScale = 1;
            paused = false;
            togglePauseEvent.Invoke(false);

            pauseScreen.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            paused = true;
            togglePauseEvent.Invoke(true);

            pauseScreen.SetActive(true);

        }
        showCredits = false;
        creditsScreen.SetActive(false);
    }

    public void ToggleCredits()
    {
        if (showCredits)
        {
            toggleCreditsEvent.Invoke(false);
            showCredits = false;
            pauseScreen.SetActive(true);
            creditsScreen.SetActive(false);
        }
        else
        {
            toggleCreditsEvent.Invoke(true);
            showCredits = true;
            pauseScreen.SetActive(false);
            creditsScreen.SetActive(true);
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quitting application...");
        Application.Quit();
        // UnityEditor.EditorApplication.isPlaying = false;
    }
}
