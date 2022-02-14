using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject inGameUI;
    public GameObject inventoryUI;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<Player>().OnPlayerDeath += ShowGameOverScreen;
        FindObjectOfType<Player>().OnPlayerDeath += HideInGameUI;
    }

    void ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true);
    }

    void HideInGameUI()
    {
        inGameUI.SetActive(false);
        inventoryUI.SetActive(false);
    }

    public void LoadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
