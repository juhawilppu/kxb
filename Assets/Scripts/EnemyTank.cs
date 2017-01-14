using UnityEngine;
using System.Collections;

public class EnemyTank : MonoBehaviour
{
    public GameObject explosionPrefab;
    public GameObject projectilePrefab;

    public float shootInterval = 7f;
    public float timeUntilNextShoot = 7f;

    // Use this for initialization
    void Start()
    {
        float x = Random.Range(Map.MAX_X * -1, Map.MAX_X);
        float y = Random.Range(Map.MAX_Y * -1, Map.MAX_Y);

        transform.position = new Vector3(x, y, 1);
    }

    // Update is called once per frame
    void Update()
    {
        timeUntilNextShoot -= Time.deltaTime;
        if (timeUntilNextShoot <= 0)
        {
            timeUntilNextShoot = shootInterval;
            Shoot();
        }

       if (transform.position.x > 0 && transform.localScale.x > 0)
            transform.localScale = new Vector3(transform.localScale.x * (-1), transform.localScale.y, transform.localScale.z);

        Transform target = GameObject.Find("Player").transform;

        Vector3 lookPos = transform.position - target.position;

        var rotation = Quaternion.LookRotation(lookPos);
        rotation.x = 0;
        rotation.y = 0;

        float damping = 5f;
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
    }

    private void Shoot()
    {
        Vector2 point = gameObject.transform.Find("Cannon End Point").transform.position;

        GameObject projectile = (GameObject)Instantiate(projectilePrefab, point, transform.rotation);

        Vector2 force = transform.right * 400;
        if (transform.position.x > 0)
            force *= -1;

        projectile.GetComponent<Rigidbody2D>().AddForce(force);
    }

    public void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Object.Destroy(transform.gameObject, 0.35f);
    }
}