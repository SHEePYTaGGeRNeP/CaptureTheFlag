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
    public GameObject InfoStuff;
    public Button SlangButton;
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
        ShowMenu(4);
        //currentMenuScreen = 3;
    }

    public void goToRPSChoice()
    {
        respawnCanvas = false;
        battleCanvas = false;
        selectChoiceButton.interactable = false;
        ShowMenu(5);
    }

    public void goToFightScreen()
    {
        battleCanvas = true;
        SlangButton.gameObject.SetActive(false);
        InfoStuff.SetActive(true);
        ShowMenu(6);
        teamImage.sprite = Game.Player.team.image;
        classImage.sprite = Game.Player.clas.image;
        classTitle.text = Game.Player.clas.className;
        classDescription.text = Game.Player.clas.description;
        ChoiceTitle.text = Helpers.RPSToString(Game.Player.Choice);
        choiceImage.sprite = rpsSprites[(int)Game.Player.Choice - 1];
        Debug.Log("Current player: " + Game.Player);
        QrCode.sprite = Game.QR.GenerateQRCode(Game.Player.ToString());
    }

    public void goToFlagHideScreen()
    {
        ShowMenu(3);
    }
    public void ToggleScanner()
    {
        if (!scannerActive)
        {
            TurnOnScanner();
        }
        else
        {
            TurnOffScanner();
        }
    }

    private void TurnOnScanner()
    {
        scannerActive = true;
        qrcam.gameObject.SetActive(true);
        choiceImage.gameObject.SetActive(false);
        ChoiceTitle.gameObject.SetActive(false);
        QrCode.gameObject.SetActive(false);
        scanButtonText.text = "Terug";
        BuidelratButton.gameObject.SetActive(false);
        SlangButton.gameObject.SetActive(false);
        RespawnQRCode.gameObject.SetActive(false);
        qrcam.OnQRScan += OnQRScan;
    }

    private void TurnOffScanner()
    {
        qrcam.OnQRScan -= OnQRScan;
        scannerActive = false;
        qrcam.gameObject.SetActive(false);
        choiceImage.gameObject.SetActive(true);
        ChoiceTitle.gameObject.SetActive(true);
        QrCode.gameObject.SetActive(true);
        scanButtonText.text = "Scan";
        if (Game.Player.clas.Id == 1)
            BuidelratButton.gameObject.SetActive(true);
        else if (Game.Player.clas.Id == 3)
            SlangButton.gameObject.SetActive(true);
        RespawnQRCode.gameObject.SetActive(true);
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
        Debug.Log(text);
        if (battleCanvas)
        {
            Debug.Log("battle canvas");
            Player opponent = Game.GetPlayer(text);
            
            if (opponent != null)
            {
                if (opponent.team == Game.Player.team)
                    return;
                Debug.Log("opponent: " + opponent);
                TurnOffScanner();
                Player winner = Game.GetWinner(Game.Player, opponent);
                Debug.Log("Winner " + winner);
                debugText.text = "Winner " + winner;
                if (Game.Player == winner)
                {
                    showWinMessage();
                    if (Game.Player.clas.Id == 3)
                    // enable button GOTORPS
                    {
                        SlangButton.gameObject.SetActive(true);
                        InfoStuff.SetActive(false);
                    }
                }
                else
                    goToRespawnScreen();
            }
            else
            {// we scanned a different QR
                Debug.Log("opponent is null ");
                debugText.text = "opponent is null";
            }
        }
        else if (respawnCanvas)
        {
            debugText.text = "respawn canvas";
            Debug.Log("respawn canvas");
            if (Game.Player.team.Tag == text)
            {
                TurnOffScanner();
                goToRPSChoice();
            }
            else if (Game.GetTeamByTag(text) != null)
            {
               debugText.text = "Scan de code van je eigen team.";
                RespawnWarningText.text = "Scan de code van je eigen team.";
            }
            else if (text.Contains("Player"))
            {
                debugText.text = "Je kan geen spelers meer scannen als je af bent.";
                RespawnWarningText.text = "Je kan geen spelers meer scannen als je af bent.";
            }
        }
        else
        {
            Debug.Log("No active canvas");
            debugText.text = "No active canvas";
        }
    }

    private void showWinMessage()
    {
        Debug.Log("You won");
        debugText.text = "You won";
        //throw new NotImplementedException();
    }

    public void goToRespawnScreen()
    {
        debugText.text = "go to respawn";
        ShowMenu(7);
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
        else if (Game.Player.clas.Id == 3)
        {
            BuidelratButton.gameObject.SetActive(false);
            BuidelratButton.enabled = false;
        }
        else
        {
            BuidelratButton.gameObject.SetActive(false);
            SlangButton.gameObject.SetActive(false);
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
        Game.Player.Choice = (Choice)id;
        selectRPSText.text = Helpers.RPSToString(Game.Player.Choice);
    }
}
