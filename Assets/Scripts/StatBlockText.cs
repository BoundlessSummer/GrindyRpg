using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class StatBlockText : MonoBehaviour {

    public Text statBlockText;

	// Use this for initialization
	void Start () {
        statBlockText = GetComponent<Text>();
	}

    // Update is called once per frame
    void Update()
    {

    }

    public void Set(BaseUnit unit)
    {
        statBlockText.text = "HP: " + unit.currentHitPoints + "/" + unit.maxHitPoints + "\nSTR: " + unit.strength + "\nDEX: " + unit.dexterity;
    }
}
