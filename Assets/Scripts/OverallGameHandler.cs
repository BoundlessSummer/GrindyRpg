using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class OverallGameHandler : MonoBehaviour {

    public static OverallGameHandler instance = null;
    public BoardManager boardScript;
    
    private int level = 3;

	// Use this for initialization
	void Start () {
		
	}

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        boardScript = GetComponent<BoardManager>();
        InitGame();
    }
	
	// Update is called once per frame
	void Update () {
	}

    void InitGame()
    {
        boardScript.SetupScene(level);
    }
}
