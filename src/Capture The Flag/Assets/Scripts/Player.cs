﻿using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;

public class Player : MonoBehaviour, IComparable<Player>
{
    public long Id;
    public string playerName;
    public Team team;
    public Class clas;
    public Choice Choice;
    private GameManager gameManager;
    private List<Player> wins;
    private List<Player> looses;


    public Player(String playerString)
    {
        string[] playerStringSplit = playerString.Split(":".ToCharArray());
        Id = Int64.Parse(playerStringSplit[0]);
        playerName = playerStringSplit[1];
        long teamId = Int64.Parse(playerStringSplit[2]);
        team = gameManager.GetTeam(teamId);
        long classId = Int64.Parse(playerStringSplit[3]);
        clas = gameManager.GetClass(classId);
        Choice = (Choice) Enum.Parse(typeof(Choice), playerStringSplit[4]);


    }
	// Use this for initialization
	void Start ()
	{
	    GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public int CompareTo(Player other)
    {
        switch (Choice)
        {
            case Choice.Rock:
                switch (other.Choice)
                {
                    case Choice.Rock:
                        return 0;
                    case Choice.Paper:
                        return 1;
                    case Choice.Scissors:
                        return -1;
                }
                break;
            case Choice.Paper:
                switch (other.Choice)
                {
                    case Choice.Rock:
                        return -1;
                    case Choice.Paper:
                        return 0;
                    case Choice.Scissors:
                        return 1;
                }
                break;
            case Choice.Scissors:
                switch (other.Choice)
                {
                    case Choice.Rock:
                        return 1;
                    case Choice.Paper:
                        return -1;
                    case Choice.Scissors:
                        return 0;
                }
                break;
        }
        return 1;
    }

    public override string ToString()
    {
        return Id + ":" + playerName + ":" + team.Id + ":" + clas.Id + ":" + Choice;
    }

}
