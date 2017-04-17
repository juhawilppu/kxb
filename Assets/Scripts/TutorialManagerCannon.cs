using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialManagerCannon : TutorialManager
{
    Transform text;
    private int textIndex = -1;
    List<Sprite> textSprites;

    Transform arrowK;
    Transform arrowB;

    Transform arrowFireCannon;

    Transform ui;
    Transform gray;
    Transform cannonK;
    Transform cannonB;
    Transform fireCannon;

    void Start()
    {
        textSprites = new List<Sprite>();
        for (int i = 0; i <= 7; i++)
        {
            textSprites.Add(Resources.Load<Sprite>("tutorial_speech_cannon_" + i));
        }
        text = transform.Find("Face/Text");

        ui = GameObject.Find("Cannon Controller").transform;
        gray = GameObject.Find("UI/Foreground Gray").transform;
        cannonK = GameObject.Find("Cannon k PlusMinus").transform;
        cannonB = GameObject.Find("Cannon b PlusMinus").transform;
        arrowK = GameObject.Find("Arrow k Cannon").transform;
        arrowB = GameObject.Find("Arrow b Cannon").transform;
        arrowFireCannon = GameObject.Find("Arrow Fire Cannon").transform;
        fireCannon = GameObject.Find("Fire Cannon Button").transform;

        HideArrows();

        Next();
    }

    public override void setBombCoordinates(int x, int y)
    {
    }

    public override void setCannonCoordinates(float k, int b)
    {
        if (textIndex == 1 && k == 2)
        {
            Next();
        }
        else if (textIndex == 4 && b == 2)
        {
            Next();
        }
        else if (textIndex == 5 && k == -1)
        {
            Next();
        }
    }

    public override Vector2 getNextEnemy()
    {
        if (textIndex == 0)
        {
            return new Vector2(2, 4);
        }
        else
        {
            return new Vector2(2, 0);
        }
    }

    public override void Next()
    {
        textIndex++;

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
        } else if (textIndex == 1)
        {
            // Set slope
            cannonK.parent = gray;
            arrowK.gameObject.SetActive(true);
            isWaitingForAction = true;
        }
        else if (textIndex == 2)
        {
            // Shoot
            cannonK.parent = ui;
            arrowK.gameObject.SetActive(false);

            fireCannon.parent = gray;
            arrowFireCannon.gameObject.SetActive(true);
            isWaitingForAction = true;
        }
        else if (textIndex == 3)
        {
            // Superb
            fireCannon.parent = ui;
            arrowFireCannon.gameObject.SetActive(false);

            Invoke("Next", 2f);
        }
        else if (textIndex == 4)
        {
            // Set constant
            cannonB.parent = gray;
            arrowB.gameObject.SetActive(true);
            arrowB.GetComponent<Arrow>().ResetPosition();
        }
        else if (textIndex == 5)
        {
            // Set slope
            cannonB.parent = ui;
            arrowB.gameObject.SetActive(false);

            cannonK.parent = gray;
            arrowK.gameObject.SetActive(true);
            arrowK.GetComponent<Arrow>().ResetPosition();
        }
        else if(textIndex == 6)
        {
            // Shoot!
            cannonK.parent = ui;
            arrowK.gameObject.SetActive(false);

            fireCannon.parent = gray;
            arrowFireCannon.gameObject.SetActive(true);
            isWaitingForAction = true;
        } else if (textIndex == 7)
        {
            // Done
            fireCannon.parent = ui;
            arrowFireCannon.gameObject.SetActive(false);

            isWaitingForAction = false;
            Invoke("Next", 10f); // In case user does not click herself
        }
    }
}