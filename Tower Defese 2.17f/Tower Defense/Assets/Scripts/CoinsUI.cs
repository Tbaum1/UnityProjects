using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsUI : MonoBehaviour {

    public Text coinsText;

    private void Update()
    {
        coinsText.text = PlayerStats.Coins.ToString();
    }
}
