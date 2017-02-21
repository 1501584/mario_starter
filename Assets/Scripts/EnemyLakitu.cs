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

    Vector3 start_position; // start position of the enemy

    // Use this for initialization
    void Start()
    {
        playerGameObject = GameObject.FindGameObjectWithTag("Player");

        startPosition = transform.position;
        direction = Vector3.zero;
        enemySpawnTimer = timeToSpawnEnemy;
        StartCoroutine("Move");

        // record the start position
        start_position = transform.position;

    }
	
	// Update is called once per frame
	void Update () {
        enemySpawnTimer += Time.deltaTime;
    }

    public void Reset()
    {
        // reset the enemy position to the start position
        transform.position = start_position;

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
