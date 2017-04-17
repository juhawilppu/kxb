using UnityEngine;
using System.Collections;

public class TutorialManagerFactory : MonoBehaviour {

	// Use this for initialization
	void Awake () {
	    if (MenuManager.isPracticeLevel())
        {
            if (MenuManager.activeLevel == 0)
                transform.gameObject.AddComponent<TutorialManagerAirstrike>();
            else
                transform.gameObject.AddComponent<TutorialManagerCannon>();
        } else
        {
            transform.gameObject.AddComponent<TutorialManagerNoTutorial>();
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
