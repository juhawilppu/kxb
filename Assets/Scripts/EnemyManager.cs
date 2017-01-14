using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

    public GameObject enemyTankPrefab;
    float timeSinceLastTank = 0;
    public float tankInterval = 6;
    public float tanksAtStart = 5;

	// Use this for initialization
	void Start () {
	    for (int i=0; i < tanksAtStart; i++)
        {
            Instantiate(enemyTankPrefab, new Vector2(0, 0), Quaternion.identity);
            timeSinceLastTank = tankInterval;
        }
	}
	
	// Update is called once per frame
	void Update () {

        timeSinceLastTank -= Time.deltaTime;

        if (timeSinceLastTank < 0)
        {
            Instantiate(enemyTankPrefab, new Vector2(0, 0), Quaternion.identity);
            timeSinceLastTank = tankInterval;
        }
    }
}
