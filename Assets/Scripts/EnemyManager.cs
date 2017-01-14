using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

    public GameObject enemyTankPrefab;

    float enemySpawnInterval = 8;
    float timeSinceLastSpawnedEnemy = 8;

    float enemiesAtStart = 1;
    float maxEnemies = 3;

	// Use this for initialization
	void Start () {
	    for (int i=0; i < enemiesAtStart; i++)
        {
            Instantiate(enemyTankPrefab, new Vector2(0, 0), Quaternion.identity);
            timeSinceLastSpawnedEnemy = enemySpawnInterval;
        }
	}
	
    int getEnemyCount()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

	// Update is called once per frame
	void Update () {
        if (getEnemyCount() >= maxEnemies)
            return;

        timeSinceLastSpawnedEnemy -= Time.deltaTime;

        if (timeSinceLastSpawnedEnemy < 0)
        {
            Instantiate(enemyTankPrefab, new Vector2(0, 0), Quaternion.identity);
            timeSinceLastSpawnedEnemy = enemySpawnInterval;
        }
    }
}
