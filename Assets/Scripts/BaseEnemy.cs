using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinding;

public class BaseEnemy : BaseUnit {

    public int goldValue;
    public int EXPValue;

    public MovementManager moveManager;
    public Seeker seeker;
    public Path path;
    public Transform target;
    public bool arrivedAtDestination;

    public int currentWaypoint;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void Attack()
    {

    }

    public override void Move()
    {

    }

    public override void BeginTurn()
    {
        moveManager.SetTileUnblocked(Mathf.RoundToInt(this.transform.position.x), Mathf.RoundToInt(this.transform.position.y));
    }

    public override void EndTurn()
    {
        thisUnitsTurn = false;
        battleManager.NextTurn();
    }

    protected virtual void OnMouseDown()
    {
        Debug.Log("Clicked on " + this.name);
        unitInfoPanel.Set(this);
        //unitInfoPanelText.Set(this);
    }
}
