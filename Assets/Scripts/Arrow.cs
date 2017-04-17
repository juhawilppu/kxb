using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

    // Use this for initialization
    Vector3 originalPosition;
    Vector3 offset = new Vector3(0, 4, 0);

	void Start () {
        originalPosition = transform.position;
        ScaleUp();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ResetPosition()
    {
        iTween.Stop(this.gameObject);
        transform.position = originalPosition;
    }

    public void ScaleUp()
    {
        iTween.MoveTo(this.gameObject, iTween.Hash(
            "position", originalPosition + offset,
            "time", 0.4f,
            "easeType", iTween.EaseType.easeInOutBounce,
            "oncomplete", "ScaleDown",
            "oncompletetarget", this.gameObject
        ));
    }

    void ScaleDown()
    {
        iTween.MoveTo(transform.gameObject, iTween.Hash(
            "position", originalPosition,
            "time", 0.4f,
            "easeType", iTween.EaseType.easeInOutBounce,
            "oncomplete", "ScaleUp",
            "oncompletetarget", this.gameObject
        ));
    }
}
