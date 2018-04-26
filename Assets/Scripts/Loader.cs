using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {

    public GameObject OverallGameManager;

	// Use this for initialization
	void Awake () {
		if(OverallGameHandler.instance == null)
        {
            Instantiate(OverallGameManager);
        }
	}
	
}
