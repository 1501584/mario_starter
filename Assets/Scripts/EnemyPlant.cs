using UnityEngine;
using System.Collections;

public class EnemyPlant : MonoBehaviour
{
    public float speed;

    private bool moveUp;
    private Vector3 startPosition;
    private Vector3 direction;
    private GameObject playerGameObject;

    // Use this for initialization
    void Start()
    {
        playerGameObject = GameObject.FindGameObjectWithTag("Player");

        moveUp = true;
        startPosition = transform.position;
        direction = Vector3.zero;
        StartCoroutine("Move");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator Move()
    {
        Vector3 direction = new Vector3(0.0f, 0.0f, 0.0f);
        CharacterController controller = GetComponent<CharacterController>();
        while (true)
        {
            if (moveUp)
            {
                direction.y = 1.0f;
            }
            else
            {
                direction.y = -1.0f;
            }
            direction *= speed;

            // make the call to move the character controller
            transform.position += direction * Time.deltaTime;
            //controller.Move(direction * Time.deltaTime);

            if (transform.position.y > startPosition.y + 1.5f)
            {
                moveUp = false;
            }
            else if (transform.position.y < startPosition.y - 1)
            {
                moveUp = true;
            }
            yield return null;
        }
    }

    void OnTriggerEnter(Collider hit)
    {
        // find out what we've hit
        if (hit.gameObject.CompareTag("Player"))
        {
            // we've hit the player

            // reset the enemy
            Object.FindObjectOfType<EnemyManager>().RestartEnemies();
        }

    }

    public void Reset()
    {
        // reset the enemy position to the start position
        transform.position = startPosition;

        moveUp = true;
        direction = Vector3.zero;
    }

}
