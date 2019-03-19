using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour {

    public Image image;
    public AnimationCurve curve;

    private void Start()
    {

        StartCoroutine(FadeIn());  //calls the FadeIn function using Coroutine
    
    }

    /// <summary>
    /// controls the fading in of the scene when it loads in the game
    /// </summary>
    /// <returns></returns>
    IEnumerator FadeIn()
    {
        float t = 1.2f;  //variable for the fade of the image
        Color c = image.color;

        //controls the value of the fade
        while(t > 0f)
        {
            t -= Time.deltaTime ;  //decrease value of t each fram
            float a = curve.Evaluate(t);  //sets the curve for the t time for the fading in effect to look more smooth
            c.a = a;  //sets the alpha color of the image to t 
            image.color = c;  //sets the color of the image with the new alpha
            //image.color = new Color(0f, 0f, 0f, t);
            yield return 0;  //skip to next frame
        }
    }

    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }

    IEnumerator FadeOut(string scene)
    {
        float t = 0f;  //variable for the fade of the image
        Color c = image.color;

        //controls the value of the fade
        while (t > 1.2f)
        {
            t += Time.deltaTime;  //decrease value of t each fram
            float a = curve.Evaluate(t);  //sets the curve for the t time for the fading in effect to look more smooth
            c.a = a;  //sets the alpha color of the image to t 
            image.color = c;  //sets the color of the image with the new alpha
            //image.color = new Color(0f, 0f, 0f, t);
            yield return 0;  //skip to next frame
        }

        SceneManager.LoadScene(scene);
    }

}
