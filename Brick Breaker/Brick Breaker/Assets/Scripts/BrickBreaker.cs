using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BrickBreaker : MonoBehaviour
{

    public GameObject ballPrefab;
    public static int lives = 3;
    public float ballBottomY = -5.8f;
    public TextMesh livesGT;
    public TextMesh startText;
    public GameObject bricks;

    // Use this for initialization
    void Start()
    {
        //set cursor to invisible
        Cursor.visible = false;
        //get refrence to GameObject 3D text called Lives
        GameObject livesGO = GameObject.Find("Lives");
        livesGT = livesGO.GetComponent<TextMesh>();
        //set the starting lives to 3
        livesGT.text = "3";
        //call setBall function
        startText = GameObject.Find("StartText").GetComponent<TextMesh>();
        SetBall();           
    }

    private void Update()
    {        
        if(bricks.gameObject.transform.childCount == 0)
        {
            SceneManager.LoadScene("_Scene_0");
            BrickBreaker.lives = 3;        
        }
    }

    private void Awake()
    {
        if (PlayerPrefs.HasKey("ScoreCounter"))
        {
            Brick.score = PlayerPrefs.GetInt("ScoreCounter");
        }

        PlayerPrefs.SetInt("ScoreCounter", Brick.score);
    }

    /// <summary>
    /// sets the balls starting position 
    /// </summary>
    public void SetBall()
    {
        GameObject ballGO = Instantiate(ballPrefab) as GameObject;
        Vector2 pos = Vector2.zero;
        pos.y = ballBottomY;
        ballGO.transform.position = pos;
    }    

    /// <summary>
    /// fuction to check how many lives/if game over if not takes away a live/ball
    /// </summary>
    public void BallDestroyed()
    {        
        //restart the game after all balls destroyed
        if (lives == 0)
        {
            SceneManager.LoadScene("_Scene_0");
            lives = 3;
        }
        else
        {
            int lives = int.Parse(livesGT.text);
            lives -= 1;
            livesGT.text = lives.ToString();
            startText.gameObject.SetActive(true);
            SetBall();
        }
    }
}
