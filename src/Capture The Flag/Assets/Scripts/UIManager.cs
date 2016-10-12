using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Assets.Scripts;

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
    public Text debugText;

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

    private void OnQRScan(string text)
    {
        if (debugText != null)
            debugText.text = text;
        Debug.Log(qrcam.text);
        if (battleCanvas)
        {
            // scanning of other player's RPS
        }
        else if (respawnCanvas)
        {
            // Scanning of home base
            // check own team
        }
    }

    public void goToRespawnScreen()
    {
        battleCanvas = false;
        respawnCanvas = true;

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
