using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class BombController : MonoBehaviour {

    Player player;
    GameObject bombTarget;

    public enum Change
    {
        INCREASE, DECREASE
    }

    public enum Numbers
    {
        X, Y
    }

    Text textX;
    Text textY;

    int bombX = 0;
    int bombY = 0;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").GetComponent<Player>();
        bombTarget = GameObject.Find("Bomb Target");
        SetBombTargetVisible(false);

        textX = GameObject.Find("x").GetComponent<Text>();
        textY = GameObject.Find("y").GetComponent<Text>();

        DrawNumbers();
        SetBombTargetVisible(false);
    }

    void DrawNumbers()
    {
        textX.text = bombX + "";
        textY.text = bombY + "";

        // Cancel existing tweens
        iTween.Stop(bombTarget);
        iTween.FadeTo(bombTarget, 1, 0);

        // Set new tweens
        var ht = new Hashtable();
        ht.Add("alpha", 0);
        ht.Add("delay", 1.5);
        ht.Add("time", 0.25);
        iTween.FadeTo(bombTarget, ht);
        iTween.MoveTo(bombTarget, new Vector2(bombX, bombY), 0.25f);
    }

    public void ReceiveMessage(string message)
    {
        string[] splitted = message.Split(' ');
        int number;

        if (splitted[0] == "x")
            number = bombX;
        else
            number = bombY;

        if (splitted[1] == "+")
            number++;
        else
            number--;

        if (splitted[0] == "x")
            bombX = number;
        else
            bombY = number;

        bombX = Mathf.Clamp(bombX, Map.MAX_X * -1, Map.MAX_X);
        bombY = Mathf.Clamp(bombY, Map.MAX_Y * -1, Map.MAX_Y);

        DrawNumbers();
    }

    public void DropBomb()
    {
        SetBombTargetVisible(false);
        player.DropBomb(bombX, bombY);
    }

    private void SetBombTargetVisible(bool isVisible)
    {
        iTween.Stop(bombTarget);
        iTween.FadeTo(bombTarget, isVisible ? 1 : 0, 0);
    }
}
