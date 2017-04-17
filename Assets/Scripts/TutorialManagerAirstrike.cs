using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialManagerAirstrike : TutorialManager
{
    List<Sprite> textSprites;

    Transform text;
    public int textIndex = -1;

    Transform ui;
    Transform arrowX;
    Transform arrowY;
    Transform arrowDropBomb;
    Transform dropBomb;
    Transform gray;
    Transform airstrikeX;
    Transform airstrikeY;

    // Use this for initialization
    void Start()
    {
        textSprites = new List<Sprite>();
        for (int i = 1; i <= 8; i++)
        {
           textSprites.Add(Resources.Load<Sprite>("tutorial_speech_" + i));
        }

        text = transform.Find("Face/Text");
        gray = GameObject.Find("UI/Foreground Gray").transform;
        ui = GameObject.Find("Airstrike Controller").transform;
        airstrikeX = GameObject.Find("Airstrike x PlusMinus").transform;
        airstrikeY = GameObject.Find("Airstrike y PlusMinus").transform;
        arrowX = GameObject.Find("Arrow X").transform;
        arrowY = GameObject.Find("Arrow Y").transform;
        arrowDropBomb = GameObject.Find("Arrow Drop Bomb").transform;
        dropBomb = GameObject.Find("Drop Bomb Button").transform;

        HideArrows();

        Next();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseUp()
    {
        if (isWaitingForAction)
            return;

        Next();
    }

    public override void setBombCoordinates(int x, int y)
    {
        if (textIndex == 1 && x == -3)
        {
            Next();
        }
        else if (textIndex == 2 && y == 2)
        {
            Next();
        }
        else if (textIndex == 5 && x == 1 && y == 4)
        {
            Next();
        }
    }

    public override void setCannonCoordinates(float k, int b)
    {

    }


    public override Vector2 getNextEnemy()
    {
        if (textIndex == 0)
        {
            return new Vector2(-3, 2);
        }
        else
        {
            return new Vector2(1, 4);
        }
    }

    public override void Next()
    {
        textIndex++;
        Debug.Log("Next here " + textIndex);
        Debug.Log("sprites " + textSprites.Count);

        if (textIndex == textSprites.Count)
        {
            MenuManager.Passed();
            GameObject.Find("Game Manager").GetComponent<GameManager>().Menu();
            isWaitingForAction = true;
            return;
        }

        text.GetComponent<SpriteRenderer>().sprite = textSprites[textIndex];

        if (textIndex == 0)
        {
            // Welcome
        }
        else if (textIndex == 1)
        {
            // Set x
            airstrikeX.parent = gray;
            arrowX.gameObject.SetActive(true);
            isWaitingForAction = true;
        }
        else if (textIndex == 2)
        {
            // Set y
            airstrikeX.parent = ui;
            airstrikeY.parent = gray;
            arrowX.gameObject.SetActive(false);
            arrowY.gameObject.SetActive(true);
            isWaitingForAction = true;
        }
        else if (textIndex == 3)
        {
            // Drop bomp
            airstrikeY.parent = ui;
            dropBomb.parent = gray;
            arrowY.gameObject.SetActive(false);
            arrowDropBomb.gameObject.SetActive(true);
            isWaitingForAction = true;
        }
        else if (textIndex == 4)
        {
            // There it goes
            dropBomb.parent = ui;
            arrowDropBomb.gameObject.SetActive(false);
            isWaitingForAction = true;
            Invoke("Next", 2);
        }
        else if (textIndex == 5)
        {
            // Put x == 1, y == 4
            airstrikeX.parent = gray;
            airstrikeY.parent = gray;
            arrowX.gameObject.SetActive(true);
            arrowY.gameObject.SetActive(true);
            arrowX.GetComponent<Arrow>().ResetPosition();
            arrowY.GetComponent<Arrow>().ResetPosition();
            //arrowX.GetComponent<Arrow>().ScaleUp();
            //arrowY.GetComponent<Arrow>().ScaleUp();
            isWaitingForAction = true;
        }
        else if (textIndex == 6)
        {
            // Drop bomb
            airstrikeX.parent = ui;
            airstrikeY.parent = ui;
            arrowX.gameObject.SetActive(false);
            arrowY.gameObject.SetActive(false);

            arrowDropBomb.gameObject.SetActive(true);
            dropBomb.parent = gray;
            isWaitingForAction = true;
        }
        else if (textIndex == 7)
        {
            // Completed
            arrowDropBomb.gameObject.SetActive(false);
            dropBomb.parent = ui;

            isWaitingForAction = false;

            Invoke("Next", 10f); // In case user does not click herself
        }
    }
}
