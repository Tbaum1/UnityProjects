using UnityEngine;

/// <summary>
/// stores values about each wave, ie which enemy to spawn and count and how fast
/// </summary>

[System.Serializable]  //to display it in the inspector
public class Wave {


    /// <summary>
    /// instantiates different vaiables that will be set in the inspector
    /// </summary>
    public GameObject enemySimplePrefab;
    public GameObject enemyFastPrefab;
    public GameObject enemyToughPrefab;
    public GameObject enemyBossPrefab;   

    public int enemySimpleCount;
    public int enemyFastCount;
    public int enemyToughCount;
    public int enemyBossCount;    

    public float enemySimpleRate;
    public float enemyFastRate;
    public float enemyToughRate;
    public float enemyBossRate;
	
}
