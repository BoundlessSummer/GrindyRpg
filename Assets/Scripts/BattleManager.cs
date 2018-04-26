using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class BattleManager : MonoBehaviour {
    /* The battle manager does the following:
     * Roll initiative for all units
     * Sort the initiative order
     * Call each unit in order to perform a move, then let the next unit perform a move
     * Repeat until combat ends
     * 
     * 
     * Some things to keep track of: 
     * Unit factions to determine when combat ends
     * Dead units
    */
    public BaseUnit[] allUnits;
    initiativeTracker[] initiativeVector;
    public int numUnits;
    List<initiativeTracker> initiativeList;

    public int roundNumber;

    public Text BattleLog;

    bool combatExecuting; //Tell the update function to pay attention
    int initiativeIndex; //Tell the update function who to pay attention to

    public GameObject BattleLogPanel;
    public ScrollRect BattleLogScrollRect;

    public class initiativeTracker
    {
        public int initiative;
        public int allUnitsIndex;
        public bool turnOver;

        public initiativeTracker(int index_in)
        {
            initiative = -1;
            allUnitsIndex = index_in;

            turnOver = false;
        }
    }

	// Use this for initialization
	void Start () {
        roundNumber = 0;

        allUnits = FindObjectsOfType<BaseUnit>();
        numUnits = allUnits.Length;

        combatExecuting = false;

        BattleLog.text = "Initializing BattleManager\n";
        PrintToBattleLog("BattleManager setup complete.");

        initiativeVector = new initiativeTracker[numUnits];
        for(int i = 0; i < numUnits; i++)
        {
            initiativeVector[i] = new initiativeTracker(i);
        }

        initiativeList = new List<initiativeTracker>();

        NewRound();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.S))
        {
            allUnits[0].thisUnitsTurn = true;
            allUnits[0].BeginTurn();
        }

        /*
        if(combatExecuting)
        {
            PrintToBattleLog("Combat Executing. It is " + allUnits[initiativeList[initiativeIndex].allUnitsIndex].name + "'s turn");
            //Have a function NextTurn() that moves the turns over, or calls NewRound() when appropriate
            //Print the turn of the next thing during NextTurn()
            //The Update function just sits around checking if it should call NextTurn()
            if(allUnits[initiativeList[initiativeIndex].allUnitsIndex].thisUnitsTurn == false)
            {
                if(initiativeIndex + 1 == numUnits)
                {
                    //Youve done all the units turns
                    combatExecuting = false;
                }
                else
                {
                    initiativeIndex++;
                    allUnits[initiativeList[initiativeIndex].allUnitsIndex].thisUnitsTurn = true;
                    allUnits[initiativeList[initiativeIndex].allUnitsIndex].BeginTurn();
                }
            }
        }//end if(combatexecuting)
        else
        {
            NewRound();
        }
        */
    }

    public void NextTurn()
    {
        //Evaulate if combat is still ongoing

        //Evaluate if all combatants have performed their turn
        if(initiativeIndex == initiativeList.Count)
        {
            NewRound();
            //End round
        }
        else
        {
            PrintToBattleLog("Unit " + allUnits[initiativeList[initiativeIndex].allUnitsIndex].name + " is performing turn");
            //allUnits[initiativeList[initiativeIndex].allUnitsIndex].thisUnitsTurn = true;
            allUnits[initiativeList[initiativeIndex].allUnitsIndex].BeginTurn();



            initiativeIndex++;
        }
    }

    void NewRound()
    {
        roundNumber++;
        PrintToBattleLog("Starting round " + roundNumber);

        initiativeList.Clear();

        for(int i = 0; i < numUnits; i++)
        {
            initiativeVector[i].initiative = allUnits[i].RollInitiative();
            PrintToBattleLog("Unit " + allUnits[i].name + " rolled an initiative of " + initiativeVector[i].initiative);

            initiativeList.Add(initiativeVector[i]);
        }

        initiativeList.Sort((a, b) => a.initiative.CompareTo(b.initiative));
        initiativeList.Reverse(); //So that it goes highest -> lowest initiative
        for (int i = 0; i < numUnits; i++)
        {
            Debug.Log("Unit " + allUnits[initiativeList[i].allUnitsIndex].name + " has an initiative of " + initiativeList[i].initiative);
        }

        //Note: This is where you roll off for matching initiatives

        combatExecuting = true;
        initiativeIndex = 0;
        NextTurn();
        //allUnits[initiativeList[0].allUnitsIndex].thisUnitsTurn = true;
        //allUnits[initiativeList[0].allUnitsIndex].BeginTurn();
    }

    void PrintToBattleLog(string text_in)
    {
        BattleLog.text += text_in + "\n";
        Canvas.ForceUpdateCanvases();
        BattleLogScrollRect.verticalNormalizedPosition = 0f;
    }
}
