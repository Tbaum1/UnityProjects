using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WavesSurvived : MonoBehaviour {

    public Text wavesText;
    
    /// <summary>
    /// when the player beats a level onEnabled is called
    /// </summary>
    private void OnEnable()
    {
        StartCoroutine(AnimateText());       
    }


    /// <summary>
    ///displays the number of waves completed by having the text change
    ///until it gets to the level of the player has completed
    /// </summary>
    /// <returns></returns>
    IEnumerator AnimateText()
    {
        wavesText.text = "0";
        int wave = 0;  //holds the wave on

        yield return new WaitForSeconds(.7f);

        while (wave < PlayerStats.Waves)
        {

            wave++;
            wavesText.text = (wave - 1).ToString();
            if(wave == 10 && !GameMan.GameIsOver)
            {
                wavesText.text = wave.ToString();
            }
            yield return new WaitForSeconds(.05f);
        }
    }
}
