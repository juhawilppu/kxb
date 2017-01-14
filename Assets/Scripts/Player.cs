using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour {

    float health = 100;

    public GameObject F16Prefab;
    HealthBar healthBar;

    // Use this for initialization
    void Start () {
        healthBar = GameObject.Find("Health Bar").GetComponent<HealthBar>();
        ReceiveDamage(0); // To update health bar
	}
	
	// Update is called once per frame
	void Update () {	
	}

    public void DropBomb(int x, int y)
    {
        GameObject f16Object = Instantiate(F16Prefab, new Vector2(0, 0), Quaternion.identity) as GameObject;
        f16Object.transform.Find("F16").GetComponent<F16>().SetTargetCoordinates(x, y);
    }

    internal void Shoot(Vector3 start, Vector3 end)
    {
        Vector3 direction = end - start;

        Debug.Log("start " + start);
        Debug.Log("end   " + end);
        Debug.Log("Shoot");

        RaycastHit[] hitInfo = Physics.RaycastAll(start, direction);

        foreach(RaycastHit hit in hitInfo) {
            Debug.Log("Hit");
            hit.collider.transform.gameObject.GetComponent<EnemyTank>().Explode();
            ReceiveDamage(-10);
        }
    }

    public void ReceiveDamage(float damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, 100);
        healthBar.SetHealth(health);

        if (health == 0)
        {
            GameObject.Find("Game Manager").GetComponent<GameManager>().PlayerDied();
        }
    }
}
