using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    GameObject successModal;
    GameObject failedModal;
    Text remaining;

    bool isGameOn = true;

    float timeRemaining = 120;

	// Use this for initialization
	void Start () {
        successModal = GameObject.Find("Modal Success");
        failedModal = GameObject.Find("Modal Failed");
        remaining = GameObject.Find("Remaining").GetComponent<Text>();

        if (successModal != null)
        {
            successModal.SetActive(false);
            failedModal.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!isGameOn)
            return;

        timeRemaining -= Time.deltaTime;
        remaining.text = "Time remaining: " + (int)timeRemaining + " seconds";

        if (timeRemaining <= 0)
            GameWon();
	}

    public void Retry()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    internal void PlayerDied()
    {
        if (!isGameOn)
            return;

        isGameOn = false;
        failedModal.SetActive(true);
    }

    internal void GameWon()
    {
        if (!isGameOn)
            return;

        isGameOn = false;
        successModal.SetActive(true);
    }
}
