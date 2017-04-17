using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public List<MenuItem> menuItems;

    // Level that player is playing now
    public static int activeLevel = 0;

    // Biggest level which player has unlocked
    public static int currentLevel = 0;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < menuItems.Count; i++)
        {
            MenuItem item = menuItems[i];

            if (i == currentLevel)
            {
                //GameObject.Find("Main Camera").transform.position = item.transform.position;
            }
            item.SetEnabled(i, currentLevel);
        }
	}

    public static bool isCannonEnabled()
    {
        return currentLevel >= 2;
    }

    public static bool isPracticeLevel()
    {
        return activeLevel == 0 || activeLevel == 2;
    }
	
    public static void Passed()
    {
        if (activeLevel == currentLevel)
            currentLevel = activeLevel + 1;
    }

	// Update is called once per frame
	void Update () {
	
	}

    public static void StartLevel(int index)
    {
        activeLevel = index;
        SceneManager.LoadScene("Scenes/map", LoadSceneMode.Single);
    }
}
