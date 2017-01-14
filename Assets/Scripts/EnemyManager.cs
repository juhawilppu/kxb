using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

    public GameObject enemyTankPrefab;
    float timeSinceLastTank = 0;

    float tankInterval = 6;
    float tanksAtStart = 1;

    float maxEnemies = 3;

	// Use this for initialization
	void Start () {
	    for (int i=0; i < tanksAtStart; i++)
        {
            Instantiate(enemyTankPrefab, new Vector2(0, 0), Quaternion.identity);
            timeSinceLastTank = tankInterval;
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

        timeSinceLastTank -= Time.deltaTime;

        if (timeSinceLastTank < 0)
        {
            Instantiate(enemyTankPrefab, new Vector2(0, 0), Quaternion.identity);
            timeSinceLastTank = tankInterval;
        }
    }
}
