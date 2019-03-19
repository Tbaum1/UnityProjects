using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Brick : MonoBehaviour {

    public TextMesh scoreGT;
    public static int score;

    // Use this for initialization
    void Start () {
        //Find a reference to ScoreCounter GameObject
        GameObject scoreGO = GameObject.Find("ScoreCounter");
        //get the TextMesh component of the GameObject
        scoreGT = scoreGO.GetComponent<TextMesh>();
        //set the starting score to 0
        scoreGT.text = "0";
    }
	
	

    private void OnCollisionEnter2D(Collision2D collision)
    {        
        GameObject collidedWith = collision.gameObject;
        if (collision.gameObject.tag == "ball")
        {
            
            Destroy(gameObject);
            int score = int.Parse(scoreGT.text);
            score += 100;
            scoreGT.text = score.ToString();
        }
    }
}
