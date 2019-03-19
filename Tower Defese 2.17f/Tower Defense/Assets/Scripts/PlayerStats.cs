using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public static int Coins;
    public int startCoins = 350;

    public static int Waves;
    public static int maxLevel = 1;

    public static int Lives;
    public int startLives = 20;


    /// <summary>
    ///sets Coins equal to startCoins at the start of game
    ///sets Lives equal to the startLives at the start of game
    ///sets the wave number to 0
    ///sets the maxLevel the player has reached which is stored using PlayerPrefs "maxLevel"
    /// </summary>
    private void Start()
    {
        Coins = startCoins;  
        Lives = startLives;  
        Waves = 0;  
        maxLevel = PlayerPrefs.GetInt("maxLevel", maxLevel);
    }

}
