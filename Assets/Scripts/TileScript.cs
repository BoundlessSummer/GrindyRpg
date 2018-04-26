using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour {

    public SpriteRenderer theSR;
    public GameObject target;
    public TargetScript theTarget;

    //public PlayerController thePlayerController;

    public GameObject waypointHover;

    public int xPos;
    public int yPos;

    public MovementManager theMovementManager;

    Color highlightColor;
    Color hoverColor;

    bool searchMoveModeLast;

	// Use this for initialization
	void Start () {
        theSR = gameObject.GetComponent<SpriteRenderer>();

        theTarget = FindObjectOfType<TargetScript>();
        target = theTarget.gameObject;

        //thePlayerController = FindObjectOfType<PlayerController>();

        theMovementManager = FindObjectOfType<MovementManager>(); //Note: Consider dragging this into prefab, to reduce cost

        highlightColor = Color.green;
        highlightColor.a = .5f;

        hoverColor = Color.yellow;
        highlightColor.a = .6f;
	}
	
	// Update is called once per frame
	void Update () {
        /*
		if(!theMovementManager.searchMoveMode)
        {
            theSR.material.color = Color.white;
            searchMoveModeLast = false;
        }
        else if(theMovementManager.searchMoveMode && !searchMoveModeLast)
        {
            searchMoveModeLast = true;
            if(theMovementManager.searchMoveMode && theMovementManager.validMoves[xPos, yPos] == true && gameObject.layer != 8)
            {
                theSR.material.color = highlightColor;
            }
        }
        else
        {

        }
        */

        if (!theMovementManager.searchMoveMode)
        {
            theSR.material.color = Color.white;
        }
    }

    public void SetHighlight()
    {
        theSR.material.color = highlightColor;
    }

    public void DeactivateHighlight()
    {
        theSR.material.color = Color.white;
    }

    private void OnMouseOver()
    {
        if(gameObject.layer == 8) //If you're on the blocking layer
        {
            //Do nothing
            return;
        }
        /*
        if (theSR.material.color == Color.white)
        {
            theSR.material.color = Color.red;
        }
        */

        /*
        if(theSR.material.color == Color.blue)
        {
            Color temp = Color.yellow;
            temp.a = .5f;
            theSR.material.color = temp;
            
        }
        */

        if(theMovementManager.searchMoveMode && theMovementManager.validMoves[xPos,yPos] == true)
        {
            theSR.material.color = hoverColor;
        }
    }

    private void OnMouseExit()
    {
        if (gameObject.layer == 8) //If you're on the blocking layer
        {
            //Do nothing
            return;
        }
        /*
        if (theSR.material.color == Color.red)
        {
            theSR.material.color = Color.white;
        }
        */

        /*
        if(theSR.material.color == Color.yellow)
        {
            theSR.material.color = Color.blue;
        }
        */
        if (theMovementManager.searchMoveMode && theMovementManager.validMoves[xPos, yPos] == true)
        {
            theSR.material.color = highlightColor;
        }
    }

    private void OnMouseDown()
    {
        if (gameObject.layer == 8) //If you're on the blocking layer
        {
            //Do nothing
            return;
        }
        //if(theSR.material.color == Color.yellow)
        if (theMovementManager.searchMoveMode && theMovementManager.validMoves[xPos, yPos] == true)
        {
            target.transform.position = gameObject.transform.position;
        }
    }
}
