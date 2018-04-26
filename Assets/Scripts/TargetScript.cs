using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour {

    public MovementManager moveManager;
    bool searchMoveMode;

    SpriteRenderer waypointSR;

    // Use this for initialization
    void Start () {
        moveManager = FindObjectOfType<MovementManager>();
        searchMoveMode = moveManager.searchMoveMode;

        waypointSR = GetComponentInChildren<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        searchMoveMode = moveManager.searchMoveMode;

        if (searchMoveMode)
        {
            waypointSR.enabled = true;
        }
        else
        {
            waypointSR.enabled = false;
        }

        /*
        if(gameObject.transform.position == thePC.transform.position)
        {
            waypointSR.enabled = false;
        }
        */
	}
}
