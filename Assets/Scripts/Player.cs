using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour {

    public GameObject explosionPrefab;

    float health = 100;

    public GameObject F16Prefab;
    HealthBar healthBar;

    float BOAT_SPEED = 6;

    bool allowShooting = true;

    public Vector3 moveToCoordinates { get; set; }

    GameManager gameManager;

    // Use this for initialization
    void Start () {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        healthBar = GameObject.Find("Health Bar").GetComponent<HealthBar>();
        ReceiveDamage(0); // To update health bar
        moveToCoordinates = Vector3.zero;            
	}
	
	// Update is called once per frame
	void Update () {

    }

    void Move()
    {
        allowShooting = false;

        GameObject.Find("Cannon Controller").GetComponent<CannonController>().DrawNumbers();

        int newY = (int)Mathf.Round(Map.PLAYER_MAX_Y - 2 * UnityEngine.Random.value * Map.PLAYER_MAX_Y);

        moveToCoordinates = new Vector3(0, newY, transform.position.z);

        Hashtable ht = new Hashtable();
        ht.Add("time", (moveToCoordinates - transform.position).magnitude / BOAT_SPEED); // t = s/v
        ht.Add("easetype", "linear");
        //ht.Add("x", moveToCoordinates.x);
        ht.Add("y", moveToCoordinates.y);
        ht.Add("onComplete", "Stop");
        iTween.MoveTo(gameObject, ht);
    }

    void Stop()
    {
        allowShooting = true;
    }

    public void DropBomb(int x, int y)
    {
        GameObject f16Object = Instantiate(F16Prefab, new Vector2(0, 0), Quaternion.identity) as GameObject;
        f16Object.transform.Find("F16").GetComponent<F16>().SetTargetCoordinates(x, y);
    }

    internal bool Shoot(Vector3 start, Vector3 end)
    {
        if (!allowShooting)
            return false;

        Vector3 direction = end - start;

        RaycastHit[] hitInfo = Physics.RaycastAll(start, direction);

        bool hitEnemy = false;

        foreach(RaycastHit hit in hitInfo) {
            hit.collider.transform.gameObject.GetComponent<EnemyTank>().Explode();
            ReceiveDamage(-10); // Receive health for successful hit
            hitEnemy = true;
        }

        if (hitEnemy)
        {
            // Muzzle flash
            GameObject explosion1 = (GameObject)Instantiate(explosionPrefab, transform.Find("Barrel Position 1").position, Quaternion.identity);
            explosion1.transform.localScale = explosion1.transform.localScale / 3;

            // Muzzle flash
            GameObject explosion2 = (GameObject)Instantiate(explosionPrefab, transform.Find("Barrel Position 2").position, Quaternion.identity);
            explosion2.transform.localScale = explosion2.transform.localScale / 3;

            StartCoroutine(ExecuteAfterTime(1));

        } else
        {
            gameManager.PlayerMissed();
        }


        return hitEnemy;
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        Move();
        gameManager.NextRound();
    }

    public void SetRotation(Vector3 toCoordinates)
    {
        Vector3 vectorToTarget = toCoordinates - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 360);
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
