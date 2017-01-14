using UnityEngine;
using System.Collections;

public class HideThis : MonoBehaviour
{

    public float hideAfterSeconds = 3;
    public float startFadingAfterSeconds = 3;
    private float fadeDuration;

    // Use this for initialization
    void Start()
    {
        fadeDuration = hideAfterSeconds - startFadingAfterSeconds;
    }

    // Update is called once per frame
    void Update()
    {
        startFadingAfterSeconds -= Time.deltaTime;

        if (startFadingAfterSeconds < 0)
        {
            iTween.ValueTo(gameObject, iTween.Hash(
            "from", 1.0f, "to", 0.0f,
            "time", fadeDuration, "easetype", "linear",
            "onupdate", "setAlpha"));

            startFadingAfterSeconds = 200000; // really smart...
        }
    }

    public void setAlpha(float newAlpha)
    {
        foreach (Material mObj in gameObject.GetComponent<Renderer>().materials)
        {
            mObj.color = new Color(
                mObj.color.r, mObj.color.g,
                mObj.color.b, newAlpha);
        }

        if(newAlpha==0)
        {
            Destroy(this);
        }
    }
}
