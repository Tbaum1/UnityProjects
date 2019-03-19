using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour {

    public static Transform[] waypoints;  //create array of Transforms

    private void Awake()
    {
        waypoints = new Transform[transform.childCount];  //instaniate array of however may waypoints that are in the Waypoints object

        //set waypoints to the correct waypoint in the parent object
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = transform.GetChild(i);  
        }
    }
}
