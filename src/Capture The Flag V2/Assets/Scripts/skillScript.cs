using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class skillScript: MonoBehaviour {
    public bool buttonEffectActive = false;
    public int Bonus = 1;
    public int Cost = 2;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
	
	}
    public void addBonusPoints ()
    {
        
        GameObject gameManager = GameObject.Find("GameManager");
        RollDice diceRollScript = gameManager.GetComponent<RollDice>();

        if (diceRollScript.points < Cost)
        {
            return;
        }
        else
        {
            diceRollScript.bonusPoints = diceRollScript.bonusPoints + Bonus;
            buttonEffectActive = true;
            this.GetComponent<Button>().interactable = false;
        }        
    }
    public void changeLowest (int low)
    {
        GameObject gameManager = GameObject.Find("GameManager");
        RollDice diceRollScript = gameManager.GetComponent<RollDice>();
        if (diceRollScript.points < Cost)
        {
            return;
        }
        else
        {
            diceRollScript.lowestNumber = low;
            buttonEffectActive = true;
            this.GetComponent<Button>().interactable = false;
        }
    }
    public void changeHighest(int high)
    {
        GameObject gameManager = GameObject.Find("GameManager");
        RollDice diceRollScript = gameManager.GetComponent<RollDice>();
        if (diceRollScript.points < Cost)
        {
            return;
        }
        else
        {
            diceRollScript.highestNumber = high;
            buttonEffectActive = true;
            this.GetComponent<Button>().interactable = false;
        }
    }
    public void resetButton ()
    {
        this.GetComponent<Button>().interactable = true;
    }
}
