using UnityEngine;
using UnityEngine.AI;

public class RespawnableObjectFalling : MonoBehaviour
{
    [HideInInspector]
    public bool hasSpawned = false;
    public float spawnHeight;
    public float spawnMoveSpeed;

    public NavMeshAgent agent;


    // Use this for initialization
    void Start()
    {
        Reset();
    }

    public void Reset()
    {
        hasSpawned = false;
        transform.Translate(Vector3.up * spawnHeight);
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasSpawned)
        {
            transform.Translate(-spawnMoveSpeed * Time.deltaTime * Vector3.up);

            if (transform.position.y <= 0)
            {
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);
                hasSpawned = true;

                if (agent != null)
                {
                    agent.enabled = true;
                }
                //randomTarget = area.RandomPosition();
            }
        }
    }
}
