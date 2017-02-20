using UnityEngine;
using System.Collections;

public class EnemyLakitu : MonoBehaviour {
    public float speed;
    public float distToSpawnSpiner;
    public float timeToSpawnEnemy;
    public GameObject enemySpinerPrefab;

    private Vector3 startPosition;
    private Vector3 direction;
    private GameObject playerGameObject;
    private float enemySpawnTimer;

    // Use this for initialization
    void Start()
    {
        playerGameObject = GameObject.FindGameObjectWithTag("Player");

        startPosition = transform.position;
        direction = Vector3.zero;
        enemySpawnTimer = timeToSpawnEnemy;
        StartCoroutine("Move");	
	}
	
	// Update is called once per frame
	void Update () {
        enemySpawnTimer += Time.deltaTime;
    }

    private IEnumerator Move()
    {
        
        while (true)
        {
            Vector3 direction = transform.position - playerGameObject.transform.position;

            // Check if distance is small enough to spawn spiner
            if(direction.x < distToSpawnSpiner)
            {
                SpawnSpiner();
            }

            // Movement
            direction.Normalize();
            direction *= speed;
            direction.y = 0;

            // make the call to move the character controller
            transform.position -= direction * Time.deltaTime;

            yield return null;
        }
    }

    private void SpawnSpiner()
    {
        if(enemySpinerPrefab == null)
        {
            Debug.Log("Prefab is null.");
        }
        else
        {
            if (enemySpawnTimer > timeToSpawnEnemy)
            {
                GameObject enemy = Instantiate(enemySpinerPrefab, transform.position, Quaternion.identity) as GameObject;
                enemySpawnTimer = 0;
            }
        }
    }
}
