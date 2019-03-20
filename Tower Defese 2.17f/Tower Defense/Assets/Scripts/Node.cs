using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Node : MonoBehaviour {

    public Color hoverColor;
    public Color notEnoughCoinsColor;

    public Text turretShopInfo;
    public Text turretName;

    public Vector3 positionOffset;

    private WaveSpawn waveSpawn;
    private Color startColor;
    private int sellAmount;
    public int sellAmountUpgrade;

    [HideInInspector]
    public GameObject turret;  //holds current turret selected from BuildManager
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;
    public int turretLevel;

    private Renderer rend;
        
    BuildManager buildManager;

    private void Start()
    {
        rend = GetComponent<Renderer>(); //instantiates the renderer component
        startColor = rend.material.color;  //sets the starting color of the node object
        buildManager = BuildManager.instance; //set buildManager to the BuildManger script
        turretLevel = 0;
        //waveSpawn.turretShopInfo.text = " ";
    }

    /// <summary>
    /// gets the position of the node to build turret on
    /// </summary>
    /// <returns></returns>
    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    /// <summary>
    /// handles the building of the turret and playing the build effect for the turret and takes away the coins for the specified turret
    /// </summary>
    /// <param name="blueprint"></param>
    void BuildTurret(TurretBlueprint blueprint)
    {
        DisableText();
        //checks to see if player has enough coins to buy the turret chosen
        if (PlayerStats.Coins < blueprint.LevelOneCost)
        {
            Debug.Log("Not enough coins to build");
            turretShopInfo.enabled = true;
            turretName.enabled = true;
            turretName.text = blueprint.LevelOneprefab.name;
            turretShopInfo.text = "Not Enough Coins To Buy";
            Invoke("DisableText", 5f);
            return;
        }

        PlayerStats.Coins -= blueprint.LevelOneCost;  //takes away the coins it cost to build the specific turret

        GameObject _turret = (GameObject)Instantiate(blueprint.LevelOneprefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;  //lets the node know what turret is on it 
        
        turretBlueprint = blueprint;
        GameObject effect = (GameObject)Instantiate(buildManager.BuildEffect, GetBuildPosition(), Quaternion.identity);  //plays the build effect
        Destroy(effect, 3f);

        turretLevel = 1;  //sets the target node turretLevel to 1
        turretShopInfo.enabled = true;
        turretName.enabled = true;
        turretName.text = blueprint.LevelOneprefab.name;
        turretShopInfo.text = " Purchased";
        Invoke("DisableText", 5f);
        //display how many coins are left
        Debug.Log("Turret built cost: " + blueprint.LevelOneCost + ", Coins left: " + PlayerStats.Coins);
    }

    /// <summary>
    /// disables the Text displayed in the shop informing player of turret purchases or not enough coins
    /// </summary>
    private void DisableText()
    {
        turretName.enabled = false;
        turretShopInfo.enabled = false;
    }

    /// <summary>
    ///checks to see which turretPrefab has been chosen then it plays the correct particle effect according
    /// </summary>
    /// <param name="b"></param>
    //private void GetBuildEffect(TurretBlueprint b)
    //{
    //    string blueprintName = b.prefab.name;

    //    if (blueprintName == "MissileTurret")
    //    {
    //        GameObject effect = (GameObject)Instantiate(buildManager.MissileBuildEffect, GetBuildPosition(), Quaternion.identity);
    //        Destroy(effect, 3f);
    //    }
    //    else if (blueprintName == "AutocannonTurret")
    //    {
    //        GameObject effect = (GameObject)Instantiate(buildManager.AutocannonBuildEffect, GetBuildPosition(), Quaternion.identity);
    //        Destroy(effect, 3f);
    //    }
    //    else if (blueprintName == "LaserBeamTurret")
    //    {
    //        GameObject effect = (GameObject)Instantiate(buildManager.LaserBeamBuildEffect, GetBuildPosition(), Quaternion.identity);
    //        Destroy(effect, 3f);
    //    }
    //    else if (blueprintName == "AutocannonTurretUpgraded" || blueprintName == "LaserBeamTurretUpgraded" || blueprintName == "MissileTurretUpgraded")
    //    {
    //        GameObject effect = (GameObject)Instantiate(buildManager.UpgradeBuildEffect, GetBuildPosition(), Quaternion.identity);
    //        Destroy(effect, 3f);
    //    }
    //}

    /// <summary>
    /// when user presses left mouse button down on the node the BuildManager will load
    /// </summary>
    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())  //if the pointer is over a eventsystem object if so exit the function
            return;                                         //so when in the UI the nodes in the background do not highlight

        if (turret != null)
        {
            buildManager.SelectNode(this);
            Debug.Log("Can't build, already has turret");
            return;
        }

        if (!buildManager.CanBuild)  //if can build turret then do nothing      
            return;

        BuildTurret(buildManager.GetTurretToBuild());  //passes the node that the player chose to build turret on        
    }

    /// <summary>
    /// when the mouse hovers over a node it changes the color
    /// </summary>
    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())  //if the pointer is over a eventsystem object if so exit the function
            return;                                         //so when in the UI the nodes in the background do not highlight

        if (!buildManager.CanBuild)  //only lets the hover color change to happen if user has selected a turret to build
            return;

        
        if (buildManager.HasLevelOneCoins || buildManager.HasLevelTwoCoins || buildManager.HasLevelThreeCoins || buildManager.HasLevelFourCoins)  //if player has enough coins to purchase turret then it highlights the node according
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughCoinsColor;  //if player does not have enough coins to purchase turret then node highlights red
        }
    }

    /// <summary>
    /// //sets color of node back to starting color when the mouse no longer is hovering over
    /// </summary>
    private void OnMouseExit()
    {
        rend.material.color = startColor;  
    }

    public void UpgradeTurret()
    {
        turretLevel++;  //adds 1 to the target node turretLevel
        
        //Destroy(turret);  //Get rid of the old turret
        if (turretLevel == 2)
        {
            if (PlayerStats.Coins < turretBlueprint.LevelTwoCost)
            {
                Debug.Log("Not enough money to upgrade that!");
                return;
            }
            Destroy(turret);
            PlayerStats.Coins -= turretBlueprint.LevelTwoCost;
            GameObject _turret = (GameObject)Instantiate(turretBlueprint.levelTwoPrefab, GetBuildPosition(), Quaternion.identity);  //builds a new turret at the target node            
            Debug.Log("Turret upgrade cost: " + turretBlueprint.LevelTwoCost + ", Coins left: " + PlayerStats.Coins);  //display how many coins are left
            turret = _turret;  //replaces the turret prefab of the main turret variable            
        }
        if (turretLevel == 3)
        {
            if (PlayerStats.Coins < turretBlueprint.LevelThreeCost)
            {
                Debug.Log("Not enough money to upgrade that!");
                return;
            }
            Destroy(turret);
            PlayerStats.Coins -= turretBlueprint.LevelThreeCost;
            GameObject _turret = (GameObject)Instantiate(turretBlueprint.levelThreePrefab, GetBuildPosition(), Quaternion.identity);  //builds a new turret at the target node
            Debug.Log("Turret upgrade cost: " + turretBlueprint.LevelThreeCost + ", Coins left: " + PlayerStats.Coins);  //display how many coins are left
            turret = _turret;  //replaces the turret prefab of the main turret variable
        }
        if (turretLevel == 4)
        {
            if (PlayerStats.Coins < turretBlueprint.LevelFourCost)
            {
                Debug.Log("Not enough money to upgrade that!");
                return;
            }
            isUpgraded = true;
            Destroy(turret);
            PlayerStats.Coins -= turretBlueprint.LevelFourCost;
            GameObject _turret = (GameObject)Instantiate(turretBlueprint.levelFourPrefab, GetBuildPosition(), Quaternion.identity);  //builds a new turret at the target node
            Debug.Log("Turret upgrade cost: " + turretBlueprint.LevelFourCost + ", Coins left: " + PlayerStats.Coins);  //display how many coins are left            
            turret = _turret;  //replaces the turret prefab of the main turret variable
        }

        

        GameObject effect = (GameObject)Instantiate(buildManager.UpgradeBuildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
    }   

    /// <summary>
    /// handles selling the turret and adding coins back to the total
    /// </summary>
    public void SellTurret(Node target)
    {
        if (target.turretLevel == 1)
        {
            PlayerStats.Coins += turretBlueprint.sellAmountLevelOne;
        }
        else if (target.turretLevel == 2)
        { 
            //checks to see if the turret has been upgraded or not then sells it for the correct amount
            PlayerStats.Coins += turretBlueprint.sellAmountLevelTwo;            
        }else if (target.turretLevel == 3)
        {
            PlayerStats.Coins += turretBlueprint.sellAmountLevelThree;
        }else if (target.turretLevel == 4)
        {
            PlayerStats.Coins += turretBlueprint.sellAmountLevelFour;            
        }

        target.turretLevel = 0;  //resets the turretLevel back to zero

        //spawn cool effect
        GameObject sellEffect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);  //plays the sell effect at the node of the turret being sold
        Destroy(sellEffect, 5f);  //destroys sellEffect after 5s        

        Destroy(turret);  //destroys the turret prefab
        turretBlueprint = null;  //sets turretBlueprint for the node back to null    
        Debug.Log("Turret sold");    
    }
    
}
