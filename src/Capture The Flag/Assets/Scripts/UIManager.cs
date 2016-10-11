using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{

    public List<GameObject> menus;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool ShowMenu(int index)
    {
        return ShowMenu(Menus[index], true);
    }

    public bool ShowMenu(string name)
    {
        GameObject menu;
        foreach( GameObject m in Menus)
        {
            if (m.name == name)
                 menu = m;
        }   
        return ShowMenu(menu, true);
    }

    public bool ShowMenu(GameObject menu, bool hideOther)
    {
        
    }
}
