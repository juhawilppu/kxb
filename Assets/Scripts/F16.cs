using UnityEngine;
using System.Collections;
using System;

public class F16 : MonoBehaviour {

    public GameObject bombPrefab;

    float flyTime = 1.5f;

    Vector3 offset = new Vector3(25, 25, 0);
    Vector3 startCoordinates;
    Vector3 targetCoordinates;
    Vector3 endCoordinates;

    // Use this for initialization
    void Start () {

    }

    internal void SetTargetCoordinates(int x, int y)
    {
        targetCoordinates = new Vector3(x, y, 1);
        startCoordinates = targetCoordinates - offset;
        endCoordinates = targetCoordinates + offset;

        transform.position = startCoordinates;

        SetRotation();

        Hashtable ht = new Hashtable();
        ht.Add("time", flyTime);
        ht.Add("easetype", "linear");
        ht.Add("x", targetCoordinates.x);
        ht.Add("y", targetCoordinates.y);
        ht.Add("onComplete", "DropBomb");
        iTween.MoveTo(gameObject, ht);

    }

    void SetRotation()
    {
        if (transform.position.x > 0 && transform.localScale.x > 0)
            transform.localScale = new Vector3(transform.localScale.x * (-1), transform.localScale.y, transform.localScale.z);

        Vector3 lookPos = transform.position - targetCoordinates;

        var rotation = Quaternion.LookRotation(lookPos);
        rotation.x = 0;
        rotation.y = 0;

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);
    }

    // Update is called once per frame
    void Update () {
	
	}

    void DropBomb()
    {
        Instantiate(bombPrefab, targetCoordinates, Quaternion.identity);

        Hashtable ht = new Hashtable();
        ht.Add("time", flyTime);
        ht.Add("easetype", "linear");
        ht.Add("x", endCoordinates.x);
        ht.Add("y", endCoordinates.y);
        ht.Add("onComplete", "Destroy");
        iTween.MoveTo(gameObject, ht);
    }
}
