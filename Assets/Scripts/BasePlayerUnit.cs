using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinding;

using UnityEngine.UI;

public class BasePlayerUnit : BaseUnit {

    #region Pathfinding variables
    public Transform target;
    Seeker seeker;
    Path path;
    int currentWaypoint;
    #endregion

    public MovementManager moveManager;
    public bool currentlyMoving;
    public bool arrivedAtDestination;

    public Animator theAnimator;

    // Use this for initialization
    protected override void Awake ()
    {
        base.Awake();
        //battleManager = FindObjectOfType<BattleManager>();
        moveManager = FindObjectOfType<MovementManager>();

        theAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        moveAmount = 3;
        moveSpeed = 3;
    }

    // Update is called once per frame
    void Update ()
    {
		if(thisUnitsTurn && !currentlyMoving)
        {
            if (Input.GetKeyUp(KeyCode.W))
            {
                moveManager.CalculateMoves(this);
            }
            else if(Input.GetKeyUp(KeyCode.E))
            {
                EndTurn();
            }
            else if(moveManager.searchMoveMode)
            {
                if(Input.GetKeyUp(KeyCode.Escape))
                {
                    moveManager.searchMoveMode = false;
                }
                else if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.Space))
                {
                    moveManager.searchMoveMode = false;
                    Move();
                }
            }
        }
        else if(thisUnitsTurn && currentlyMoving)
        {
            if (gameObject.transform.position == target.transform.position)
            {
                currentlyMoving = false;
                arrivedAtDestination = true;
            }
            else if (currentWaypoint >= path.vectorPath.Count)
            {
                currentlyMoving = false;
                arrivedAtDestination = true;
                return;
            }
            else if (transform.position != path.vectorPath[currentWaypoint])
            {
                //Debug.Log("Moving to destination");
                transform.position = Vector3.MoveTowards(transform.position, path.vectorPath[currentWaypoint], Time.deltaTime * moveSpeed);
            }
            else
            {
                currentWaypoint++;
            }
        }

        theAnimator.SetBool("isWalking", currentlyMoving);
	}

    public override void Attack()
    {

    }

    public override void Move()
    {
        seeker = GetComponent<Seeker>();
        seeker.StartPath(transform.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
            currentlyMoving = true;
        }
        else
        {
            Debug.Log(p.error);
        }
    }

    public override void BeginTurn()
    {
        Debug.Log("Beginning player unit turn");
        thisUnitsTurn = true;
    }

    public override void EndTurn()
    {
        Debug.Log("Ending " + this.name + "'s turn");
        thisUnitsTurn = false;

        battleManager.NextTurn();
    }

    private void OnMouseDown()
    {
        Debug.Log("Clicked on " + this.name);
        unitInfoPanel.Set(this);
        //unitInfoPanelText.Set(this);
        unitInfoPanel.SetDescriptionText("This is the player unit.");
    }
}
