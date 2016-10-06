using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

    int teamNumber;
    int selectedClass;
    int selectedRPS;
    int currentMenuScreen = 0;

    bool classSelected = false;
    bool rpsSelected = false;

    public GameObject titleMenu;
    public GameObject infoMenu;
    public GameObject colorSelectMenu;
    public GameObject classSelectMenu;

    public GameObject inGameButtons;
    public GameObject pausedButtons;

    public Text selectedColorText;
    public Text choiceAlertText;

    [Header("Buttons")]
    public GameObject[] classButtons;
    public GameObject[] rpsButtons;

    [Header("UI Colors")]
    public Color selectedColor;
    public Color selectedAndConfirmedColor;

	// Use this for initialization
	void Start () {
        // disable menus
        infoMenu.SetActive(false);
        colorSelectMenu.SetActive(false);
        classSelectMenu.SetActive(false);
        // enable menus
        titleMenu.SetActive(true);
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setTeamNumber(int number)
    {
        teamNumber = number;
        Debug.Log(teamNumber);
        if (teamNumber == 1)
        {
            selectedColorText.text = "Rood";
        } else if (teamNumber == 2)
        {
            selectedColorText.text = "Geel";
        }
    }
    public void goToTitleMenu()
    {
        // disable menus
        infoMenu.SetActive(false);
        colorSelectMenu.SetActive(false);
        classSelectMenu.SetActive(false);
        // enable menus
        titleMenu.SetActive(true);
        currentMenuScreen = 0;
    }
    public void goToInfo()
    {
        // disable menus
        titleMenu.SetActive(false);
        colorSelectMenu.SetActive(false);
        classSelectMenu.SetActive(false);
        // enable menus
        infoMenu.SetActive(true);
        currentMenuScreen = 1;
    }
    public void goToColorSelect()
    {
        selectedColorText.text = "";
       // disable menus
       titleMenu.SetActive(false);
        infoMenu.SetActive(false);
        classSelectMenu.SetActive(false);
        // enable menus
        colorSelectMenu.SetActive(true);
        currentMenuScreen = 2;
    }
    public void goToClassSelect()
    {
        if (teamNumber == 0)
        {
            selectedColorText.text = "Kies een team!";
            return;
        }
        colorSelectMenu.SetActive(false);
        classSelectMenu.SetActive(true);
        currentMenuScreen = 3;
    }

    // Class & RPS Settings
    public void setClass(int ClassNumber)
    {
        selectedClass = ClassNumber;
    }
    public void setRPS(int RPSNumber)
    {
        selectedRPS = RPSNumber;
    }

    // Button toggles & stuff
    public void disableClassButtons(int buttonNumber)
    {
        for (var i = 0; i < classButtons.Length; i++) {
            //classButtons[i].SetActive(false);

            Button currentButton = classButtons[i].GetComponent<Button>();
            ColorBlock cb = currentButton.colors;
            cb.normalColor = Color.gray;
            currentButton.colors = cb;
            //currentButton.colors.normalColor = new Color(0.22f, 0.22f, 0.22f, 1f);
        }
        classButtons[buttonNumber].SetActive(true);

        Button currentButton2 = classButtons[buttonNumber].GetComponent<Button>();
        ColorBlock cb2 = currentButton2.colors;
        cb2.highlightedColor = Color.green;
        cb2.normalColor = selectedColor;
        cb2.disabledColor = selectedAndConfirmedColor;
        currentButton2.colors = cb2;

        classSelected = true;

    }
    public void disableRPSButtons(int buttonNumber)
    {

        for (var i = 0; i < rpsButtons.Length; i++)
        {
           // rpsButtons[i].SetActive(false);

            Button currentButton = rpsButtons[i].GetComponent<Button>();
            ColorBlock cb = currentButton.colors;
            cb.normalColor = Color.gray;
            currentButton.colors = cb;
        }
        rpsButtons[buttonNumber].SetActive(true);

        Button currentButton2 = rpsButtons[buttonNumber].GetComponent<Button>();
        ColorBlock cb2 = currentButton2.colors;
        cb2.highlightedColor = Color.green;
        cb2.normalColor = selectedColor;
        cb2.disabledColor = selectedAndConfirmedColor;
        currentButton2.colors = cb2;

        rpsSelected = true;
    }

    public void confirmChoice()
    {
        choiceAlertText.text = "";
        if (classSelected && rpsSelected)
        {
            for (var i = 0; i < rpsButtons.Length; i++)
            {
                Button currentButton = rpsButtons[i].GetComponent<Button>();
                currentButton.interactable = false;
            }
            for (var i = 0; i < classButtons.Length; i++)
            {
                Button currentButton = classButtons[i].GetComponent<Button>();
                currentButton.interactable = false;
            }

            inGameButtons.SetActive(true);
            pausedButtons.SetActive(false);
            // TO DO: Terug en OK button weg, Respawn & stop button aan, class change timer activeren.
        }
        else
        {
            choiceAlertText.text = "Kies een class & soort!";
        }
    }
    public void stop()
    {
        pausedButtons.SetActive(true);
        inGameButtons.SetActive(false);

        for (var i = 0; i < rpsButtons.Length; i++)
        {
            Button currentButton = rpsButtons[i].GetComponent<Button>();
            currentButton.interactable = true;
            
           // button color handler nodig, elke button is groen wanneer ze allemaal een keer aageklikt zijn
        }
        for (var i = 0; i < classButtons.Length; i++)
        {
            Button currentButton = classButtons[i].GetComponent<Button>();
            currentButton.interactable = true;
            
            ColorBlock cb = currentButton.colors;
            cb.normalColor = Color.white;
            currentButton.colors = cb;
        }
    }
}
