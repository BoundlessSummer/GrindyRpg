using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinding;

public class GreenSlime : BaseEnemy {

    public bool doneMoving;

    // Use this for initialization
    void Start () {
        strength = Random.Range(1, 4);
        //dexterity = Random.Range(1, 3);
        dexterity = 1;
        maxHitPoints = Random.Range(10, 20);
        currentHitPoints = maxHitPoints;
        moveAmount = 2;

        moveSpeed = 3.0f;

    }
	
	// Update is called once per frame
	void Update ()
    {
        if(thisUnitsTurn && !doneMoving)
        {
            if (path == null)
            {
                doneMoving = true;
            }
            else if (currentWaypoint >= path.vectorPath.Count || currentWaypoint >= moveAmount + 1)
            {
                doneMoving = true;
            }
            else
            {
                //Debug.Log("Current waypoint: " + currentWaypoint + ",  path.vectorPath.Count: " + path.vectorPath.Count);
                if(Mathf.Abs(transform.position.x - path.vectorPath[currentWaypoint].x) < 0.1f && Mathf.Abs(transform.position.y - path.vectorPath[currentWaypoint].y) < 0.1f)
                {
                    transform.position = path.vectorPath[currentWaypoint];
                    currentWaypoint++;
                }
                else if (transform.position != path.vectorPath[currentWaypoint])
                {
                    transform.position = Vector3.MoveTowards(transform.position, path.vectorPath[currentWaypoint], Time.deltaTime * moveSpeed);
                }
                else
                {
                    currentWaypoint++;
                }
            }

        }
        else if(thisUnitsTurn && doneMoving)
        {
            EndTurn();
        }
    }

    public override void BeginTurn()
    {
        Debug.Log("Beginning Green Slime turn");
        base.BeginTurn();
        

        thisUnitsTurn = true;
        AstarPath.active.Scan();
        //Not sure if I need it. Could be useful to make sure it doesn't bump into other units

        currentWaypoint = 0;

        doneMoving = false;
        path = moveManager.MoveRandomDirection(this);
    }

    public override void EndTurn()
    {
        //Debug.Log("Ending turn of " + this.name);
        //Debug.Log("Setting tile as blocked at " + Mathf.RoundToInt(this.transform.position.x) + "," + Mathf.RoundToInt(this.transform.position.y));
        moveManager.SetTileBlocked(Mathf.RoundToInt(this.transform.position.x), Mathf.RoundToInt(this.transform.position.y));

        thisUnitsTurn = false;
        battleManager.NextTurn();
        //base.EndTurn();
    }

    protected override void OnMouseDown()
    {
        base.OnMouseDown();

        unitInfoPanel.SetDescriptionText("A green slime. Go poke it.");
    }
}
