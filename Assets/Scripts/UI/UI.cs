using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public static UI instance;

    public UI_InGame inGameUI { get; private set; }
    public UI_GameOver gameOverUI { get; private set; } 
    public GameObject pauseUI;

    [SerializeField] private GameObject[] UIElements;

    private void Awake()
    {
        instance = this;
        inGameUI = GetComponentInChildren<UI_InGame>(true);
        gameOverUI = GetComponentInChildren<UI_GameOver>(true);
    }

    private void Start()
    {
        AssignInputsUI();
        ControlsManager.instance.SwitchToUIControls();
    }

    public void SwitchTo(GameObject uiToSwitchOn)
    {
        foreach(GameObject go in UIElements)
        {
            go.SetActive(false);
        }

        uiToSwitchOn.SetActive(true);
    }

    public void StartTheGame()
    {
        SwitchTo(inGameUI.gameObject);
        ControlsManager.instance.SwitchToCharacterControls();
        Time.timeScale = 1;
    }

    public void QuitTheGame() => Application.Quit();

    public void StartLevelGeneration() => LevelGenerator.instance.InitializeGeneration();

    public void RestartTheGame() => GameManager.instance.RestartScene(); 

    public void PauseSwitch()
    {
        bool gamePaused = pauseUI.activeSelf;

        if (gamePaused)
        {
            SwitchTo(inGameUI.gameObject);
            ControlsManager.instance.SwitchToCharacterControls();
            Time.timeScale = 1;
        }
        else
        {
            SwitchTo(pauseUI);
            ControlsManager.instance.SwitchToUIControls();
            Time.timeScale = 0;
        }
    }

    public void ShowGameOverUI()
    {
        SwitchTo(gameOverUI.gameObject);
    }

    private void AssignInputsUI()
    {
        PlayerControls controls = GameManager.instance.player.controls;

        controls.UI.UIPause.performed += ctx => PauseSwitch();
    }

    [ContextMenu ("Assign Audio To Buttons")]
    public void AssignAudioListenersToButtons()
    {
        UI_Button[] buttons = FindObjectsOfType<UI_Button> (true);

        foreach (var button in buttons)
        {
            button.AssignAudioSource();
        }
    }
}
