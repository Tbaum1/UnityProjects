using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour {

    public SceneFader fader;

    public Button[] levelButtons;  //initialize an array of buttons to hold the level buttons in the select menu

    private void Start()
    {
        //PlayerPrefs is a built in Unity game/program storage used to store basic valuse (int, string, float)        
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);  //this gets from PlayerPrefs a default levelReached at level 1 for each new user


        //iterates through the levelButtons[] array and making setting the interactable to false
        for (int i = 0; i < levelButtons.Length; i++)
        {
            //
            if (i + 1 > levelReached)
            {
                levelButtons[i].interactable = false;
            }            
        }
    }

    /// <summary>
    /// sets the level to load using the SceneFader fader FadeTo() function
    /// </summary>
    /// <param name="levelName"></param>
    public void Select(string levelName)
    {
        fader.FadeTo(levelName);
    }

}
