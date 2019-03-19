using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {

    public static BuildManager instance;  //stores refence to BuildManager script, singleton pattern(means only ever be 1 instance of the BuildManager)
        
    private TurretBlueprint turretToBuild;

    public GameObject BuildEffect;
    public GameObject UpgradeBuildEffect;
    public GameObject sellEffect;
    public NodeUI nodeUI; 

    private Node selectedNode;
        

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one BuildManager in scene");  //if there is already a BuildManager then do nothing
            return;
        }
        instance = this;  //sets this builManager to the current
        
    }
    
    public bool CanBuild { get { return turretToBuild != null;  } }  //this is a property, only allows user to get from it and not set it, returns true if can build false if not
    public bool HasLevelOneCoins { get { return PlayerStats.Coins >= turretToBuild.LevelOneCost; } }  //if player has enough coins to build returns true otherwise returns false 
    public bool HasLevelTwoCoins { get { return PlayerStats.Coins >= turretToBuild.LevelTwoCost; } }  //if player has enough coins to build returns true otherwise returns false 
    public bool HasLevelThreeCoins { get { return PlayerStats.Coins >= turretToBuild.LevelThreeCost; } }  //if player has enough coins to build returns true otherwise returns false 
    public bool HasLevelFourCoins { get { return PlayerStats.Coins >= turretToBuild.LevelFourCost; } }  //if player has enough coins to build returns true otherwise returns false 

    /// <summary>
    /// sets the turret that needs to be built
    /// </summary>
    /// <param name="turret"></param>
    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;  //sets turrretToBuild to the turret passed in
        //selectedNode = null;

        DeselectNode();  //calls function DeselectNode
    }
    
    /// <summary>
    /// gets the node that the turret is on that user chose to upgrade or sell
    /// </summary>
    /// <param name="node"></param>
    public void SelectNode(Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();  //calls function DeselectNode
            return;
        }

        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);  //calls SetTarget funtion and passes in the node from NodeUI script, that the user chose to upgrade/sell the turret at
    }

    /// <summary>
    /// hides the upgrade/sell ui and sets selectedNode equal null
    /// </summary>
    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }
}
