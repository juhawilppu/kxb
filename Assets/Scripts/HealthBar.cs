using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

    RectTransform rectTransform;
    float maxWidth = 0;
    float height;
	
    // Use this for initialization
	void Start () {

        rectTransform = gameObject.GetComponent<RectTransform>();
        maxWidth = rectTransform.rect.width;
        height = rectTransform.rect.height;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetHealth(float health)
    {
        rectTransform.sizeDelta = new Vector2(health / 100 * maxWidth, height);
    }
}
