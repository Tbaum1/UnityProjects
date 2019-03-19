using UnityEngine;
using UnityEngine.SceneManagement;

public class Paused : MonoBehaviour {

    public GameObject ui;

    public string menuSceneName = "MainMenu";

    public SceneFader sceneFader;

    private void Update()
    {
        //checks to see if the P or ESC keys were pressed for the pause menu if so then it calls the Toggle() function
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Toggle();
        }
    }

    /// <summary>
    /// toggles the Paused menu ui.SetActive state
    /// </summary>
    public void Toggle()
    {
        ui.SetActive(!ui.activeSelf);  //sets the ui.SetActive to the inverse of current state


        //if ui is in active state/activeSelf true
        if (ui.activeSelf)
        {
            Time.timeScale = 0f;  //freezing the fixed delta time for game, pauses the game            
        }
        else
        {
            Time.timeScale = 1;  //returns game fixed delta time back to normal and unpauses/freezes the game
        }
    }


    /// <summary>
    /// handles when the user clicks the retry button in the pause menu
    /// </summary>
    public void Retry()
    {
        Toggle();
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);  //
    }

    /// <summary>
    /// handles when the user clicks the menu button in the pause menu
    /// </summary>
    public void Menu()
    {
        Toggle();
        sceneFader.FadeTo(menuSceneName);  //loads the main menu
        Debug.Log("Go to menu");
    }

}
