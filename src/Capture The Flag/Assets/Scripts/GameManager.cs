using System;

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Assets.Scripts;

public class GameManager : MonoBehaviour
{

    int teamNumber;
    int selectedClass;
    int selectedRPS;
    int currentMenuScreen = 0;
    int currentClassCount = 0;

    bool classSelected = false;
    bool rpsSelected = false;

    public GameObject titleMenu;
    public GameObject infoMenu;
    public GameObject colorSelectMenu;
    public GameObject classSelectMenu;

    public GameObject inGameButtons;
    public GameObject pausedButtons;

    public GameObject lostPanel;
    public GameObject snakePanel;

    public Button possumReviveButton;

    public Text selectedColorText;
    public Text choiceAlertText;

    public Text selectAnimalText;
    public Text selectRPSText;
    public Text snakeText;

    public int classCountBeforeReselect = 3;

    [Header("Buttons")]
    public Button[] classButtons;
    public Button[] rpsButtons;

    [Header("UI Colors")]
    public Color selectedColor;
    public Color selectedAndConfirmedColor;
    public Color defaultDisableColor;

    // Use this for initialization
    void Start()
    {
        // disable menus
        infoMenu.SetActive(false);
        colorSelectMenu.SetActive(false);
        classSelectMenu.SetActive(false);
        lostPanel.SetActive(false);
        snakePanel.SetActive(false);
        // enable menus
        titleMenu.SetActive(true);
    }

    public void setTeamNumber(int number)
    {
        teamNumber = number;
        Debug.Log(teamNumber);
        if (teamNumber == 1)
        {
            selectedColorText.text = "Rood";
        }
        else if (teamNumber == 2)
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
        DisableButtonColors(classButtons.ToList());
        classButtons[buttonNumber].gameObject.SetActive(true);

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
        DisableButtonColors(rpsButtons.ToList());
        rpsButtons[buttonNumber].gameObject.SetActive(true);

        Button currentButton2 = rpsButtons[buttonNumber].GetComponent<Button>();
        ColorBlock cb2 = currentButton2.colors;
        cb2.highlightedColor = Color.green;
        cb2.normalColor = selectedColor;
        cb2.disabledColor = selectedAndConfirmedColor;
        currentButton2.colors = cb2;

        rpsSelected = true;
    }
    private void DisableButtonColors(List<Button> buttons)
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            ColorBlock cb = buttons[i].colors;
            cb.normalColor = Color.gray;
            cb.disabledColor = this.defaultDisableColor;
            buttons[i].colors = cb;
        }
    }



    public void confirmChoice()
    {
        choiceAlertText.text = "";
        if (classSelected && rpsSelected)
        {
            for (int i = 0; i < rpsButtons.Length; i++)
            {
                Button currentButton = rpsButtons[i].GetComponent<Button>();
                currentButton.interactable = false;
            }
            for (int i = 0; i < classButtons.Length; i++)
            {
                Button currentButton = classButtons[i].GetComponent<Button>();
                currentButton.interactable = false;
            }
            if (currentClassCount >= classCountBeforeReselect)
                currentClassCount = 0;

            selectAnimalText.gameObject.SetActive(false);
            selectRPSText.gameObject.SetActive(false);
            inGameButtons.SetActive(true);
            pausedButtons.SetActive(false);
        }
        else
        {
            choiceAlertText.text = "Kies een class & soort!";
        }
    }



    public void AllowSelection()
    {
        selectAnimalText.gameObject.SetActive(true);
        selectRPSText.gameObject.SetActive(true);

        if (currentClassCount >= classCountBeforeReselect)
        {
            classSelected = false;
            selectAnimalText.text = "Kies dier";
            for (int i = 0; i < classButtons.Length; i++)
            {
                Button currentButton = classButtons[i].GetComponent<Button>();
                ResetButton(currentButton);
            }
        }
        else
        {
            Debug.Log("Mag nog geen dier kiezen " + currentClassCount);
            selectAnimalText.text = String.Format("Pas over: {0} respawn", classCountBeforeReselect - currentClassCount);
        }

        rpsSelected = false;
        for (int i = 0; i < rpsButtons.Length; i++)
        {
            Button currentButton = rpsButtons[i].GetComponent<Button>();
            ResetButton(currentButton);
            // button color handler nodig, elke button is groen wanneer ze allemaal een keer aageklikt zijn
        }
    }
    private static void ResetButton(Button currentButton)
    {
        currentButton.interactable = true;

        ColorBlock cb = currentButton.colors;
        cb.normalColor = Color.white;
        cb.highlightedColor = Color.green;
        currentButton.colors = cb;
    }

    public void Won()
    {
        if (selectedClass == 2) // slang
        {
            this.snakeText.text = String.Format("Wil je <color=#00ffffff>{0}</color> behouden of veranderen naar <color=#00ffffff>{1}</color>",
                Helpers.RPSToString(selectedRPS), Helpers.RPSToString(Helpers.LosesOf(selectedRPS)));
            snakePanel.SetActive(true);
        }
        else
            BackToSelection();
    }

    public void SnakeKeep()
    {
        BackToSelection();
    }
    public void SnakeSwitch()
    {
        SetEatenElement();
        BackToSelection();
    }

    private void SetEatenElement()
    {
        selectedRPS = Helpers.LosesOf(selectedRPS);
    }

    public void Lost()
    {
        possumReviveButton.gameObject.SetActive(selectedClass == 0);
        lostPanel.SetActive(true);
    }

    public void Respawn()
    {
        currentClassCount++;
        lostPanel.SetActive(false);
        BackToSelection();
        AllowSelection();
    }

    public void RespawnPossum()
    {
        lostPanel.SetActive(false);
        BackToSelection();
    }

    private void BackToSelection()
    {
        pausedButtons.SetActive(true);
        inGameButtons.SetActive(false);
        snakePanel.SetActive(false);
        UpdateButtonsSelected();
    }

    // ORDER IN LIST OF BUTTONS MATTER ALOT
    private void UpdateButtonsSelected()
    {
        for (int i = 0; i < classButtons.Length; i++)
        {
            if (i != selectedClass) continue;
            disableClassButtons(i);
            break;
        }
        for (int i = 0; i < rpsButtons.Length; i++)
        {
            if (i != selectedRPS) continue;
            disableRPSButtons(i);
            break;
        }
    }
}
