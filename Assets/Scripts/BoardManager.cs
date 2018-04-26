using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

//https://www.youtube.com/watch?v=bvvaqAbpPjc&feature=youtu.be
//Unity Tutorial

public class BoardManager : MonoBehaviour {

    public class Count
    {
        public int minimum;
        public int maximum;

        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    public int columns;
    public int rows;

    public GameObject floors;
    public Sprite[] floorSprites;

    public GameObject outerTiles;
    public Sprite[] outerTileSprites;

    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();

    private BoxCollider2D theBoxCollider;

    //public GameObject[,] floorTilesArray;
    //Unity does not serialize multidimensional arrays

    [System.Serializable]
    public class FloorArrayWrapper
    {
        public GameObject[] floorArray;

        public FloorArrayWrapper(int rows)
        {
            floorArray = new GameObject[rows];
        }
    }

    public FloorArrayWrapper[] floorTileArray;

    public GameObject[] floorTilesOneArray;

    public List<GameObject> floorTileList = new List<GameObject>();

    private void Awake()
    {
        //Debug.Log("BoardManager Awake()");
        floorTileArray = new FloorArrayWrapper[columns];
        for(int i = 0; i < columns; i++)
        {
            floorTileArray[i] = new FloorArrayWrapper(rows);
        }

        //Debug.Log("Size of floorTileArray: " + columns + ", " + rows);

        floorTilesOneArray = new GameObject[rows * columns];
    }

    void InitializeList()
    {
        gridPositions.Clear();

        for(int x = 1; x < columns - 1; x++)
        {
            for(int y = 1; y < rows - 1; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }

    void BoardSetup()
    {
        boardHolder = new GameObject("Board").transform;

        int floorTileIndex = 0;
        for (int x = -1; x < columns + 1; x++)
        {
            for (int y = -1; y < rows + 1; y++)
            {
                GameObject toInstantiate;
                if(x == -1 || x == columns || y == -1 || y == rows) //Outer ring
                {
                    toInstantiate = outerTiles;
                    SpriteRenderer SR = toInstantiate.GetComponent<SpriteRenderer>();
                    SR.sprite = outerTileSprites[Random.Range(0, outerTileSprites.Length)];
                    theBoxCollider = toInstantiate.GetComponent<BoxCollider2D>();
                    theBoxCollider.isTrigger = false;
                    toInstantiate.layer = 8; //8 is the Blocking layer
                }
                else
                {
                    toInstantiate = floors;
                    SpriteRenderer SR = toInstantiate.GetComponent<SpriteRenderer>();
                    SR.sprite = floorSprites[Random.Range(0, floorSprites.Length)];
                    toInstantiate.layer = 3;

                    TileScript theTS = toInstantiate.GetComponent<TileScript>();
                    theTS.xPos = x;
                    theTS.yPos = y;
                }
                

                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);

                if (x == -1 || x == columns || y == -1 || y == rows) //Outer ring
                {

                }
                else
                {
                    floorTileArray[x].floorArray[y] = instance;
                    //Debug.Log("Inserted: " + x + " " + y + " to floor tile array");

                    floorTilesOneArray[floorTileIndex] = instance;
                    floorTileIndex++;

                    floorTileList.Add(instance);
                    //Debug.Log("List length:" + floorTileList.Count);
                }
            }
        }
    }

    Vector3 RandomPosition()
    {
        int randomIndex = Random.Range(0, gridPositions.Count);
        Vector3 randomPosition = gridPositions[randomIndex];
        gridPositions.RemoveAt(randomIndex); //Ensures you don't spawn 2 things on the same tile
        return randomPosition;
    }

    void LayoutObjectsAtRandom(GameObject[] tileArray, int minimum, int maximum)
    {
        //Give it an array of possible tiles, and min/max count, and it will distribute them across the board
        int objectCount = Random.Range(minimum, maximum + 1);

        for(int i = 0; i < objectCount; i++)
        {
            Vector3 randomPosition = RandomPosition();
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
            Instantiate(tileChoice, randomPosition, Quaternion.identity);
        }
    }

    public void SetupScene(int level)
    {
        BoardSetup();
        InitializeList();

        //Eg:
        //LayoutObjectsAtRandom(powerupTiles, powerupMin, powerupMax);
    }
}
