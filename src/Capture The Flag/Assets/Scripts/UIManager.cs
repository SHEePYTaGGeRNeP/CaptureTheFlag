﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public List<GameObject> Menus = new List<GameObject>();
    
    public Text selectTeamText;
    public Button selectTeamButton;

    public Text selectClassTitle;
    public Text selectClassDescription;
    public Button selectClassButton;

    public Button selectChoiceButton;
    public Button possumReviveButton;
    public Text selectAnimalText;
    public Text selectRPSText;
    public Text snakeText;

    public Image teamImage;
    public Image classImage;
    public Image QrCode;
    public Image choiceImage;
    public Text classTitle;
    public Text classDescription;
    public Text ChoiceTitle;

    public Button scanButton;
    public Text scanButtonText;
    public bool scannerActive;

    public qrCam qrcam;

    
    public GameManager Game;
    // Use this for initialization
    void Start ()
    {
        Game = transform.GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if (scannerActive)
        {
            if (qrcam.text != null)
                Debug.Log(qrcam.text);
        }
	}

    public void ShowMenu(int index)
    {
        ShowMenu(Menus[index], true);
    }

    public void ShowMenu(string name)
    {
        GameObject menu = null;
        foreach( GameObject m in Menus)
        {
            if (m.name == name)
                 menu = m;
        }   
        ShowMenu(menu, true);
    }

    public void ShowMenu(GameObject menu, bool hideOther)
    {
        HideMenus();
        menu.SetActive(true);

    }

    public void HideMenus()
    {
        foreach (GameObject m in Menus)
        {
            m.SetActive(false);
        }
    }

    public void goToTitleMenu()
    {
       ShowMenu(0);
    }

    public void goToInfo()
    {
        ShowMenu(1);
    }

    public void goToTeamSelect()
    {
       selectTeamText.text = "";
        selectTeamButton.interactable = false;
        ShowMenu(2);
        //currentMenuScreen = 2;
    }

    public void goToClassSelect()
    {
        selectClassButton.interactable = false;
        ShowMenu(3);
        //currentMenuScreen = 3;
    }

    public void goToRPSChoice()
    {
        selectChoiceButton.interactable = false;
        ShowMenu(4);
    }

    public void goToFightScreen()
    {
        ShowMenu(5);
        teamImage.sprite = Game.Player.team.image;
        classImage.sprite = Game.Player.clas.image;
        classTitle.text = Game.Player.clas.className;
        classDescription.text = Game.Player.clas.description;
        ChoiceTitle.text = System.Enum.GetName(Game.Player.Choice.GetType(), Game.Player.Choice-1);
        QrCode.sprite = Game.QR.GenerateQRCode(Game.Player.ToString());
        //choiceImage.sprite = 
    }

    public void ToggleScanner()
    {
        if (!scannerActive)
        {
            qrcam.gameObject.SetActive(true);
            scanButtonText.text = "Terug";
        }
        else
        {
            qrcam.gameObject.SetActive(false);
            scanButtonText.text = "Scan";
        }
    }

    

    public void SetTeamNumber(int id)
    {
        Game.Player.team = Game.GetTeam(id);   
        selectTeamButton.interactable = true;
    }

    public void SetClass(int id)
    {

        selectClassButton.interactable = true;
        Game.Player.clas = Game.GetClass(id);
        selectClassTitle.text = Game.Player.clas.className;
        selectClassDescription.text = Game.Player.clas.description;
    }

    public void SetChoice(int id)
    {
        Game.Player.Choice = (Choice) id;
    }
}
