using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class UnitInfo : MonoBehaviour {

    public Text HPText;
    public Text NameText;

    public BaseUnit unit;

	// Use this for initialization
	void Start () {
        unit = GetComponentInParent<BaseUnit>();


	}
	
	// Update is called once per frame
	void Update () {
        //NOTE: Should probably have a function that manually updates this
        HPText.text = "HP: " + unit.currentHitPoints + " / " + unit.maxHitPoints;
        NameText.text = "" + unit.name;
    }
}
