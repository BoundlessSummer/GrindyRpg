using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinding;

public class MovementManager : MonoBehaviour {

    public OverallGameHandler theGameHandler;
    public BoardManager theBoardManager;

    //private List<GameObject> floorTileList;
    public GameObject[,] floorTilesArray;

    //private int totalNumTiles;
    public bool[,] validMoves;

    public Transform target;
    Seeker seeker;
    Path path;
    int currentWaypoint;

    Vector2 lastTile;
    int lastX;
    int lastY;

    int playerSpeed;

    public bool validityVerbose;

    public bool searchMoveMode;

    private void Awake()
    {
        theGameHandler = FindObjectOfType<OverallGameHandler>();
        theBoardManager = theGameHandler.GetComponent<BoardManager>();

        searchMoveMode = false;
    }

    // Use this for initialization
    void Start() {
        //theGameHandler = FindObjectOfType<OverallGameHandler>();
        //theBoardManager = theGameHandler.GetComponent<BoardManager>();


        //floorTileList = theBoardManager.floorTileList;

        /*
        validMoves = new bool[theBoardManager.columns, theBoardManager.rows];

        int tileCounter = 0;
        for (int x = 0; x < theBoardManager.columns; x++)
        {
            for (int y = 0; y < theBoardManager.rows; y++)
            {
                floorTilesArray[x, y] = floorTileList[tileCounter];
                tileCounter++;

                validMoves[x, y] = true;
            }
        }

        totalNumTiles = floorTileList.Count;

        validMoves = new bool[theBoardManager.columns, theBoardManager.rows];
        */


        floorTilesArray = new GameObject[theBoardManager.columns, theBoardManager.rows];
        int tileCounter = 0;
        for (int x = 0; x < theBoardManager.columns; x++)
        {
            for (int y = 0; y < theBoardManager.rows; y++)
            {
                floorTilesArray[x, y] = theBoardManager.floorTileList[tileCounter];
                tileCounter++;
            }
        }
    }

    // Update is called once per frame
    void Update() {
    }

    /*
    public void CalculateMoves(PlayerController player)
    {
        Debug.Log("Starting new movement calculation");

        //floorTileList = theBoardManager.floorTileList;
        //Debug.Log("Boardmanager floortilelist: " + theBoardManager.floorTileList.Count);
        //Debug.Log("this floortilelist: " + floorTileList.Count);
        validMoves = new bool[theBoardManager.columns, theBoardManager.rows];
        floorTilesArray = new GameObject[theBoardManager.columns,theBoardManager.rows];

        int tileCounter = 0;
        for (int x = 0; x < theBoardManager.columns; x++)
        {
            for (int y = 0; y < theBoardManager.rows; y++)
            {
                floorTilesArray[x, y] = theBoardManager.floorTileList[tileCounter];
                tileCounter++;

                validMoves[x, y] = false;
            }
        }

        //totalNumTiles = floorTileList.Count;

        

        //Debug.Log("Num tiles: " + floorTileList.Count);




        playerSpeed = player.numMoves;
        Vector2 playerPos = player.transform.position;
        seeker = player.GetComponent<Seeker>();

        validityVerbose = false;

        //Heuristic to throw out most of the invalid tiles
        for (int x = 0; x < theBoardManager.columns; x++)
        {
            for (int y = 0; y < theBoardManager.rows; y++)
            {
                if(validityVerbose)
                    Debug.Log("Testing position " + x + "," + y);

                //if ((playerPos.x - x) * (playerPos.x - x) + (playerPos.y - y) * (playerPos.y - y) > playerSpeed * playerSpeed)
                //Debug.Log("Int rounded distance.abs: " + (int)Mathf.Abs(playerPos.x - x) + "," + (int)Mathf.Abs(playerPos.y - y));
                //Debug.Log((int)Mathf.Abs(playerPos.x - x) + (int)Mathf.Abs(playerPos.y - y));
                if (((int)Mathf.Abs(playerPos.x - x) + (int)Mathf.Abs(playerPos.y - y)) > playerSpeed)
                {
                    //Debug.Log(("Player pos: " + playerPos.x + "," + playerPos.y + "    x,y: " + x + "," + y + "    playerSpeed: " + playerSpeed));
                    //Debug.Log(Mathf.Abs(playerPos.x - x) + "+" + Mathf.Abs(playerPos.y - y) + ">" + playerSpeed);
                    validMoves[x, y] = false;

                    if (validityVerbose)
                        Debug.Log("Position" + x + "," + y + " outside of heuristic. Invalid");
                }
                else if (floorTilesArray[x, y].layer == 8)
                {
                    if (validityVerbose)
                        Debug.Log("Position" + x + "," + y + " is not walkable. Invalid");
                    validMoves[x, y] = false;
                }
                else
                {
                    //Debug.Log("xy:" + x + "," + y);
                    lastTile = new Vector2(x, y);
                    lastX = x;
                    lastY = y;
                    //seeker.StartPath(player.transform.position, lastTile, OnPathComplete);
                    //.Log("player.transform.position: " + player.transform.position.x + "," + player.transform.position.y + "    lastTile:" + lastTile.x + "," + lastTile.y);
                    Path p = seeker.StartPath(player.transform.position, lastTile);
                    p.BlockUntilCalculated();
                    OnPathComplete(p);
                    

                }
            }
        }

        for (int x = 0; x < theBoardManager.columns; x++)
        {
            for (int y = 0; y < theBoardManager.rows; y++)
            {
                if(validMoves[x, y] == true)
                {
                    //SpriteRenderer SR = floorTilesArray[x, y].GetComponent<SpriteRenderer>();
                    //SR.material.color = Color.blue;
                }
            }
        }
    }
    */

