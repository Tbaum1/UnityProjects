using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Move : MonoBehaviour {
    public float force;
    public static float bottomY = -8f;
    private float x;
    public TextMesh startText;

    // Use this for initialization
    void Start () {
        x = Random.Range(-.5f, 1f);    
        startText = GameObject.Find("StartText").GetComponent<TextMesh>();
    }
	
	// Update is called once per frame
	void Update () {
        
        //if the ball goes below the paddle destroy ball GameObject        
        if (transform.position.y < bottomY)
        {
            Destroy(this.gameObject);
            BrickBreaker.lives -= 1;
            BrickBreaker bbScript = Camera.main.GetComponent<BrickBreaker>();
            bbScript.BallDestroyed();
        }

        if (Input.GetKey(KeyCode.Space))
        {  
            startText.gameObject.SetActive(false);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(x, 0.5f) * Time.deltaTime * force);
        }        
    }
}
