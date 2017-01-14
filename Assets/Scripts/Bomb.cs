using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {

    public GameObject explosionPrefab;
    float explosionDuration = 0.4f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (explosionDuration < 0)
            return;

        explosionDuration -= Time.deltaTime;

        if (explosionDuration < 0)
        {
            Explode();
        }
	}

    void Explode()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Enemy");

        bool hitTank = false;
        
        foreach (GameObject obj in objs)
        {
            // Enemy tank is in same position as bomb
            float distance = (obj.transform.position - transform.position).magnitude;
            bool hitsTank = distance < 0.1;

            if (hitsTank)
            {
                obj.GetComponent<EnemyTank>().Explode();
                hitTank = true;
            }
        }

        if (!hitTank)
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        Object.Destroy(transform.gameObject, 0.35f);
    }
}
