using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public string levelToLoad = "Level_01";

    public SceneFader sceneFader;  //gets access to the sceneFader component

    /// <summary>
    /// loads the starting scene/level
    /// </summary>
    public void Play()
    {
        Debug.Log("Playing");
        sceneFader.FadeTo(levelToLoad);  //sets the fader prefab to use then loads the level 
    }

    /// <summary>
    /// exits the game
    /// </summary>
    public void Quit()
    {
        Debug.Log("Quiting");
        Application.Quit();
    }

}
