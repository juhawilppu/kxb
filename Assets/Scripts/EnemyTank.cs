﻿using UnityEngine;
using System.Collections;

public class EnemyTank : MonoBehaviour
{
    public GameObject explosionPrefab;
    public GameObject projectilePrefab;

    float boatSpeed = 5;

    public float shootInterval = 1f;
    public float timeUntilNextShoot = 1f;

    private Vector3 targetCoordinates;

    private bool isStopped = false;

    // Use this for initialization
    void Start()
    {
        float x = Random.Range(Map.MAX_X * -1, Map.MAX_X);
        float y = Random.Range(Map.MAX_Y * -1, Map.MAX_Y);
        targetCoordinates = new Vector3(x, y, 1);

        float startY;
        float startX;

        bool isOnX = Random.value < 0.5;

        // Randomize whether the start location is on X or Y axis
        if (isOnX)
        {
            startY = Map.OUTSIDE_Y;
            startX = Random.value * Map.OUTSIDE_X;
        } else
        {
            startX = Map.OUTSIDE_X;
            startY = Random.value * Map.OUTSIDE_Y;
        }

        // Start location should be near start location, so move them to the correct quarter
        if (x < 0)
            startX *= -1;
        if (y < 0)
            startY *= -1;

        var startCoordinates = new Vector3(startX, startY, 1);

        transform.position = startCoordinates;

        Hashtable ht = new Hashtable();
        ht.Add("time", (targetCoordinates-startCoordinates).magnitude / boatSpeed); // t = s/v
        ht.Add("easetype", "linear");
        ht.Add("x", targetCoordinates.x);
        ht.Add("y", targetCoordinates.y);
        ht.Add("onComplete", "Stop");
        iTween.MoveTo(gameObject, ht);

        SetRotation(targetCoordinates);
    }

    void Stop()
    {
        isStopped = true; // start rotation
        SetRotation(GameObject.Find("Player").transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (isStopped)
        {
            timeUntilNextShoot -= Time.deltaTime;
            if (timeUntilNextShoot <= 0)
            {
                timeUntilNextShoot = shootInterval;
                Shoot();
            }
        }
    }

    public void SetRotation(Vector3 toCoordinates)
    {
        Vector3 vectorToTarget = toCoordinates - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 360);
    }

    private void Shoot()
    {
        Vector2 point = gameObject.transform.Find("Cannon End Point").transform.position;

        // Muzzle flash
        GameObject explosion = (GameObject)Instantiate(explosionPrefab, point, Quaternion.identity);
        explosion.transform.localScale = explosion.transform.localScale / 3;

        // Cannon ball
        GameObject projectile = (GameObject)Instantiate(projectilePrefab, point, transform.rotation);
        Vector2 force = transform.right * 400;
        projectile.GetComponent<Rigidbody2D>().AddForce(force);
    }

    public void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Object.Destroy(transform.gameObject, 0.35f);
    }
}