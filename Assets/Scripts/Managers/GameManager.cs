using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;

    [Header("Settings")]
    public bool friendlyFire;

    private void Awake()
    {
        instance = this;

        player = FindObjectOfType<Player>();
    }

    /*public void GameStart()
    {
        LevelGenerator.instance.InitializeGeneration();
    }*/

    public void RestartScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    public void GameOver()
    {
        UI.instance.ShowGameOverUI();
    }
}