    public void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;

            //Debug.Log("Path length:" + path.vectorPath.Count);
            //Debug.Log("path.vectorPath.Count: " + path.vectorPath.Count);
            if(path.vectorPath.Count > playerSpeed+1) //It counts the initial spot as a waypoint
            {
                validMoves[lastX, lastY] = false;

                if (validityVerbose)
                    Debug.Log("Reachable, but too far: " + lastX + "," + lastY);
            }
            else
            {
                validMoves[lastX, lastY] = true;

                if (validityVerbose)
                    Debug.Log("Valid location: " + lastX + "," + lastY);
            }
        }
        else
        {
            validMoves[lastX, lastY] = false;
            Debug.Log("Path error: ");
            Debug.Log(p.error);
        }
    }




    public Path MoveRandomDirection(BaseUnit theUnit)
    {
        //A unit asks to be moved in a random direction, given its movement type

        /*
         * Build an array of possibly valid tiles.
         * Pick one at random
         * Try to path to it; if not possible, remove from valid tiles, pick another at random
         * If path is possible, move to it and end turn.
         */

        /*
         * Alternative plan:
         * Pick ANY random spot on the map, regardless of distance
         * Stop when you either 
         *      A) Move the max number of tiles you can this turn
         *      B) Reach the target
         */
        AstarPath.active.Scan();

        int randomX = Random.Range(0, theBoardManager.columns);
        //int randomX = Random.Range(0, theGameHandler.GetComponent<BoardManager>().columns);
        int randomY = Random.Range(0, theBoardManager.rows);
        //Debug.Log("Randomly chosen x,y:  " + randomX + "," + randomY);

        //If this is a valid tile...Check collisions with other objects

        seeker = theUnit.GetComponent<Seeker>();
        target.transform.position = new Vector2(randomX, randomY);

        Path p = seeker.StartPath(theUnit.transform.position, target.position);
        p.BlockUntilCalculated();

        if(!p.error)
        {
            return p;
            //valid path found
        }
        else
        {
            Debug.Log(p.error);
            return null;
        }
    }

    public Path MoveTowardsPlayer(BaseUnit theUnit)
    {
        Path p = null;
        return p;
    }


    public void CalculateMoves(BasePlayerUnit player)
    {
        Debug.Log("Calculating all moves for player unit " + player.name);
        AstarPath.active.Scan();

        searchMoveMode = true;

        validMoves = new bool[theBoardManager.columns, theBoardManager.rows];
        //floorTilesArray = new GameObject[theBoardManager.columns, theBoardManager.rows];

        //int tileCounter = 0;
        for (int x = 0; x < theBoardManager.columns; x++)
        {
            for (int y = 0; y < theBoardManager.rows; y++)
            {
                //floorTilesArray[x, y] = theBoardManager.floorTileList[tileCounter];
                //tileCounter++;

                validMoves[x, y] = false;
            }
        }

        playerSpeed = player.moveAmount;
        Vector2 playerPos = player.transform.position;
        seeker = player.GetComponent<Seeker>();


        //DEBUGGING: See every tile evaluation
        validityVerbose = false;

        //Heuristic to throw out most of the invalid tiles
        for (int x = 0; x < theBoardManager.columns; x++)
        {
            for (int y = 0; y < theBoardManager.rows; y++)
            {
                if (validityVerbose)
                    Debug.Log("Testing position " + x + "," + y);

                //if ((playerPos.x - x) * (playerPos.x - x) + (playerPos.y - y) * (playerPos.y - y) > playerSpeed * playerSpeed)
                //Debug.Log("Int rounded distance.abs: " + (int)Mathf.Abs(playerPos.x - x) + "," + (int)Mathf.Abs(playerPos.y - y));
                //Debug.Log((int)Mathf.Abs(playerPos.x - x) + (int)Mathf.Abs(playerPos.y - y));
                if (((int)Mathf.Abs(playerPos.x - x) + (int)Mathf.Abs(playerPos.y - y)) > playerSpeed)
                {
                    //Debug.Log(("Player pos: " + playerPos.x + "," + playerPos.y + "    x,y: " + x + "," + y + "    playerSpeed: " + playerSpeed));
                    //Debug.Log(Mathf.Abs(playerPos.x - x) + "+" + Mathf.Abs(playerPos.y - y) + ">" + playerSpeed);
                    validMoves[x, y] = false;

                    if (validityVerbose)
                        Debug.Log("Position" + x + "," + y + " outside of heuristic. Invalid");
                }
                else if (floorTilesArray[x, y].layer == 8)
                {
                    if (validityVerbose)
                        Debug.Log("Position" + x + "," + y + " is not walkable. Invalid");
                    validMoves[x, y] = false;
                }
                else
                {
                    //Debug.Log("xy:" + x + "," + y);
                    lastTile = new Vector2(x, y);
                    lastX = x;
                    lastY = y;
                    //seeker.StartPath(player.transform.position, lastTile, OnPathComplete);
                    //.Log("player.transform.position: " + player.transform.position.x + "," + player.transform.position.y + "    lastTile:" + lastTile.x + "," + lastTile.y);
                    Path p = seeker.StartPath(player.transform.position, lastTile);
                    p.BlockUntilCalculated();
                    
                    
                    //OnPathComplete(p);
                    if(!p.error)
                    {
                       if(p.vectorPath.Count > player.moveAmount + 1)
                        {
                            validMoves[lastX, lastY] = false;
                            if (validityVerbose)
                                Debug.Log("Reachable, but too far: " + lastX + "," + lastY);
                        }
                        else
                        {
                            validMoves[lastX, lastY] = true;
                            if (validityVerbose)
                                Debug.Log("Valid location: " + lastX + "," + lastY);
                        }
                    }
                    else
                    {
                        validMoves[lastX, lastY] = false;
                        Debug.Log("Path error: ");
                        Debug.Log(p.error);
                    }

                }
            }
        }

        /*
        for (int x = 0; x < theBoardManager.columns; x++)
        {
            for (int y = 0; y < theBoardManager.rows; y++)
            {
                if (validMoves[x, y] == true)
                {
                    //SpriteRenderer SR = floorTilesArray[x, y].GetComponent<SpriteRenderer>();
                    //SR.material.color = Color.blue;
                    floorTilesArray[x, y].GetComponent<TileScript>().SetHighlight();
                }
            }
        }
        */

        ToggleTileHighlights(1);
        target.transform.position = player.transform.position;
    }

    void ToggleTileHighlights(int OnOff)
    {
        //On = 1, Off = 0
        for (int x = 0; x < theBoardManager.columns; x++)
        {
            for (int y = 0; y < theBoardManager.rows; y++)
            {
                if (validMoves[x, y] == true)
                {
                    //SpriteRenderer SR = floorTilesArray[x, y].GetComponent<SpriteRenderer>();
                    //SR.material.color = Color.blue;
                    if(OnOff == 1)
                    {
                        floorTilesArray[x, y].GetComponent<TileScript>().SetHighlight();

                    }
                    else
                    {
                        floorTilesArray[x, y].GetComponent<TileScript>().DeactivateHighlight();
                    }
                }
            }
        }
    }

    public void SetTileBlocked(int x, int y)
    {
        floorTilesArray[x, y].layer = 8;
    }
    public void SetTileUnblocked(int x, int y)
    {
        floorTilesArray[x, y].layer = 0;
    }
}
