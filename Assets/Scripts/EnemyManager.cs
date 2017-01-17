using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

    public GameObject enemyTankPrefab;

	// Use this for initialization
	void Start () {

	}
	
    public int getEnemyCount()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    public void NextEnemies()
    {
        Instantiate(enemyTankPrefab, new Vector2(0, 0), Quaternion.identity);
    }
}
