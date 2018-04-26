using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

using UnityEngine.UI;

public abstract class BaseUnit : MonoBehaviour {

    //Stat block
    public int strength;
    public int dexterity;

    public int currentHitPoints;
    public int maxHitPoints;

    public int moveAmount; //How many tiles you can move in one turn

    public bool thisUnitsTurn;

    public float moveSpeed;

    public BattleManager battleManager;
    public UnitInfoPanel unitInfoPanel;
    public StatBlockText unitInfoPanelText;

    public Sprite portraitSprite;

    public GameObject unitInfo;

	// Use this for initialization
	protected virtual void Awake () {
        //Debug.Log("Running Awake() for BaseUnit");
        thisUnitsTurn = false;
        battleManager = FindObjectOfType<BattleManager>();
        unitInfoPanel = FindObjectOfType<UnitInfoPanel>();
        unitInfoPanelText = unitInfoPanel.GetComponentInChildren<StatBlockText>();


        var clone = Instantiate(unitInfo, transform.position, Quaternion.Euler(Vector3.zero));
        clone.transform.parent = gameObject.transform;
        //Debug.Log(this.name + " ran BaseUnit Awake()");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public int RollInitiative()
    {
        return Random.Range(1, 21) + (dexterity / 2);
    }

    //Things all units can do:
    //Move
    //Attack
    public abstract void Attack();

    public abstract void Move();

    public abstract void BeginTurn();
    //BattleManager calls to tell unit it is their turn

    public abstract void EndTurn();
    //Tell BattleManager you're done

    private void OnMouseDown()
    {
        //  unitInfoPanelText = unitInfoPanel.GetComponent<StatBlockText>();
        Debug.Log("Clicked on " + this.name);
        unitInfoPanelText.Set(this);
    }
}
