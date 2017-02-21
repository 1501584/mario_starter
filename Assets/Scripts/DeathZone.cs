using UnityEngine;
using System.Collections;

public class DeathZone : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Reset()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            // we've hit the player

            // reset the enemy
            Object.FindObjectOfType<EnemyManager>().RestartEnemies();

        }
        else if (col.gameObject.CompareTag("Enemy"))
        {
            Destroy(col.gameObject, 3); 
        }
    }
}
