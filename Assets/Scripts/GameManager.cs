using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    GameObject successModal;
    GameObject failedModal;
    Text roundText;

    public int round { get; set; }

    bool isGameOn = true;

    int LAST_ROUND = 1;

    EnemyManager enemyManager;

	// Use this for initialization
	void Start () {
        round = 0;

        successModal = GameObject.Find("Modal Success");
        failedModal = GameObject.Find("Modal Failed");
        roundText = GameObject.Find("Remaining").GetComponent<Text>();
        enemyManager = GameObject.Find("Enemy Manager").GetComponent<EnemyManager>();

        if (successModal != null)
        {
            successModal.SetActive(false);
            failedModal.SetActive(false);
        }

        NextRound();
    }
	
    public void NextRound()
    {
        round++;
        roundText.text = "Round: " + round;

        if (round == LAST_ROUND + 1)
            GameWon();
        else
            enemyManager.NextEnemies();
    }

    public void PlayerMissed()
    {
        var objects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject obj in objects)
        {
            obj.GetComponent<EnemyTank>().Shoot();
        }
    }

    public void Retry()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    public void Menu()
    {
        SceneManager.LoadScene("Scenes/menu", LoadSceneMode.Single);
    }

    internal void PlayerDied()
    {
        if (!isGameOn || failedModal == null)
            return;

        isGameOn = false;
        failedModal.SetActive(true);
    }

    internal void GameWon()
    {
        if (!isGameOn)
            return;

        MenuManager.Passed();
        isGameOn = false;
        successModal.SetActive(true);
    }
}
