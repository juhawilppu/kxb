using UnityEngine;
using System.Collections;
using System;

public class F16 : MonoBehaviour {

    public GameObject bombPrefab;

    float flySpeed = 20f;

    float minY = -9;
    float maxY = 9;

    float minX = -9;
    float maxX = 9;

    Vector3 startCoordinates;
    Vector3 targetCoordinates;
    Vector3 endCoordinates;

    // Use this for initialization
    void Start () {

    }

    internal void SetTargetCoordinates(int x, int y)
    {
        targetCoordinates = new Vector3(x, y, 1);

        var distanceToBottom = y - minY;
        var distanceToLeft = x - minX;
        var distance1 = Mathf.Min(distanceToBottom, distanceToLeft);
        var bottomOffset = new Vector3(distance1, distance1, 0);

        var distanceToTop = maxY - y;
        var distanceToRight = maxX - x;
        var distance2 = Mathf.Min(distanceToTop, distanceToRight);
        var topOffset = new Vector3(distance2, distance2, 0);

        startCoordinates = targetCoordinates - bottomOffset;
        endCoordinates = targetCoordinates + topOffset;

        transform.position = startCoordinates;

        SetRotation();

        Hashtable ht = new Hashtable();
        ht.Add("time", bottomOffset.magnitude / flySpeed); // t = s/v
        ht.Add("easetype", "linear");
        ht.Add("x", targetCoordinates.x);
        ht.Add("y", targetCoordinates.y);
        ht.Add("onComplete", "DropBomb");
        iTween.MoveTo(gameObject, ht);

    }

    void SetRotation()
    {
        //if (transform.position.x > 0 && transform.localScale.x > 0)
        //    transform.localScale = new Vector3(transform.localScale.x * (-1), transform.localScale.y, transform.localScale.z);

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
        ht.Add("time", (targetCoordinates-endCoordinates).magnitude/flySpeed); // t = s/v
        ht.Add("easetype", "linear");
        ht.Add("x", endCoordinates.x);
        ht.Add("y", endCoordinates.y);
        ht.Add("onComplete", "Destroy");
        iTween.MoveTo(gameObject, ht);
    }

    void Destroy()
    {
        UnityEngine.Object.Destroy(gameObject);
    }
}
