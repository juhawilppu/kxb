using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class TutorialManager : MonoBehaviour {

    protected bool isWaitingForAction = false;

    void OnMouseUp()
    {
        if (isWaitingForAction)
            return;

        Next();
    }

    protected void HideArrows()
    {
        GameObject.Find("Arrow X").gameObject.SetActive(false);
        GameObject.Find("Arrow Y").gameObject.SetActive(false);
        GameObject.Find("Arrow Drop Bomb").gameObject.SetActive(false);

        GameObject.Find("Arrow k Cannon").gameObject.SetActive(false);
        GameObject.Find("Arrow b Cannon").gameObject.SetActive(false);
        GameObject.Find("Arrow Fire Cannon").gameObject.SetActive(false);
    }

    public abstract void setBombCoordinates(int x, int y);

    public abstract void setCannonCoordinates(float k, int b);

    public abstract Vector2 getNextEnemy();

    public abstract void Next();

}
