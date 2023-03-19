using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator anim;
    public GameObject spawnEffect;
    public GameObject ragdoll;
    public GameObject bloodEffect;

    [SerializeField] GameObject collisionSphere;

    public float spawnHeight;
    public float spawnMoveSpeed;
    public float jumpAttackDistance;

    public float walkspeed;
    public float runspeed;

    public Color normalColor;
    public Color angryColor;

    Transform player;
    MoveArea area;

    bool spawned;
    [SerializeField] bool isAngry;

    Vector3 randomTarget;
    NavMeshPath path;

    public void Init(MoveArea moveArea, Transform plr)
    {
        area = moveArea;
        player = plr;

        path = new NavMeshPath();
        agent.speed = walkspeed;
        agent.enabled = false;

        Instantiate(spawnEffect, transform.position, transform.rotation);
        transform.Translate(Vector3.up * spawnHeight);

        if (isAngry)
        {
            randomTarget = plr.transform.position;
        }
    }

    void Update()
    {
        if (!spawned)
        {
            transform.Translate(Vector3.up * Time.deltaTime * -spawnMoveSpeed);

            if (transform.position.y <= 0)
            {
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);
                spawned = true;
                agent.enabled = true;
                collisionSphere.SetActive(true);

                anim.SetInteger("State", 1);
                if (area == null)
                {
                    Debug.Log("Area is null!");
                }
                randomTarget = area.RandomPosition();
            }
            return;
        }

        if (isAngry && player != null)
        {
            agent.CalculatePath(player.position, path);

            if (path.status != NavMeshPathStatus.PathComplete)
            {
                if (anim.GetInteger("State") != 1)
                    ContinueWalking();

                RandomWalk();
                return;
            }

            agent.destination = player.position;

            //if (anim.GetInteger("State") != 2)
            //{
            //    anim.SetInteger("State", 2);
            //    agent.speed = runspeed;
            //    agent.stoppingDistance = jumpAttackDistance;
            //}

            //if (Vector3.Distance(transform.position, player.position) < agent.stoppingDistance + 0.1f)
            //{
            //    agent.isStopped = true;
            //    transform.LookAt(player.position);
            //    anim.SetInteger("State", 3);
            //    spawned = false;

            //    StartCoroutine(Attack());
            //}
        }
        else
        {
            RandomWalk();
        }
    }

    void ContinueWalking()
    {
        anim.SetInteger("State", 1);
        randomTarget = area.RandomPosition();
        agent.speed = walkspeed;
        agent.isStopped = false;
        spawned = true;
    }

    void RandomWalk()
    {
        if (Vector3.Distance(transform.position, randomTarget) < agent.stoppingDistance + 0.1f)
        {
            randomTarget = area.RandomPosition();
        }
        else
        {
            agent.destination = randomTarget;
        }
    }

    public void Hit()
    {
        Instantiate(bloodEffect, transform.position + Vector3.up * 1.5f, transform.rotation);

        if (isAngry)
        {
            Die();
        }
        else
        {
            isAngry = true;
        }
    }

    void Die()
    {
        GameObject newRagdoll = Instantiate(ragdoll, transform.position, transform.rotation);
        newRagdoll.GetComponentInChildren<Renderer>().material.color = angryColor;

        Destroy(gameObject);
    }
}
