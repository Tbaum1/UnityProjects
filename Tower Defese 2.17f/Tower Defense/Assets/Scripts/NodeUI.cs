using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour {

    public GameObject ui;

    public Image upgradeCoinImg ;
    public Image sellCoinImg;
    public Text upgradeCost;
    public Text sellAmount;
    public Button upgradeButton;

    private Node target;

    /// <summary>
    /// handles the setting of the target node
    /// </summary>
    /// <param name="_target"></param>
    public void SetTarget(Node _target)
    {
        target = _target;  //sets target node to the node the user chose

        transform.position = target.GetBuildPosition();  //sets the position of target to the center top edge of the node

        //checks to see what level the turret is currently on and sets the upgrade and sell prices accordingly
        if (target.turretLevel == 1)
        {
            upgradeCoinImg.enabled = true;  //sets the coin image.enabled in the upgrade button to true
            upgradeCost.text = target.turretBlueprint.LevelTwoCost.ToString();
            sellAmount.text = "   " + target.turretBlueprint.sellAmountLevelOne;
            Debug.Log("New shop sell: " + target.turretBlueprint.sellAmountLevelOne); 
            upgradeButton.interactable = true;
        }
        else if (target.turretLevel == 2)
        {
            //upgradeCoinImg.enabled = true;
            upgradeCost.text = target.turretBlueprint.LevelThreeCost.ToString();
            sellAmount.text = "   " + target.turretBlueprint.sellAmountLevelTwo;
            Debug.Log("New shop sell: " + target.turretBlueprint.sellAmountLevelTwo);
            upgradeButton.interactable = true;
        }
        else if (target.turretLevel == 3)
        {
            //upgradeCoinImg.enabled = true;
            upgradeCost.text = target.turretBlueprint.LevelFourCost.ToString();
            sellAmount.text = "   " + target.turretBlueprint.sellAmountLevelThree;
            Debug.Log("New shop sell: " + target.turretBlueprint.sellAmountLevelTwo);
            upgradeButton.interactable = true;
        }
        else if (target.turretLevel == 4)
        {
            upgradeCoinImg.enabled = false;  //set the upgrade button to disabled
            upgradeCost.text = "Max Level";  //sets the upgrade cost to show Max Level
            sellAmount.text = "   " + target.turretBlueprint.sellAmountLevelFour;
            Debug.Log("New shop sell: " + target.turretBlueprint.sellAmountLevelThree);
            upgradeButton.interactable = false;
        }              

        ui.SetActive(true); //when the user clicks on the turret the upgrade/sell ui displays
    }        

    /// <summary>
    ///hides the upgrade/sell ui 
    /// </summary>
    public void Hide()
    {
        ui.SetActive(false);  
    }

    /// <summary>
    /// handles the upgrade of turrets
    /// </summary>
    public void Upgrade()
    {
        target.UpgradeTurret();  //calls the upgradeTurret function from the Node script
        BuildManager.instance.DeselectNode();  //after the turret gets upgraded the menu closes and deselects the NodeUI
    }

    /// <summary>
    /// handles the selling of turrets
    /// </summary>
    public void Sell()
    {
        target.SellTurret(target);  //calls SellTurret function from Node script
        BuildManager.instance.DeselectNode();  //after the turret gets sold the menu closes and deselects the NodeUI
    }
}
