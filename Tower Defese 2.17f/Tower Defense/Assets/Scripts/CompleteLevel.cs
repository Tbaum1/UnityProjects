using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteLevel : MonoBehaviour {

    public string meneSceneName = "MainMenu";
    public string nextLevel = "Level_02";

    public int levelToUnlock = 2;

    public SceneFader sceneFader;

    private void Start()
    {
        PlayerPrefs.SetInt("maxLevel" , levelToUnlock - 1);
    }

    public void Menu()
    {        
        sceneFader.FadeTo(meneSceneName);
    }

    public void Continue()
    {
        PlayerPrefs.SetInt("levelReached", levelToUnlock);
        Debug.Log("LEVEL WON!");
        sceneFader.FadeTo(nextLevel);
    }

}
