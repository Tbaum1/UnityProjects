using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour {

    public Text livesText;

    private void Update()
    {
        livesText.text = PlayerStats.Lives + " Lives";  //sets the text for the number of lives
    }


}
