using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

    public Vector3 anglePerSecond;

    // Define on class level so that no more memory is needed during runtime
    Vector3 timeScaled;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        timeScaled = anglePerSecond * Time.deltaTime;
        gameObject.transform.Rotate(timeScaled);
    }
}