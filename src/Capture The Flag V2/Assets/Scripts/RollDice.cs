using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RollDice : MonoBehaviour {

    int rolledNumber = 0;
    public int lowestNumber = 1;
    public int highestNumber = 6;
    public int points = 0;
    public Transform button1;
    public int bonusPoints = 0;
    [Header("Buttons")]
    public GameObject[] singleUseButtons;
    public GameObject[] permaButtons;
    [Header("UI Elements")]
    public Text scoreUI;
    public Text rolledNumberUI;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        scoreUI.text = points.ToString();
        rolledNumberUI.text = rolledNumber.ToString();
    }

    public void Roll ()
    {
        rolledNumber = Random.Range(lowestNumber, highestNumber + 1);
        Debug.Log(rolledNumber);
        rolledNumber = rolledNumber + bonusPoints;
        Debug.Log(rolledNumber);

        resetSingleUsePowers();
    }

    public void addPoint ()
    {
        points++;
    }
    public void removePoints(int Cost)
    {
        if (points < Cost)
        {
            return;
        }
        else
        {
            points = points - Cost;
        }
    }


    // Skills
    void resetSingleUsePowers()
    {
        bonusPoints = 0;
        lowestNumber = 1;
        highestNumber = 6;
        for (int i = 0; i < singleUseButtons.Length; i++)
        {
            skillScript SkillScript = singleUseButtons[i].GetComponent<skillScript>();
            SkillScript.resetButton();
            //singleUseButtons[i].resetButton();
        }
    }
    public void addBonusPoints(int Bonus)
    {
        bonusPoints = Bonus;
    }

    public void ResetDefaultParameters ()
    {
        lowestNumber = 1;
        highestNumber = 6;
    }
}
