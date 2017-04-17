using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {

    public GameObject explosionPrefab;

	// Use this for initialization
	void Start () {
        Hashtable ht = new Hashtable();
        ht.Add("time", 1f);
        ht.Add("amount", new Vector3(0.15f, 0.15f, 0));
        ht.Add("onComplete", "Explode");
        iTween.ShakeScale(gameObject, ht);
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


        var gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        if (hitTank)
        {
            gameManager.NextRound();
        }
        else
        {
            gameManager.PlayerMissed();
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        Object.Destroy(transform.gameObject, 0.35f);
    }
}
