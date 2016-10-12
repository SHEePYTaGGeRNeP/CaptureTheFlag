using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public List<Team> Teams;
    public List<Class> Classes;
    public Player Player;
    public UIManager UI;
    public QRManager QR;
   
    // Use this for initialization
    void Start()
    {
        UI = transform.GetComponent<UIManager>();
        Player = transform.GetComponent<Player>();
        QR = transform.GetComponent<QRManager>();
        UI.HideMenus();
        UI.ShowMenu(0);
    }

    public Player GetPlayer(string qrCode)
    {
        Player player;
        if (qrCode != null)
            player = new Player(qrCode);
        return Player;
    }

    









   

  

    public Team GetTeam(long teamId)
    {
        foreach (Team team in Teams)
        {
            if (team.Id == teamId)
                return team;
        }
        return null;
    }

    public Class GetClass(long classId)
    {
        foreach (Class clas in Classes)
        {
            if (clas.Id == classId)
                return clas;
        }
        return null;
    }

    public Player GetWinner(Player player, Player oponent)
    {
        int compareInt = player.CompareTo(oponent);
        switch (compareInt)
        {
            case -1:
                return player;
            case 0:
                return null;
            case 1:
                return oponent;
            default:
                return null;
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
        if (Player.clas.Id == 3) // slang
        {
            // this.snakeText.text =
            //     String.Format(
            //         "Wil je <color=#00ffffff>{0}</color> behouden of veranderen naar <color=#00ffffff>{1}</color>",
            //         Helpers.RPSToString(selectedRPS), Helpers.RPSToString(Helpers.LosesOf(selectedRPS)));
            // snakePanel.SetActive(true);
        }
        else
        {
            UI.goToTitleMenu();
        }
        //BackToSelection();
    }

    public void SnakeKeep()
    {
        //BackToSelection();
    }

    public void SnakeSwitch()
    {
        SetEatenElement();
        //BackToSelection();
    }

    private void SetEatenElement()
    {
        //selectedRPS = Helpers.LosesOf(selectedRPS);
    }

    public void Lost()
    {
        //possumReviveButton.gameObject.SetActive(Player.clas.Id == 0);
        //lostPanel.SetActive(true);
    }

    public void Respawn()
    {

        //lostPanel.SetActive(false);
        //BackToSelection();
    }

    public void RespawnPossum()
    {
        //lostPanel.SetActive(false);
        //BackToSelection();
    }
}
