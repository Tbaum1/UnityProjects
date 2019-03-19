using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //enables unity to show the variables in the inspector
public class TurretBlueprint {

    //variables for the regular turret stats and GameObject
    public GameObject LevelOneprefab;
    public int LevelOneCost;
    public int sellAmountLevelOne;

    //variables for the upgraded turret stats and GameObject
    public GameObject levelTwoPrefab;
    public int LevelTwoCost;
    public int sellAmountLevelTwo;

    public GameObject levelThreePrefab;
    public int LevelThreeCost;
    public int sellAmountLevelThree;

    public GameObject levelFourPrefab;
    public int LevelFourCost;
    public int sellAmountLevelFour;
      

}
