using UnityEngine;
using System.Collections;

public class MenuItem : MonoBehaviour {

    private int levelIndex;
    public Sprite activeButton;
    public Sprite disabledButton;
    public Sprite unknownText;

    Transform text;
    Transform button;
    float currentScale;

    bool isClickable;

    private bool isMouseOver = false;

    // Use this for initialization
    void Awake () {
        text = transform.Find("Menu Text");
        button = transform.Find("Menu Button");
        currentScale = button.localScale.x;
    }

    void OnMouseDown()
    {
        if (!isClickable)
            return;

        isMouseOver = true;
        iTween.Stop(button.gameObject);
        iTween.ScaleTo(button.gameObject, iTween.Hash(
            "scale", new Vector3(currentScale * 1.5f, currentScale * 1.5f, currentScale * 1.5f),
            "time", 0.1f
        ));
    }

    void OnMouseExit()
    {
        if (!isClickable)
            return;

        if (isMouseOver)
        {
            isMouseOver = false;
            ScaleDown();
        }
    }

    void OnMouseUp()
    {
        if (!isClickable || !isMouseOver)
            return;

        MenuManager.StartLevel(levelIndex);
    }

    public void SetEnabled(int thisLevel, int currentLevel)
    {
        levelIndex = thisLevel;

        if (thisLevel < currentLevel)
        {
            isClickable = true;
            iTween.Stop(button.gameObject);
        }
        else if (thisLevel == currentLevel)
        {
            ScaleUp();
            isClickable = true;
        }
        else if (thisLevel > currentLevel)
        {
            SetDisabled();
            isClickable = false;
        }
    }

    public void SetDisabled()
    {
        text.GetComponent<SpriteRenderer>().sprite = unknownText;
        button.GetComponent<SpriteRenderer>().sprite = disabledButton;
    }
	
    void ScaleUp()
    {
        iTween.ScaleTo(button.gameObject, iTween.Hash(
            "scale", new Vector3(currentScale*1.2f, currentScale*1.2f, currentScale*1.2f),
            "time", 0.4f,
            "easeType", iTween.EaseType.easeInOutBounce,
            "oncomplete", "ScaleDown",
            "oncompletetarget", this.gameObject           
        ));
    }

    void ScaleDown()
    {
        iTween.ScaleTo(button.gameObject, iTween.Hash(
            "scale", new Vector3(currentScale, currentScale, currentScale),
            "time", 0.4f,
            "easeType", iTween.EaseType.easeInOutBounce,
            "oncomplete", "ScaleUp",
            "oncompletetarget", this.gameObject
        ));
    }

    // Update is called once per frame
    void Update () {
	
	}
}
