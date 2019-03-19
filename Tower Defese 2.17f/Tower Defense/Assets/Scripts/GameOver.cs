using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    

    public string menuSceneName = "MainMenu";

    public SceneFader sceneFader;

    

    /// <summary>
    /// handles the retry button when the game is over
    /// </summary>
    public void Retry()
    {
        sceneFader.FadeTo(SceneManager.GetActiveScene().name); //restarts the active scene
    }

    /// <summary>
    /// handles the menu button when the game is over
    /// </summary>
    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName);  //launches the menu scene using the SceneFader
        Debug.Log("Go to menu");
    }

}
