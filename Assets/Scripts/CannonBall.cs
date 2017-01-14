using UnityEngine;
using System.Collections;

public class CannonBall : MonoBehaviour {

    public GameObject explosionPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
            coll.gameObject.GetComponent<Player>().ReceiveDamage(Map.DAMAGE_PER_HIT);

        GameObject explosion = (GameObject)Instantiate(explosionPrefab, coll.contacts[0].point, Quaternion.identity);
        explosion.transform.localScale = Vector3.one * 0.1f;

        Object.Destroy(gameObject);
    }
}
