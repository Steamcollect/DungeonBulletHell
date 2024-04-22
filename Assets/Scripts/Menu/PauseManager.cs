using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public Animator settingsAnimator;
    bool isSettingsOpen;

    GameStateManager gameStateManager;

    private void Awake()
    {
        gameStateManager = FindFirstObjectByType<GameStateManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) PauseButton();
    }

    public void BackToMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void PauseButton()
    {
        if (isSettingsOpen)
        {
            StartCoroutine(SetGameplay());
            isSettingsOpen = false;
        }
        else
        {
            gameStateManager.PauseGameState();
            isSettingsOpen = true;
        }
        settingsAnimator.SetBool("IsOpen", isSettingsOpen);
    }

    IEnumerator SetGameplay()
    {
        yield return new WaitForSeconds(.5f);

        Time.timeScale = 0;
        GameStateManager.instance.ResumeGameState();

        while (Time.timeScale < 1)
        {
            Time.timeScale += 1 * Time.deltaTime;
            yield return null;
        }

        Time.timeScale = 1;
    }
}