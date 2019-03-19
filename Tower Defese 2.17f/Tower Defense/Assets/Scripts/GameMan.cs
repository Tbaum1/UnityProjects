using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMan : MonoBehaviour {

    public static bool GameIsOver = false;

    public GameObject gameOverUI;
    public GameObject completeLevelUI;
    

    /// <summary>
    /// checks to see if the game is over, or if the player has lost all their lives 
    /// or if the players pushed e to quit when implemented
    /// </summary>
    private void Update()
    {        
        if (GameIsOver)
            return;

        //if (Input.GetKeyDown("e"))
        //{
        //    EndGame();
        //}

        if (PlayerStats.Lives <= 0)
        {
            EndGame();
        }
    }

    /// <summary>
    /// handles when the game ends
    /// </summary>
    void EndGame()
    {
        GameIsOver = true;
        Debug.Log("Game Over!");
        gameOverUI.SetActive(true);
    }

    /// <summary>
    /// handles when player beats a level
    /// </summary>
    public void WinLevel()
    {
        GameIsOver = true;
        completeLevelUI.SetActive(true);
    }
}
