using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialManagerNoTutorial : TutorialManager
{
    // Use this for initialization
    void Start()
    {
        foreach(var o in GameObject.FindGameObjectsWithTag("Tutorial"))
        {
            o.SetActive(false);
        }
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

    }

    public override void setCannonCoordinates(float k, int b)
    {

    }


    public override Vector2 getNextEnemy()
    {
        return Vector2.zero;
    }

    public override void Next()
    {

    }

}
