using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

    // Use this for initialization
    float currentScale;
    Vector3 currentPosition;
    public Vector3 offset = new Vector3(0, 4, 0);

	void Start () {
        currentScale = transform.localScale.x;
        currentPosition = transform.position;
        ScaleUp();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void ScaleUp()
    {
        /*iTween.ScaleTo(transform.gameObject, iTween.Hash(
            "scale", new Vector3(currentScale * 1.1f, currentScale * 1.1f, currentScale * 1.1f),
            "time", 0.4f,
            "easeType", iTween.EaseType.easeInOutBounce,
            "oncomplete", "ScaleDown",
            "oncompletetarget", this.gameObject
        ));*/
        iTween.MoveTo(transform.gameObject, iTween.Hash(
            "position", currentPosition + offset,
            "time", 0.4f,
            "easeType", iTween.EaseType.easeInOutBounce,
            "oncomplete", "ScaleDown",
            "oncompletetarget", this.gameObject
        ));
    }

    void ScaleDown()
    {
        /*iTween.ScaleTo(transform.gameObject, iTween.Hash(
            "scale", new Vector3(currentScale, currentScale, currentScale),
            "time", 0.4f,
            "easeType", iTween.EaseType.easeInOutBounce,
            "oncomplete", "ScaleUp",
            "oncompletetarget", this.gameObject
        ));*/
        iTween.MoveTo(transform.gameObject, iTween.Hash(
            "position", currentPosition,
            "time", 0.4f,
            "easeType", iTween.EaseType.easeInOutBounce,
            "oncomplete", "ScaleUp",
            "oncompletetarget", this.gameObject
        ));
    }
}
