using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Paddle : MonoBehaviour {

    
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        
        // Get current screen position of the mouse from Input
        Vector3 mousePos2D = Input.mousePosition;

        //Get camera position set how far to push the mouse
        mousePos2D.z = -Camera.main.transform.position.z;

        //Convert the point from 2D screen space into 3D game world
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        //move the x position of the basket to the x position of the mouse
        Vector3 pos = this.transform.position;

        //constrains the mouse to the game area
        pos.x = Mathf.Clamp(mousePos3D.x, -12.65f, 11.14f);
        this.transform.position = pos;
    }
}
