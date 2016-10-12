using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Assets.Scripts;

using UnityEngine.UI;
using System;

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
    public Text debugText;
    public Image RespawnQRCode;
    public Text RespawnInfoText;
    public Text RespawnWarningText;
    public Text BuidelratRespawnInfoText;
    public Button BuidelratButton;
    public Button scanButton;
    public Text scanButtonText;
    public bool scannerActive;
    
    
    public qrCam qrcam;

    public Sprite[] rpsSprites;
    
    public GameManager Game;

    private bool battleCanvas;
    private bool respawnCanvas;

    // Use this for initialization
    void Start ()
    {
        Game = transform.GetComponent<GameManager>();
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
        qrcam.gameObject.SetActive(false);
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
        battleCanvas = true;
        ShowMenu(5);
        teamImage.sprite = Game.Player.team.image;
        classImage.sprite = Game.Player.clas.image;
        classTitle.text = Game.Player.clas.className;
        classDescription.text = Game.Player.clas.description;
        ChoiceTitle.text = Helpers.RPSToString(Game.Player.Choice - 1);
        choiceImage.sprite = rpsSprites[(int)Game.Player.Choice - 1];
        QrCode.sprite = Game.QR.GenerateQRCode(Game.Player.ToString());
    }

    public void ToggleScanner()
    {
        if (!scannerActive)
        {
            scannerActive = true;
            qrcam.gameObject.SetActive(true);
            choiceImage.gameObject.SetActive(false);
            ChoiceTitle.gameObject.SetActive(false);
            QrCode.gameObject.SetActive(false);
            scanButtonText.text = "Terug";
            qrcam.OnQRScan += OnQRScan;
        }
        else
        {
            qrcam.OnQRScan -= OnQRScan;
            scannerActive = false;
            qrcam.gameObject.SetActive(false);
            choiceImage.gameObject.SetActive(true);
            ChoiceTitle.gameObject.SetActive(true);
            QrCode.gameObject.SetActive(true);
            scanButtonText.text = "Scan";
        }
    }

    public void EnableScanner()
    {
        qrcam.gameObject.SetActive(true);
        choiceImage.gameObject.SetActive(false);
        ChoiceTitle.gameObject.SetActive(false);
        QrCode.gameObject.SetActive(false);
        qrcam.OnQRScan += OnQRScan;
    }
    private void OnQRScan(string text)
    {
        if (debugText != null)
            debugText.text = text;
        Debug.Log(qrcam.text);
        if (battleCanvas)
        {
            Player opponent = Game.GetPlayer(text);

            if (opponent != null)
            {
                Player winner = Game.GetWinner(Game.Player, opponent);
                if (Game.Player == winner)
                {
                    showWinMessage();
                    if (Game.Player.clas.Id == 3)
                        goToRPSChoice();
                }
                else
                    goToRespawnScreen();
            }
        }
        else if (respawnCanvas)
        {
            if (Game.Player.team.Tag == text)
                goToRPSChoice();
            else if (Game.GetTeamByTag(text) != null)
                RespawnWarningText.text = "Scan de code van je eigen team.";
            else if (text.Contains("Player"))
                RespawnWarningText.text = "Je kan geen spelers meer scannen als je af bent.";
        }
    }

    private void showWinMessage()
    {
        throw new NotImplementedException();
    }

    public void goToRespawnScreen()
    {
        ShowMenu(6);
        battleCanvas = false;
        respawnCanvas = true;
        RespawnQRCode.sprite = QrCode.sprite;
        Debug.Log(Game.Player.clas);
        Debug.Log(Game.Player.clas.Id);
        if (Game.Player.clas.Id == 1)
        {
            RespawnInfoText.text = "Je bent een buidelrat. Laat je tikken door een teamgenoot om verder te spelen of ga terug naar de startplaats van jouw team.";

            BuidelratButton.gameObject.SetActive(true);
        }
        else
        {
            BuidelratButton.gameObject.SetActive(false);
            BuidelratButton.enabled = false;
            RespawnInfoText.text = "Ga terug naar de startplaats van jouw team en scan de code om verder te spelen.";
        }

        

    }
    

    public void SetTeamNumber(int id)
    {
        Game.Player.team = Game.GetTeam(id);   
        selectTeamButton.interactable = true;
        if (this.selectTeamText != null)
            this.selectTeamText.text = id == 1 ? "Team Oost" : "Team West";
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
